using TaskManagementApp.Models;

namespace TaskManagementApp.Interfaces
{
    public interface ITaskService
    {
        List<WorkTask> FilterByPriority(List<WorkTask> tasks, WorkTask.TaskPriority priority);
        List<WorkTask> SortByDeadline(List<WorkTask> tasks);
        double CalculateCompletionPercentage(List<WorkTask> tasks);
    }
}
