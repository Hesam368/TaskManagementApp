using TaskManagementApp.Models;

namespace TaskManagementApp.Services
{
    public class TaskManager : ITaskManager
    {
        //Properties
        private readonly List<WorkTask> _tasks = new();
        private readonly ILogger _logger;
        private readonly TaskConfig _config;

        //Constructor
        public TaskManager(ILogger logger, TaskConfig config)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _config = config ?? throw new ArgumentNullException(nameof(config));
        }

        //Create and assign a task to a team
        public WorkTask CreateTask(
            string title,
            Team assignedTeam,
            DateTime deadline,
            WorkTask.TaskPriority priority,
            ILogger logger,
            TaskConfig config
        )
        {
            WorkTask task = WorkTask.Create(
                title,
                assignedTeam,
                deadline,
                priority,
                logger,
                config
            );
            _tasks.Add(task);
            return task;
        }

        //Create a subtask
        public Subtask CreateSubtask(
            string title,
            DateTime deadline,
            WorkTask.TaskPriority priority,
            WorkTask parentTask,
            ILogger logger,
            TaskConfig config
        )
        {
            return Subtask.Create(
                title,
                parentTask.AssignedTeam,
                deadline,
                priority,
                parentTask,
                logger,
                config
            );
        }

        //Complete a task
        public void CompleteTask(Guid taskId)
        {
            var task = _tasks.FirstOrDefault(t => t.ID == taskId);
            task?.MarkAsCompleted();
        }

        public IEnumerable<WorkTask> GetTasksByTeam(Team team)
        {
            return _tasks.Where(t => t.AssignedTeam == team);
        }

        public IEnumerable<WorkTask> GetTasksByPriority(WorkTask.TaskPriority priority)
        {
            return _tasks.Where(t => t.Priority == priority);
        }

        public WorkTask.TaskStats GetTaskStats()
        {
            int completedTasks = _tasks.Count(t => t.IsCompleted);
            return new WorkTask.TaskStats(_tasks.Count, completedTasks);
        }
    }
}
