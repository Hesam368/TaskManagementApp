using TaskManagementApp.Models;

namespace TaskManagementApp.Services{
    public class TaskManager{
        //Singleton instance
        private static TaskManager? _instance;
        public static TaskManager Instance => _instance ??= new TaskManager();
        
        //Properties
        private readonly List<WorkTask> tasks = new();
        public IReadOnlyList<WorkTask> Tasks => tasks;

        //Constructor
        private TaskManager(){}
        
        //Create and assign a task to a team
        public WorkTask CreateTask(string title, Team assignedTeam, DateTime deadline, WorkTask.TaskPriority priority){
            WorkTask task = WorkTask.Create(title, assignedTeam, deadline, priority);
            tasks.Add(task);
            return task;
        }

        //Create a subtask
        public Subtask CreateSubtask(string title, DateTime deadline, WorkTask.TaskPriority priority, WorkTask parentTask){
            return Subtask.Create(title, parentTask.AssignedTeam, deadline, priority, parentTask);
        }

        //Complete a task
        public void CompleteTask(Guid taskId){
            var task = tasks.FirstOrDefault(t => t.ID == taskId);
            task?.MarkAsCompleted();
        }

        public IEnumerable<WorkTask> GetTasksByTeam(Team team)
        {
            return tasks.Where(t => t.AssignedTeam == team);
        }

        public IEnumerable<WorkTask> GetTasksByPriority(WorkTask.TaskPriority priority)
        {
            return tasks.Where(t => t.Priority == priority);
        }

        public WorkTask.TaskStats GetTaskStats(){
            int completedTasks = tasks.Count(t => t.IsCompleted);
            return new WorkTask.TaskStats(tasks.Count, completedTasks);
        }

        public void DisplayReports(IEnumerable<IReportable> reportables){
            foreach (var reportable in reportables)
            {
                Console.WriteLine(reportable.GenerateReport());
            }
        }
        
    }   
}