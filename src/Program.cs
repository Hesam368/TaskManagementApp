using TaskManagementApp.Models;
using TaskManagementApp.Services;
using TaskManagementApp.Utilities;

public class Program{
    public static void Main(){
        var taskManager = TaskManager.Instance;
        Console.WriteLine("=== Task Management System ===");

        // Step 1: Create Teams
        Team devTeam = new Team("Development Team");
        TeamMember alice = new TeamMember("Alice", TeamMember.RoleType.TeamLeader);
        TeamMember bob = new TeamMember("Bob", TeamMember.RoleType.Developer);
        devTeam.AddMember(alice);
        devTeam.AddMember(bob);
        devTeam.AssignTeamLeader(alice);

        Team qaTeam = new Team("QA Team");
        TeamMember charlie = new TeamMember("Charlie", TeamMember.RoleType.Tester);
        qaTeam.AddMember(charlie);

        Console.WriteLine("Teams created:");
        taskManager.DisplayReports(new List<IReportable> { devTeam, qaTeam });
        Console.WriteLine();

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
        taskManager.DisplayReports(new List<IReportable> { mainTask, bugFixTask });
        Console.WriteLine();

        // Step 3: Create Subtasks
        Subtask subtask1 = taskManager.CreateSubtask(
            title: "Design the Feature",
            deadline: DateTime.UtcNow.AddDays(3),
            priority: WorkTask.TaskPriority.Medium,
            parentTask: mainTask
        );

        Subtask subtask2 = taskManager.CreateSubtask(
            title: "Implement the Feature",
            deadline: DateTime.UtcNow.AddDays(5),
            priority: WorkTask.TaskPriority.High,
            parentTask: mainTask
        );

        Subtask subtask3 = taskManager.CreateSubtask(
            title: "Write Unit Tests",
            deadline: DateTime.UtcNow.AddDays(6),
            priority: WorkTask.TaskPriority.Medium,
            parentTask: mainTask
        );

        Console.WriteLine("Main Task and Subtasks:");
        Console.WriteLine(mainTask.GenerateReport());
        foreach (var subtask in mainTask.Subtasks)
        {
            Console.WriteLine($"  - {subtask.GenerateReport()}");
        }
        Console.WriteLine();

        // Step 4: Mark a Task as Completed
        taskManager.CompleteTask(mainTask.ID);
        Console.WriteLine();

        // Step 5: View All Tasks by Priority
        Console.WriteLine("Tasks by Priority (High):");
        foreach (var task in taskManager.GetTasksByPriority(WorkTask.TaskPriority.High))
        {
            Console.WriteLine($"- {task.GenerateReport()}");
        }
        Console.WriteLine();

        // Step 6: Display Task Statistics
        var stats = taskManager.GetTaskStats();
        Console.WriteLine("Task Statistics:");
        Console.WriteLine(stats);
        Console.WriteLine(TaskHelper.CalculateCompletionPercentage(new List<WorkTask> { mainTask, bugFixTask }) + "% completed.");
    }
}