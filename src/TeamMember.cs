public class TeamMember{
    public Guid ID{get; private set;}
    public string Name{get; set;}
    public RoleType Role{get; set;}

    public TeamMember(string name, RoleType role){
        if(string.IsNullOrWhiteSpace(name)){throw new ArgumentException("Name of memeber cannot be empty!");}
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