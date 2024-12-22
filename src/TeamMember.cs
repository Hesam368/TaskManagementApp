public class TeamMember{
    //Properties
    public Guid ID{get; private set;}
    public string Name{get; set;}
    public RoleType Role{get; set;}

    //Constructor
    public TeamMember(string name, RoleType role){
        if(string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("Name of memeber cannot be empty!");
        }
        ID = Guid.NewGuid();
        Name = name;
        Role = role;
    }

    public enum RoleType{
        TeamLeader,
        Developer,
        Tester,
        Designer
    }
}