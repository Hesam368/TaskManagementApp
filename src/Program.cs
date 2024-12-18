public class Program{
    public static void Main(){
        
        List<Task> tasks = new List<Task>();

        while(true){
            Console.WriteLine("\n---Task Management System---");
            Console.WriteLine("1. Create a Task");
            Console.WriteLine("2. View All Tasks");
            Console.WriteLine("3. Add a Subtask");
            Console.WriteLine("4. Mark Task as Completed");
            Console.WriteLine("5. Remove a Subtask");
            Console.WriteLine("6. Exit");

            Console.Write("Enter your choice: ");
            var choice = Console.ReadLine();

            switch(choice){
                case "1":
                    CreateTask(tasks);
                    break;
                case "2":
                    ViewTasks(tasks);
                    break;
                case "3":
                    AddSubtask(tasks);
                    break;
                case "4":
                    MarkTaskAsCompleted(tasks);
                    break;
                case "5":
                    RemoveSubtask(tasks);
                    break;
                case "6":
                    Console.WriteLine("Exiting...");
                    return;
                default:
                    Console.WriteLine("Invalid choice! Please try again.");
                    break;
            }
        }
    }

    private static void CreateTask(List<Task> tasks){
        Console.Write("Enter task title: ");
        string title = Console.ReadLine();

        Console.Write("Enter task description: ");
        string description = Console.ReadLine();

        Console.Write("Enter team member name: ");
        string doerName = Console.ReadLine();
        Console.WriteLine("Select role: 1. TeamLeader 2. Developer 3. Tester 4. Designer");
        int roleChoice = int.Parse(Console.ReadLine());
        TeamMember.RoleType role = (TeamMember.RoleType)(roleChoice);

        TeamMember doer = new TeamMember(doerName, role);
        
        Console.Write("Enter task deadline (yyyy-MM-dd): ");
        DateTime deadline = DateTime.Parse(Console.ReadLine());

        Console.WriteLine("Select priority: 1. High 2. Medium 3. Low");
        int priorityChoice = int.Parse(Console.ReadLine());
        Task.TaskPriority priority = (Task.TaskPriority)(priorityChoice - 1);

        Task newTask = new Task(title, doer, deadline, priority);
        newTask.Description = description;

        tasks.Add(newTask);

        Console.WriteLine($"Task '{title}' created successfully!");
    }

    private static void ViewTasks(List<Task> tasks){
        if (tasks.Count == 0)
        {
            Console.WriteLine("No tasks available.");
            return;
        }
        foreach (Task task in tasks){
            Console.WriteLine($"\nTask: {task.Title} (Priority: {task.Priority}, Completed: {task.IsCompleted})");
            Console.WriteLine($"  Description: {task.Description}");
            Console.WriteLine($"  Assigned to: {task.Doer.Name} ({task.Doer.Role})");
            Console.WriteLine($"  Deadline: {task.Deadline}");
            if (task.Subtasks.Count > 0)
            {
                Console.WriteLine("  Subtasks:");
                foreach (Task subtask in task.Subtasks)
                {
                    Console.WriteLine($"    - {subtask.Title} (Completed: {subtask.IsCompleted})");
                }
            }
        }
    }

    private static void AddSubtask(List<Task> tasks){
        Console.Write("Enter the title of the main task to add a subtask to: ");
        string mainTaskTitle = Console.ReadLine();

        Task mainTask = tasks.Find(t => t.Title.Equals(mainTaskTitle, StringComparison.OrdinalIgnoreCase));
        if (mainTask == null)
        {
            Console.WriteLine("Main task not found.");
            return;
        }

        Console.Write("Enter subtask title: ");
        string subtaskTitle = Console.ReadLine();

        Console.Write("Enter team member name for the subtask: ");
        string doerName = Console.ReadLine();

        Console.WriteLine("Select role: 1. TeamLeader 2. Developer 3. Tester 4. Designer");
        int roleChoice = int.Parse(Console.ReadLine());
        TeamMember.RoleType role = (TeamMember.RoleType)(roleChoice - 1);

        TeamMember subtaskDoer = new TeamMember(doerName, role);

        Console.Write("Enter subtask deadline (yyyy-MM-dd): ");
        DateTime subtaskDeadline = DateTime.Parse(Console.ReadLine());

        Console.WriteLine("Select priority: 1. High 2. Medium 3. Low");
        int priorityChoice = int.Parse(Console.ReadLine());
        Task.TaskPriority priority = (Task.TaskPriority)(priorityChoice - 1);

        Task subtask = new Task(subtaskTitle, subtaskDoer, subtaskDeadline, priority);

        try
        {
            mainTask.AddSubtask(subtask);
            Console.WriteLine($"Subtask '{subtaskTitle}' added successfully!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }

    private static void MarkTaskAsCompleted(List<Task> tasks){
        Console.Write("Enter the title of the task to mark as completed: ");
        string taskTitle = Console.ReadLine();

        Task task = tasks.Find(t => t.Title.Equals(taskTitle, StringComparison.OrdinalIgnoreCase));
        if (task == null)
        {
            Console.WriteLine("Task not found.");
            return;
        }

        task.MarkAsCompleted();
        Console.WriteLine($"Task '{task.Title}' marked as completed!");
    }

    private static void RemoveSubtask(List<Task> tasks){
        Console.Write("Enter the title of the main task to remove a subtask from: ");
        string mainTaskTitle = Console.ReadLine();

        Task mainTask = tasks.Find(t => t.Title.Equals(mainTaskTitle, StringComparison.OrdinalIgnoreCase));
        if (mainTask == null)
        {
            Console.WriteLine("Main task not found.");
            return;
        }

        Console.Write("Enter the title of the subtask to remove: ");
        string subtaskTitle = Console.ReadLine();

        Task subtask = mainTask.Subtasks.Find(st => st.Title.Equals(subtaskTitle, StringComparison.OrdinalIgnoreCase));
        if (subtask == null)
        {
            Console.WriteLine("Subtask not found.");
            return;
        }

        mainTask.RemoveSubtask(subtask);
        Console.WriteLine($"Subtask '{subtask.Title}' removed successfully!");
    }
}