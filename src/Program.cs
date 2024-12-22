public class Program{
    public static void Main(){
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

        TaskManager.Instance.AddTeam(devTeam);
        TaskManager.Instance.AddTeam(qaTeam);

        Console.WriteLine("Teams created:");
        Console.WriteLine(devTeam.GenerateReport());
        Console.WriteLine(qaTeam.GenerateReport());
        Console.WriteLine();

        // Step 2: Create Tasks
        Task mainTask = TaskManager.Instance.CreateTask(
            title: "Develop New Feature",
            assignedTeam: devTeam,
            deadline: DateTime.UtcNow.AddDays(7),
            priority: Task.TaskPriority.High
        );

        Task bugFixTask = TaskManager.Instance.CreateTask(
            title: "Fix Critical Bug",
            assignedTeam: qaTeam,
            deadline: DateTime.UtcNow.AddDays(2),
            priority: Task.TaskPriority.High
        );

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
        Console.WriteLine($"Task '{mainTask.Title}' and its subtasks have been completed.");
        Console.WriteLine();

        // Step 5: View All Tasks by Priority
        Console.WriteLine("Tasks by Priority (High):");
        foreach (var task in TaskManager.Instance.GetTasksByPriority(Task.TaskPriority.High))
        {
            Console.WriteLine($"- {task.GenerateReport()}");
        }
        Console.WriteLine();

        // Step 6: Display Task Statistics
        var stats = TaskManager.Instance.GetTaskStats();
        Console.WriteLine("Task Statistics:");
        Console.WriteLine(stats);
    }
}