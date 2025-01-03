namespace TaskManagementApp.Models{
    public class Team : IReportable{
        //Properties
        public Guid ID{get; private set;}
        public string Name{get; set;}
        public TeamMember? TeamLeader {get; private set;}
        public List<TeamMember> Members{get; private set;}

        //Constructor
        public Team(string name){
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Team name cannot be empty!");
            }
            Name = name;
            ID = Guid.NewGuid();
            Members = new List<TeamMember>();
        }

        //Assign team leader
        public void AssignTeamLeader(TeamMember teamLeader){
            if (!Members.Contains(teamLeader))
            {
                throw new ArgumentException("Team leader must be a member of the team!");
            }
            TeamLeader = teamLeader;

        }

        //Add a team member
        public void AddMember(TeamMember member){
            if(Members.Exists(m => m.ID == member.ID))
            {
                throw new ArgumentException($"Team member '{member.Name}' is already part of the team!");
            }
            Members.Add(member);
        }

        //Remove a team member
        public void RemoveMember(Guid memberId)
        {
            TeamMember member = Members.FirstOrDefault(m => m.ID == memberId) ?? throw new InvalidOperationException("Team member not found!");
            Members.Remove(member);

            // If the removed member was the team leader, reset the team leader
            if (TeamLeader == member)
                TeamLeader = null;
        }

        //Generate team report
        public string GenerateReport()
        {
            string members = string.Join(", ", Members.Select(m => m.Name));
            return $"Team: {Name}, Members: [{members}]";
        }
    }
}