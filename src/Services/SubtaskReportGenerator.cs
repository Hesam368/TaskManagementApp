using TaskManagementApp.Models;

namespace TaskManagementApp.Services
{
    public class SubtaskReportGenerator : IReportable<Subtask>
    {
        public string GenerateReport(Subtask subtask)
        {
            return $"Subtask: {subtask.Title} (Priority: {subtask.Priority}), Parent Task: {subtask.ParentTask.Title}, Deadline: {subtask.Deadline}, Completed: {subtask.IsCompleted}";
        }
    }
}
