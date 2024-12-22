public class Task : IReportable{
    //Properties
    public Guid ID{get; private set;}
    public string Title{get; set;}
    public string? Description{get; set;}
    public Team AssignedTeam{get; private set;}
    public DateTime Deadline{get; set;}
    public bool IsCompleted{get; private set;}
    public TaskPriority Priority{get; set;} //Enum for task priority
    public List<Task> Subtasks{get; private set;} //Collection of subtasks

    //Constructor
    public Task(string title, Team assignedTeam, DateTime deadline, TaskPriority priority){
        if (string.IsNullOrWhiteSpace(title))
        {
            throw new ArgumentException("Title cannot be empty!");
        }
        if (assignedTeam == null)
        {
            throw new ArgumentNullException(nameof(assignedTeam), "Assigned team cannot be null!");
        }
        if (deadline < DateTime.UtcNow)
        {
            throw new ArgumentException("Deadline cannot be in the past!");
        }
        
        ID = Guid.NewGuid(); //Auto generate unique task ID
        Title = title;
        AssignedTeam = assignedTeam;
        Deadline = deadline;
        Priority = priority;
        IsCompleted = false; //Default to not completed
        Subtasks = new List<Task>(); //Initialize the subtasks list
    }

    //Mark task as completed
    public void MarkAsCompleted(bool logCompletion = true){
        IsCompleted = true;
        
        //When the task is completed all subtasks need to be completed as well
        foreach(Task subtask in Subtasks)
        {
            subtask.MarkAsCompleted(false);
        }
        if (logCompletion){
            Console.WriteLine($"Task '{Title}' completed by team '{AssignedTeam.Name}'.");
        }
    }

    //Add subtask to the task
    public void AddSubtask(string title, DateTime deadline, TaskPriority priority){
        if (Subtasks.Count >= AppSetting.MaxSubTasks)
        {
            throw new InvalidOperationException($"Cannot add more than {AppSetting.MaxSubTasks} subtasks!");
        }
        
        Task subtask = new Task(title, AssignedTeam, deadline, priority);
        
        //Validate duplicate titles
        if (Subtasks.Exists(s => s.Title.Equals(subtask.Title, StringComparison.OrdinalIgnoreCase))){
            throw new ArgumentException($"A subtask with the title '{subtask.Title}' already exists!");
        }
        Subtasks.Add(subtask);
    }
    
    //Remove subtask by ID
    public void RemoveSubtask(Guid subtaskId){
        Task subtask = Subtasks.FirstOrDefault(s => s.ID == subtaskId) ?? throw new ArgumentException($"Subtask with ID '{subtaskId}' not found!");
        Subtasks.Remove(subtask);
    }

    //Generate report for the task
    public string GenerateReport()
    {
        return $"Task: {Title} Assigned To: {AssignedTeam.Name}, Deadline: {Deadline}, Completed: {IsCompleted}";
    }

    public struct TaskStats{
        public int TotalTasks{get;}
        public int CompletedTasks{get;}
        public TaskStats(int totalTasks, int completedTask){
            TotalTasks = totalTasks;
            CompletedTasks = completedTask;
        }
        public override string ToString()
        {
            return $"Total Tasks: {TotalTasks}, Completed Tasks: {CompletedTasks}";
        }
    }

    public enum TaskPriority{
        High,
        Medium,
        Low
    }

}