using Microsoft.Extensions.DependencyInjection;
using TaskManagementApp.Interfaces;
using TaskManagementApp.Models;
using TaskManagementApp.Reporting;
using TaskManagementApp.Services;

public class Program
{
    public static void Main()
    {
        var serviceProvider = new ServiceCollection()
            .AddTransient<ITaskService, TaskService>()
            .AddTransient<IReportable<WorkTask>, TaskReportGenerator>()
            .AddTransient<IReportable<Subtask>, SubtaskReportGenerator>()
            .AddTransient<IReportable<Team>, TeamReportGenerator>()
            .AddTransient<ILogger, ConsoleLogger>()
            .AddSingleton<ITaskManager, TaskManager>()
            .AddSingleton<ITeamManager, TeamManager>()
            .AddSingleton<TaskConfig>(new TaskConfig(10))
            .BuildServiceProvider();

        var taskService = serviceProvider.GetRequiredService<ITaskService>();
        var taskReportGenerator = serviceProvider.GetRequiredService<IReportable<WorkTask>>();
        var subtaskReportGenerator = serviceProvider.GetRequiredService<IReportable<Subtask>>();
        var teamReportGenerator = serviceProvider.GetRequiredService<IReportable<Team>>();
        var logger = serviceProvider.GetRequiredService<ILogger>();
        var taskManager = serviceProvider.GetRequiredService<ITaskManager>();
        var teamManager = serviceProvider.GetRequiredService<ITeamManager>();

        logger.Log("=== Task Management System ===");

        // Step 1: Create Teams
        Team devTeam = teamManager.CreateTeam("Development Team");
        TeamMember alice = teamManager.CreateTeamMember("Alice", TeamMember.RoleType.TeamLeader);
        TeamMember bob = teamManager.CreateTeamMember("Bob", TeamMember.RoleType.Developer);
        devTeam.AddMember(alice);
        devTeam.AddMember(bob);
        devTeam.AssignTeamLeader(alice);

        Team qaTeam = teamManager.CreateTeam("QA Team");
        TeamMember charlie = teamManager.CreateTeamMember("Charlie", TeamMember.RoleType.Tester);
        qaTeam.AddMember(charlie);

        logger.Log("Teams created:");
        foreach (var team in teamManager.GetAllTeams())
        {
            logger.Log(teamReportGenerator.GenerateReport(team));
        }

        // Step 2: Create Tasks
        WorkTask mainTask = taskManager.CreateTask(
            title: "Develop New Feature",
            assignedTeam: devTeam,
            deadline: DateTime.UtcNow.AddDays(7),
            priority: WorkTask.TaskPriority.High
        );

        WorkTask bugFixTask = taskManager.CreateTask(
            title: "Fix Critical Bug",
            assignedTeam: qaTeam,
            deadline: DateTime.UtcNow.AddDays(2),
            priority: WorkTask.TaskPriority.High
        );

        // Step 3: Create Subtasks
        taskManager.CreateSubtask(
            title: "Design the Feature",
            deadline: DateTime.UtcNow.AddDays(3),
            priority: WorkTask.TaskPriority.Medium,
            parentTask: mainTask
        );

        taskManager.CreateSubtask(
            title: "Implement the Feature",
            deadline: DateTime.UtcNow.AddDays(5),
            priority: WorkTask.TaskPriority.High,
            parentTask: mainTask
        );

        taskManager.CreateSubtask(
            title: "Write Unit Tests",
            deadline: DateTime.UtcNow.AddDays(6),
            priority: WorkTask.TaskPriority.Medium,
            parentTask: mainTask
        );

        logger.Log("Main Task and Subtasks:");
        logger.Log(taskReportGenerator.GenerateReport(mainTask));
        foreach (Subtask subtask in mainTask.Subtasks)
        {
            logger.Log($"  - {subtaskReportGenerator.GenerateReport(subtask)}");
        }
        logger.Log();

        // Step 4: Mark a Task as Completed
        taskManager.CompleteTask(mainTask.ID);
        logger.Log();

        // Step 5: View All Tasks by Priority
        logger.Log("Tasks by Priority (High):");
        foreach (var task in taskManager.GetTasksByPriority(WorkTask.TaskPriority.High))
        {
            logger.Log($"- {taskReportGenerator.GenerateReport(task)}");
        }
        logger.Log();

        // Step 6: Display Task Statistics
        var stats = taskManager.GetTaskStats();
        logger.Log("Task Statistics:");
        logger.Log(stats.ToString());
        logger.Log(
            taskService.CalculateCompletionPercentage(taskManager.GetAllTasks()) + "% completed."
        );
    }
}
