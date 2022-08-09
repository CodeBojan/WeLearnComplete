using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using WeLearn.Data.Models;
using WeLearn.Data.Persistence;
using WeLearn.IdentityServer.Models.DtoExtensions;

namespace WeLearn.IdentityServer.Pages.Admin.Users;

public class IndexModel : PageModel
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly ILogger _logger;
    private readonly ApplicationDbContext _dbContext;

    public IEnumerable<UserListItemDto> UserListDto { get; set; }

    public string UserId { get; set; }

    public IndexModel(
        UserManager<ApplicationUser> userManager,
        ILogger<IndexModel> logger,
        ApplicationDbContext dbContext)
    {
        _userManager = userManager;
        _logger = logger;
        _dbContext = dbContext;
    }

    public async Task OnGetAsync()
    {
        UserListDto = await _dbContext.Users
            .MapToListItemDto()
            .ToListAsync();
    }
}
