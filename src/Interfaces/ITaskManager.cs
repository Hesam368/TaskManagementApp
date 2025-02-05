using TaskManagementApp.Models;

namespace TaskManagementApp.Interfaces
{
    public interface ITaskManager
    {
        WorkTask CreateTask(
            string title,
            Team assignedTeam,
            DateTime deadline,
            WorkTask.TaskPriority priority
        );

        Subtask CreateSubtask(
            string title,
            DateTime deadline,
            WorkTask.TaskPriority priority,
            WorkTask parentTask
        );
        void CompleteTask(Guid taskId);
        IEnumerable<WorkTask> GetTasksByTeam(Team team);
        IEnumerable<WorkTask> GetTasksByPriority(WorkTask.TaskPriority priority);
        WorkTask.TaskStats GetTaskStats();
        List<WorkTask> GetAllTasks();
    }
}
