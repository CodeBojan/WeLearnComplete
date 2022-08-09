using Microsoft.AspNetCore.Identity;

namespace WeLearn.Data.Models;

public class ApplicationUser : IdentityUser
{
    public ApplicationUser()
    {
    }

    public ApplicationUser(string username, string email)
    {
        UserName = username;
        Email = email;
    }

    public ApplicationUser(string username, string email, bool approved)
    {
        UserName = username;
        Email = email;
        Approved = approved;
        CreatedDate = DateTime.UtcNow;
    }

    public bool Approved { get; set; }
    public DateTime CreatedDate { get; set; }
}
