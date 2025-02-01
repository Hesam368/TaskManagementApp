using TaskManagementApp.Models;

public interface ITaskService
{
    List<WorkTask> FilterByPriority(List<WorkTask> tasks, WorkTask.TaskPriority priority);
    List<WorkTask> SortByDeadline(List<WorkTask> tasks);
    double CalculateCompletionPercentage(List<WorkTask> tasks);
}
