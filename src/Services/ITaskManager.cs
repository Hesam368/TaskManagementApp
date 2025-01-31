using TaskManagementApp.Models;

public interface ITaskManager
{
    WorkTask CreateTask(
        string title,
        Team assignedTeam,
        DateTime deadline,
        WorkTask.TaskPriority priority,
        ILogger logger,
        TaskConfig config
    );

    Subtask CreateSubtask(
        string title,
        DateTime deadline,
        WorkTask.TaskPriority priority,
        WorkTask parentTask,
        ILogger logger,
        TaskConfig config
    );
    void CompleteTask(Guid taskId);
    IEnumerable<WorkTask> GetTasksByTeam(Team team);
    IEnumerable<WorkTask> GetTasksByPriority(WorkTask.TaskPriority priority);
    WorkTask.TaskStats GetTaskStats();
}
