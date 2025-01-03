using TaskManagementApp.Models;

namespace TaskManagementApp.Utilities{
    public static class TaskHelper{
        
        // Filter tasks by priority
        public static List<WorkTask> FilterByPriority(List<WorkTask> tasks, WorkTask.TaskPriority priority){
            return tasks.Where(t => t.Priority == priority).ToList();
        }

        // Sort tasks by deadline
        public static List<WorkTask> SortByDeadline(List<WorkTask> tasks){
            return tasks.OrderBy(t => t.Deadline).ToList();
        }

        // Calculate completion percentage
        public static double CalculateCompletionPercentage(List<WorkTask> tasks){
            if (tasks.Count == 0)
            {
                return 0;
            }
            
            int completedTasks = tasks.Count(t => t.IsCompleted);
            return (double)completedTasks / tasks.Count * 100;
        }
    }
}