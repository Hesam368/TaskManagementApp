using TaskManagementApp.Interfaces;

namespace TaskManagementApp.Models
{
    public class Subtask : WorkTask
    {
        public WorkTask ParentTask { get; }

        private Subtask(
            string title,
            Team assignedTeam,
            DateTime deadline,
            TaskPriority priority,
            WorkTask parentTask,
            ILogger logger,
            TaskConfig config
        )
            : base(title, assignedTeam, deadline, priority, logger, config)
        {
            ParentTask =
                parentTask
                ?? throw new ArgumentNullException(
                    nameof(parentTask),
                    "Parent task cannot be null!"
                );
        }

        public static Subtask Create(
            string title,
            Team assignedTeam,
            DateTime deadline,
            TaskPriority priority,
            WorkTask parentTask,
            ILogger logger,
            TaskConfig config
        )
        {
            var subtask = new Subtask(
                title,
                assignedTeam,
                deadline,
                priority,
                parentTask,
                logger,
                config
            );
            parentTask.AddSubtask(subtask);
            return subtask;
        }
    }
}
