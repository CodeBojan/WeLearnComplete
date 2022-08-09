namespace WeLearn.IdentityServer.Pages.Admin.Users;

public class UserViewModel
{
    public string Id { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public string[] Roles { get; set; }
    public DateTime CreatedDate { get; set; }
    public bool Approved { get; set; }
    public RoleClaim[] RoleClaims { get; set; }
    // TODO add
}

public class RoleClaim
{
    public string Type { get; set; }
    public string Value { get; set; }
}
