using TaskManagementApp.Models;

public interface ITeamManager
{
    Team CreateTeam(string name);
    TeamMember CreateTeamMember(string name, TeamMember.RoleType role);
}
