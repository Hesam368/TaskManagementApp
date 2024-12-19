public class Program{
    public static void Main(){
        
    // Step 1: Create a Team
        Team devTeam = new Team("Development Team");
        TeamMember alice = new TeamMember("Alice", TeamMember.RoleType.TeamLeader);
        TeamMember bob = new TeamMember("Bob", TeamMember.RoleType.Developer);
        devTeam.AddMember(alice);
        devTeam.AddMember(bob);
        devTeam.AssignTeamLeader(alice);
        Console.WriteLine($"Team '{devTeam.Name}' created with leader: {devTeam.TeamLeader.Name}");

    // Step 2: Create a Task and Assign It to the Team
        TaskManager manager = TaskManager.Instance;
        Task mainTask = manager.CreateTask(
            title: "Build API",
            assignedTeam: devTeam,
            deadline: DateTime.Now.AddDays(5),
            priority: Task.TaskPriority.High
        );
        
    // Step 3: Add Subtasks
        Task subtask1 = new Task("Set Up Database", devTeam, DateTime.Now.AddDays(3), Task.TaskPriority.Medium);
        Task subtask2 = new Task("Implement Endpoints", devTeam, DateTime.Now.AddDays(4), Task.TaskPriority.High);
        mainTask.AddSubtask(subtask1);
        mainTask.AddSubtask(subtask2);
        Console.WriteLine($"Subtasks added to task '{mainTask.Title}':");
        foreach (var subtask in mainTask.Subtasks)
        {
            Console.WriteLine($"- {subtask.Title}");
        }

    // Step 4: Mark Main Task as Completed
        mainTask.MarkAsCompleted();
        Console.WriteLine($"Task '{mainTask.Title}' status: Completed = {mainTask.IsCompleted}");
        foreach (var subtask in mainTask.Subtasks)
        {
            Console.WriteLine($"- Subtask '{subtask.Title}' status: Completed = {subtask.IsCompleted}");
        }

    // Step 5: List Tasks by Priority
        Console.WriteLine("\nTasks by priority:");
        foreach (var task in manager.GetTasksByPriority(Task.TaskPriority.High))
        {
            Console.WriteLine($"- {task.Title} (Priority: {task.Priority})");
        }

    // Step 6: Test Removing a Subtask
        Console.WriteLine($"\nRemoving subtask '{subtask1.Title}'...");
        mainTask.RemoveSubtask(subtask1);
        Console.WriteLine("Remaining subtasks:");
        foreach (var subtask in mainTask.Subtasks)
        {
            Console.WriteLine($"- {subtask.Title}");
        }
    }
}