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

        taskManager.AddTeam(devTeam);
        taskManager.AddTeam(qaTeam);

        Console.WriteLine("Teams created:");
        taskManager.DisplayReports(new List<IReportable> { devTeam, qaTeam });
        Console.WriteLine();

        // Step 2: Create Tasks
        Task mainTask = taskManager.CreateTask(
            title: "Develop New Feature",
            assignedTeam: devTeam,
            deadline: DateTime.UtcNow.AddDays(7),
            priority: Task.TaskPriority.High
        );

        Task bugFixTask = taskManager.CreateTask(
            title: "Fix Critical Bug",
            assignedTeam: qaTeam,
            deadline: DateTime.UtcNow.AddDays(2),
            priority: Task.TaskPriority.High
        );
        taskManager.DisplayReports(new List<IReportable> { mainTask, bugFixTask });
        Console.WriteLine();

        // Step 3: Add Subtasks to Main Task
        mainTask.AddSubtask("Design the Feature", DateTime.UtcNow.AddDays(3), Task.TaskPriority.Medium);
        mainTask.AddSubtask("Implement the Feature", DateTime.UtcNow.AddDays(5), Task.TaskPriority.High);
        mainTask.AddSubtask("Write Unit Tests", DateTime.UtcNow.AddDays(6), Task.TaskPriority.Medium);

        Console.WriteLine("Main Task and Subtasks:");
        Console.WriteLine(mainTask.GenerateReport());
        foreach (var subtask in mainTask.Subtasks)
        {
            Console.WriteLine($"  - {subtask.GenerateReport()}");
        }
        Console.WriteLine();

        // Step 4: Mark a Task as Completed
        mainTask.MarkAsCompleted();
        Console.WriteLine();

        // Step 5: View All Tasks by Priority
        Console.WriteLine("Tasks by Priority (High):");
        foreach (var task in taskManager.GetTasksByPriority(Task.TaskPriority.High))
        {
            Console.WriteLine($"- {task.GenerateReport()}");
        }
        Console.WriteLine();

        // Step 6: Display Task Statistics
        var stats = taskManager.GetTaskStats();
        Console.WriteLine("Task Statistics:");
        Console.WriteLine(stats);
    }
}