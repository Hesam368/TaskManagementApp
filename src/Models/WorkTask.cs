using TaskManagementApp.Configurations;

namespace TaskManagementApp.Models{
    public class WorkTask : IReportable{
        //Properties
        public Guid ID{get; private set;}
        public string Title{get; set;}
        public string? Description{get; set;}
        public Team AssignedTeam{get; private set;}
        public DateTime Deadline{get; set;}
        public bool IsCompleted{get; private set;}
        public TaskPriority Priority{get; set;} //Enum for task priority
        public List<WorkTask> Subtasks{get; private set;} //Collection of subtasks

        //Constructor
        protected WorkTask(string title, Team assignedTeam, DateTime deadline, TaskPriority priority){
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
            Subtasks = new List<WorkTask>(); //Initialize the subtasks list
        }

        internal static WorkTask Create(string title, Team assignedTeam, DateTime deadline, TaskPriority priority)
        {
            return new WorkTask(title, assignedTeam, deadline, priority);
        }

        //Mark task as completed
        public void MarkAsCompleted(bool logCompletion = true){
            IsCompleted = true;
            
            //When the task is completed all subtasks need to be completed as well
            foreach(WorkTask subtask in Subtasks)
            {
                subtask.MarkAsCompleted(false);
            }
            if (logCompletion){
                Console.WriteLine($"Task '{Title}' and its subtasks completed by team '{AssignedTeam.Name}'.");
            }
        }

        //Add subtask to the task
        public void AddSubtask(Subtask subtask){
            if (Subtasks.Count >= AppSetting.MaxSubTasks)
            {
                throw new InvalidOperationException($"Cannot add more than {AppSetting.MaxSubTasks} subtasks!");
            }
            if (subtask == null)
            {
                throw new ArgumentNullException(nameof(subtask), "Subtask cannot be null!");
            }
                        
            //Validate duplicate titles
            if (Subtasks.Exists(s => s.Title.Equals(subtask.Title, StringComparison.OrdinalIgnoreCase))){
                throw new ArgumentException($"A subtask with the title '{subtask.Title}' already exists!");
            }
            Subtasks.Add(subtask);
        }

        //Generate report for the task
        public virtual string GenerateReport()
        {
            return $"Task: {Title} (Priority: {Priority}), Assigned To: {AssignedTeam.Name}, Deadline: {Deadline}, Completed: {IsCompleted}";
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
}