using TaskManagementApp.Models;

public class TeamManager : ITeamManager
{
    public Team CreateTeam(string name)
    {
        return Team.Create(name);
    }

    public TeamMember CreateTeamMember(string name, TeamMember.RoleType role)
    {
        return TeamMember.Create(name, role);
    }
}
