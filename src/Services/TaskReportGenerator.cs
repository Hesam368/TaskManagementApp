using TaskManagementApp.Models;

namespace TaskManagementApp.Services
{
    //Generate report for the task
    public class TaskReportGenerator : IReportable<WorkTask>
    {
        public virtual string GenerateReport(WorkTask task)
        {
            return $"Task: {task.Title} (Priority: {task.Priority}), Assigned To: {task.AssignedTeam.Name}, Deadline: {task.Deadline}, Completed: {task.IsCompleted}";
        }
    }
}
