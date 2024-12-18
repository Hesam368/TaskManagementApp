public class Task : IReportable{
    public Guid ID{get; private set;}
    public string Title{get; set;}
    public string? Description{get; set;}
    public Team AssignedTeam{get; set;}
    public DateTime Deadline{get; set;}
    public bool IsCompleted{get; private set;}
    public TaskPriority Priority{get; set;} //Enum for task priority
    public List<Task> Subtasks{get; private set;} //Collection of subtasks

    public Task(string title, Team assignedTeam, DateTime deadline, TaskPriority priority){
        if (string.IsNullOrWhiteSpace(title)){throw new ArgumentException("Title cannot be empty!");}
        if (assignedTeam == null){throw new ArgumentException("Assigned team cannot be null!");}
        if (deadline < DateTime.Now){throw new ArgumentException("Deadline cannot be in the past!");}
        
        ID = Guid.NewGuid(); //Auto generate unique task ID
        Title = title;
        AssignedTeam = assignedTeam;
        Deadline = deadline;
        Priority = priority;
        IsCompleted = false; //Default to not completed
        Subtasks = new List<Task>(); //Initialize the subtasks list
    }

    public void MarkAsCompleted(){
        IsCompleted = true;
        foreach(Task subtask in Subtasks){ //When the task is completed all subtasks need to be completed
            subtask.MarkAsCompleted();
        }
        Console.WriteLine($"Task '{Title}' completed by team '{AssignedTeam.Name}'.");
    }

    public void AddSubtask(Task subtask){
        if (subtask == null){throw new ArgumentException("Subtask cannot be null!");}
        if (Subtasks.Count >= AppSetting.MaxSubTasks){
            throw new InvalidOperationException($"Cannot add more than {AppSetting.MaxSubTasks} subtasks!");
        }
        //Validate duplicate titles
        if (Subtasks.Exists(s => s.Title.Equals(subtask.Title, StringComparison.OrdinalIgnoreCase))){
            throw new ArgumentException($"A subtask with the title '{subtask.Title}' already exists!");
        }
        Subtasks.Add(subtask);
    }

    public void AddSubtasks(params Task[] tasks){
        foreach (Task task in tasks){
            AddSubtask(task);
        }
    }
    
    //Remove subtask by ID
    public void RemoveSubtask(Guid subtaskId){
        Task subtask = Subtasks.FirstOrDefault(s => s.ID == subtaskId) ?? throw new ArgumentException($"Subtask with ID '{subtaskId}' not found!");
        Subtasks.Remove(subtask);
    }

    //Remove subtask by Reference
    public void RemoveSubtask(Task subtask){
        if (subtask == null || !Subtasks.Contains(subtask)){throw new ArgumentException("Subtask not found!");}
        Subtasks.Remove(subtask);
    }

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