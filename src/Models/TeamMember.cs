namespace TaskManagementApp.Models
{
    public class TeamMember
    {
        //Properties
        public Guid ID { get; }
        public string Name { get; }
        public RoleType Role { get; }

        //Constructor
        private TeamMember(string name, RoleType role)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Name of memeber cannot be empty!");
            }
            ID = Guid.NewGuid();
            Name = name;
            Role = role;
        }

        public static TeamMember Create(string name, RoleType role)
        {
            return new TeamMember(name, role);
        }

        public enum RoleType
        {
            TeamLeader,
            Developer,
            Tester,
            Designer,
        }
    }
}
