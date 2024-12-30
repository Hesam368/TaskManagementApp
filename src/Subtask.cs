public class Subtask : Task
{
    public Task ParentTask { get; set; }
    public Subtask(
        string title,
        Team assignedTeam,
        DateTime deadline,
        TaskPriority priority,
        Task parentTask) : base(title, assignedTeam, deadline, priority)
    {
        ParentTask = parentTask;
    }

    public override string GenerateReport()
    {
        return $"Subtask: {Title} (Priority: {Priority}), Parent Task: {ParentTask.Title}, Deadline: {Deadline}, Completed: {IsCompleted}";
    }
}