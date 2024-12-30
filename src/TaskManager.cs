public class TaskManager{
    //Singleton instance
    private static TaskManager? _instance;
    public static TaskManager Instance => _instance ??= new TaskManager();
    
    //Properties
    private List<Task> tasks;
    private List<Team> teams;

    //Constructor
    private TaskManager(){
        tasks = new List<Task>();
        teams = new List<Team>();
    }
    
    //Create and assign a task to a team
    public Task CreateTask(string title, Team assignedTeam, DateTime deadline, Task.TaskPriority priority){
        Task task = new Task(title, assignedTeam, deadline, priority);
        tasks.Add(task);
        return task;
    }

    //Add a team
    public void AddTeam(Team team){
        if(teams.Exists(t => t.Name.Equals(team.Name, StringComparison.OrdinalIgnoreCase)))
        {
            throw new ArgumentException($"A team named '{team.Name}' already exists!");
        }
        teams.Add(team);
    }

    public IEnumerable<Task> GetTasksByTeam(Team team)
    {
        return tasks.Where(t => t.AssignedTeam == team);
    }

    public IEnumerable<Task> GetTasksByPriority(Task.TaskPriority priority)
    {
        return tasks.Where(t => t.Priority == priority);
    }

    public Task.TaskStats GetTaskStats(){
        int completedTasks = tasks.Count(t => t.IsCompleted);
        return new Task.TaskStats(tasks.Count, completedTasks);
    }

    public void DisplayReports(IEnumerable<IReportable> reportables){
        foreach (var reportable in reportables)
        {
            Console.WriteLine(reportable.GenerateReport());
        }
    }
    
}