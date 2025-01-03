using TaskManagementApp.Models;

namespace TaskManagementApp.Services{
    public class TeamService{
        //Singleton instance
        private static TeamService? _instance;
        public static TeamService Instance => _instance ??= new TeamService();
        
        //Properties
        private readonly List<Team> teams = new();
        public IReadOnlyList<Team> Teams => teams;

        //Constructor
        private TeamService(){
        }
        
        //Add a team
        public void AddTeam(Team team){
            if(teams.Exists(t => t.Name.Equals(team.Name, StringComparison.OrdinalIgnoreCase)))
            {
                throw new ArgumentException($"A team named '{team.Name}' already exists!");
            }
            teams.Add(team);
        }

        //Assign a leader to a team
        public void AssignLeader(Guid teamId, TeamMember teamLeader){
            var team = teams.FirstOrDefault(t => t.ID == teamId) ?? throw new ArgumentException("Team not found!");
            team.AssignTeamLeader(teamLeader);
        }

        public void DisplayReports(IEnumerable<IReportable> reportables){
            foreach (var reportable in reportables)
            {
                Console.WriteLine(reportable.GenerateReport());
            }
        }
    }
}