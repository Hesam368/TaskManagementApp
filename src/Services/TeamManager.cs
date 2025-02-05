using TaskManagementApp.Interfaces;
using TaskManagementApp.Models;

namespace TaskManagementApp.Services
{
    public class TeamManager : ITeamManager
    {
        private readonly List<Team> _teams = new();

        public Team CreateTeam(string name)
        {
            Team team = Team.Create(name);
            _teams.Add(team);
            return team;
        }

        public TeamMember CreateTeamMember(string name, TeamMember.RoleType role)
        {
            return TeamMember.Create(name, role);
        }

        public List<Team> GetAllTeams()
        {
            return _teams;
        }
    }
}
