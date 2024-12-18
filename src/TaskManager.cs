public delegate void TaskCompletedHandler(Task task);
public class TaskManager{
    private static TaskManager _instance;
    public static TaskManager Instance => _instance ??= new TaskManager();
    
    List<Task> tasks;
    List<Team> teams;
    Dictionary<Team, List<Task>> teamTaskMapping;
    private TaskManager(){
        tasks = new List<Task>();
        teams = new List<Team>();
        teamTaskMapping = new Dictionary<Team, List<Task>>();
    }

    public event TaskCompletedHandler OnTaskCompleted;
    public void MarkTaskAsCompleted(Task task){
        task.MarkAsCompleted();
        OnTaskCompleted?.Invoke(task); //Notify all listeners
    }

    public Task CreateTask(string title, Team assignedTeam, DateTime deadline, Task.TaskPriority priority){
        Task task = new Task(title, assignedTeam, deadline, priority);
        tasks.Add(task);
        Console.WriteLine($"Task '{title}' created and assigned to team '{assignedTeam.Name}'.");
        return task;
    }

    public void AddTeam(Team team){
        if(teams.Exists(t => t.Name.Equals(team.Name, StringComparison.OrdinalIgnoreCase))){
            throw new ArgumentException($"A team named '{team.Name}' already exists!");
        }
        teams.Add(team);
    }

    public IEnumerable<Task> GetTasksByTeam(Team team){
        return tasks.Where(t => t.AssignedTeam == team);
    }

    public IEnumerable<Task> GetTasksByPriority(Task.TaskPriority priority){
        return tasks.Where(t => t.Priority == priority);
    }

    public void PrintTeamTaskMatrix(){
        Console.WriteLine("\n--- Team-Task Matrix ---");
        string[,] TTMat = new string[teams.Count, tasks.Count];
        for (int i = 0; i < teams.Count; i++){
            for (int j = 0; j < tasks.Count; j++){
                TTMat[i,j] = tasks[j].AssignedTeam == teams[i] ? "✔" : "✘";
            }
        }

        for (int i = 0; i < teams.Count; i++){
            Console.Write($"{teams[i].Name,-20} ");
            for (int j = 0; j < tasks.Count; j++)
            {
                Console.Write($"{TTMat[i, j]} ");
            }
            Console.WriteLine();
        }
    }

    public void AssignTaskToTeam(Task task, Team team){
        if(!teamTaskMapping.ContainsKey(team)){
            teamTaskMapping[team] = new List<Task>();
        }
        teamTaskMapping[team].Add(task);
        task.AssignedTeam = team;
    }

    public Task.TaskStats GetTaskStats(){
        int completedTasks = tasks.Count(t => t.IsCompleted);
        return new Task.TaskStats(tasks.Count, completedTasks);
    }

    public List<Task> GetTasksForTeam(Team team){
        if (teamTaskMapping.TryGetValue(team, out List<Task> tasks)){
            return tasks;
        }
        return new List<Task>(); //Return empty list if no tasks are mapped to this team
    }
}