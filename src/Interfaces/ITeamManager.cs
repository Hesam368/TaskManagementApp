using TaskManagementApp.Models;

namespace TaskManagementApp.Interfaces
{
    public interface ITeamManager
    {
        Team CreateTeam(string name);
        TeamMember CreateTeamMember(string name, TeamMember.RoleType role);
        List<Team> GetAllTeams();
    }
}
