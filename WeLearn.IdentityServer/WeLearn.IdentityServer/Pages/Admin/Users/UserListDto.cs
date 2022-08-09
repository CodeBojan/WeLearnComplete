namespace WeLearn.IdentityServer.Pages.Admin.Users;

public class UserListItemDto
{
    public string Id { get; set; }
    public string Email { get; set; }
    public string Username { get; set; }
    public DateTime CreatedDate { get; set; }
    public bool Approved { get; set; }
}
