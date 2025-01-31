namespace TaskManagementApp.Models
{
    public class WorkTask
    {
        //Properties
        public Guid ID { get; }
        public string Title { get; }
        public string? Description { get; set; }
        public Team AssignedTeam { get; }
        public DateTime Deadline { get; }
        public bool IsCompleted { get; private set; }
        public TaskPriority Priority { get; } //Enum for task priority
        public List<WorkTask> Subtasks { get; } //Collection of subtasks
        private readonly ILogger _logger;
        private readonly TaskConfig _config;

        //Constructor
        protected WorkTask(
            string title,
            Team assignedTeam,
            DateTime deadline,
            TaskPriority priority,
            ILogger logger,
            TaskConfig config
        )
        {
            if (string.IsNullOrWhiteSpace(title))
            {
                throw new ArgumentException("Title cannot be empty!");
            }
            if (assignedTeam == null)
            {
                throw new ArgumentNullException(
                    nameof(assignedTeam),
                    "Assigned team cannot be null!"
                );
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
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _config = config ?? throw new ArgumentNullException(nameof(config));
        }

        public static WorkTask Create(
            string title,
            Team assignedTeam,
            DateTime deadline,
            TaskPriority priority,
            ILogger logger,
            TaskConfig config
        )
        {
            return new WorkTask(title, assignedTeam, deadline, priority, logger, config);
        }

        //Mark task as completed
        public void MarkAsCompleted(bool logCompletion = true)
        {
            IsCompleted = true;

            //When the task is completed all subtasks need to be completed as well
            foreach (WorkTask subtask in Subtasks)
            {
                subtask.MarkAsCompleted(false);
            }
            if (logCompletion)
            {
                _logger.Log(
                    $"Task '{Title}' and its subtasks completed by team '{AssignedTeam.Name}'."
                );
            }
        }

        //Add subtask to the task
        public void AddSubtask(Subtask subtask)
        {
            if (Subtasks.Count >= _config.MaxSubTasks)
            {
                throw new InvalidOperationException(
                    $"Cannot add more than {_config.MaxSubTasks} subtasks!"
                );
            }
            if (subtask == null)
            {
                throw new ArgumentNullException(nameof(subtask), "Subtask cannot be null!");
            }

            //Validate duplicate titles
            if (
                Subtasks.Exists(s =>
                    s.Title.Equals(subtask.Title, StringComparison.OrdinalIgnoreCase)
                )
            )
            {
                throw new ArgumentException(
                    $"A subtask with the title '{subtask.Title}' already exists!"
                );
            }
            Subtasks.Add(subtask);
        }

        public struct TaskStats
        {
            public int TotalTasks { get; }
            public int CompletedTasks { get; }

            public TaskStats(int totalTasks, int completedTask)
            {
                TotalTasks = totalTasks;
                CompletedTasks = completedTask;
            }

            public override string ToString()
            {
                return $"Total Tasks: {TotalTasks}, Completed Tasks: {CompletedTasks}";
            }
        }

        public enum TaskPriority
        {
            High,
            Medium,
            Low,
        }
    }
}
