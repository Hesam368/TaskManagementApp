using Microsoft.Extensions.DependencyInjection;
using TaskManagementApp.Models;
using TaskManagementApp.Services;
using TaskManagementApp.Utilities;

public class Program
{
    public static void Main()
    {
        var serviceProvider = new ServiceCollection()
            .AddSingleton<ITaskService, TaskService>()
            .AddSingleton<TaskConfig>(new TaskConfig(10))
            .AddSingleton<IReportable<WorkTask>, TaskReportGenerator>()
            .AddSingleton<IReportable<Subtask>, SubtaskReportGenerator>()
            .AddSingleton<IReportable<Team>, TeamReportGenerator>()
            .AddSingleton<ITaskManager, TaskManager>()
            .AddSingleton<ILogger, ConsoleLogger>()
            .AddSingleton<ITeamManager, TeamManager>()
            .BuildServiceProvider();

        var taskManager = serviceProvider.GetRequiredService<ITaskManager>();
        var teamManager = serviceProvider.GetRequiredService<ITeamManager>();
        var logger = serviceProvider.GetRequiredService<ILogger>();
        var taskService = serviceProvider.GetRequiredService<ITaskService>();
        var taskReportGenerator = serviceProvider.GetRequiredService<IReportable<WorkTask>>();
        var subtaskReportGenerator = serviceProvider.GetRequiredService<IReportable<Subtask>>();

        Console.WriteLine("=== Task Management System ===");

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

        Console.WriteLine("Teams created:");

        // Step 2: Create Tasks
        WorkTask mainTask = taskManager.CreateTask(
            title: "Develop New Feature",
            assignedTeam: devTeam,
            deadline: DateTime.UtcNow.AddDays(7),
            priority: WorkTask.TaskPriority.High,
            logger: logger,
            config: new TaskConfig(10)
        );

        WorkTask bugFixTask = taskManager.CreateTask(
            title: "Fix Critical Bug",
            assignedTeam: qaTeam,
            deadline: DateTime.UtcNow.AddDays(2),
            priority: WorkTask.TaskPriority.High,
            logger: logger,
            config: new TaskConfig(10)
        );

        // Step 3: Create Subtasks
        Subtask subtask1 = taskManager.CreateSubtask(
            title: "Design the Feature",
            deadline: DateTime.UtcNow.AddDays(3),
            priority: WorkTask.TaskPriority.Medium,
            parentTask: mainTask,
            logger: logger,
            config: new TaskConfig(10)
        );

        Subtask subtask2 = taskManager.CreateSubtask(
            title: "Implement the Feature",
            deadline: DateTime.UtcNow.AddDays(5),
            priority: WorkTask.TaskPriority.High,
            parentTask: mainTask,
            logger: logger,
            config: new TaskConfig(10)
        );

        Subtask subtask3 = taskManager.CreateSubtask(
            title: "Write Unit Tests",
            deadline: DateTime.UtcNow.AddDays(6),
            priority: WorkTask.TaskPriority.Medium,
            parentTask: mainTask,
            logger: logger,
            config: new TaskConfig(10)
        );

        Console.WriteLine("Main Task and Subtasks:");
        Console.WriteLine(taskReportGenerator.GenerateReport(mainTask));
        foreach (var subtask in mainTask.Subtasks)
        {
            Console.WriteLine($"  - {taskReportGenerator.GenerateReport(subtask)}");
        }
        Console.WriteLine();

        // Step 4: Mark a Task as Completed
        taskManager.CompleteTask(mainTask.ID);
        Console.WriteLine();

        // Step 5: View All Tasks by Priority
        Console.WriteLine("Tasks by Priority (High):");
        foreach (var task in taskManager.GetTasksByPriority(WorkTask.TaskPriority.High))
        {
            Console.WriteLine($"- {taskReportGenerator.GenerateReport(task)}");
        }
        Console.WriteLine();

        // Step 6: Display Task Statistics
        var stats = taskManager.GetTaskStats();
        Console.WriteLine("Task Statistics:");
        Console.WriteLine(stats);
        Console.WriteLine(
            taskService.CalculateCompletionPercentage(new List<WorkTask> { mainTask, bugFixTask })
                + "% completed."
        );
    }
}
