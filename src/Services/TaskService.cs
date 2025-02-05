using TaskManagementApp.Interfaces;
using TaskManagementApp.Models;

namespace TaskManagementApp.Services
{
    public class TaskService : ITaskService
    {
        // Filter tasks by priority
        public List<WorkTask> FilterByPriority(List<WorkTask> tasks, WorkTask.TaskPriority priority)
        {
            return tasks.Where(t => t.Priority == priority).ToList();
        }

        // Sort tasks by deadline
        public List<WorkTask> SortByDeadline(List<WorkTask> tasks)
        {
            return tasks.OrderBy(t => t.Deadline).ToList();
        }

        // Calculate completion percentage
        public double CalculateCompletionPercentage(List<WorkTask> tasks)
        {
            if (tasks.Count == 0)
            {
                return 0;
            }

            int completedTasks = tasks.Count(t => t.IsCompleted);
            return (double)completedTasks / tasks.Count * 100;
        }
    }
}
