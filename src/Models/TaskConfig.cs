namespace TaskManagementApp.Models
{
    public class TaskConfig
    {
        public int MaxSubTasks { get; }

        public TaskConfig(int maxSubTasks)
        {
            this.MaxSubTasks = maxSubTasks;
        }
    }
}
