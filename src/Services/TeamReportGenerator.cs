using TaskManagementApp.Models;

namespace TaskManagementApp.Services
{
    //Generate team report
    public class TeamReportGenerator : IReportable<Team>
    {
        public string GenerateReport(Team team)
        {
            string members = string.Join(", ", team.Members.Select(m => m.Name));
            return $"Team: {team.Name}, Members: [{members}]";
        }
    }
}
