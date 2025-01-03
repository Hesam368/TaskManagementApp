namespace TaskManagementApp.Models{
    public class Subtask : WorkTask
    {
        public WorkTask ParentTask { get; private set; }
        private Subtask(
            string title,
            Team assignedTeam,
            DateTime deadline,
            TaskPriority priority,
            WorkTask parentTask) : base(title, assignedTeam, deadline, priority)
        {
            if(parentTask == null)
            {
                throw new ArgumentNullException(nameof(parentTask), "Parent task cannot be null!");
            }
            ParentTask = parentTask;
            ParentTask.AddSubtask(this);
        }

        internal static Subtask Create(string title, Team assignedTeam, DateTime deadline, TaskPriority priority, WorkTask parentTask)
        {
            return new Subtask(title, assignedTeam, deadline, priority, parentTask);
        }

        public override string GenerateReport()
        {
            return $"Subtask: {Title} (Priority: {Priority}), Parent Task: {ParentTask.Title}, Deadline: {Deadline}, Completed: {IsCompleted}";
        }
    }
}