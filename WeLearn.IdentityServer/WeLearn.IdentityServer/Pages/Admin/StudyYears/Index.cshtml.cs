using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using WeLearn.Data.Models;
using WeLearn.Shared.Dtos.Paging;
using WeLearn.Shared.Dtos.StudyYear;
using WeLearn.Shared.Exceptions.Models;
using WeLearn.Shared.Services.StudyYear;

namespace WeLearn.IdentityServer.Pages.Admin.StudyYears;

public class IndexModel : PageModel
{
    private readonly IStudyYearService _studyYearService;

    [BindProperty(Name = "pg", SupportsGet = true)]
    public new int? Page { get; set; }
    [BindProperty(SupportsGet = true)]
    public int? Limit { get; set; }

    [Required]
    [BindProperty]
    public PostStudyYearDto Post { get; set; }

    public IndexViewModel ViewModel { get; set; }

    public IndexModel(IStudyYearService studyYearService)
    {
        _studyYearService = studyYearService;
    }

    public async Task<IActionResult> OnGet()
    {
        await InitializeViewModel();

        return Page();
    }

    private async Task InitializeViewModel()
    {
        var studyYearsPage = await _studyYearService.GetStudyYearsAsync(new PageOptionsDto(Page ?? 1, Limit ?? 5));
        ViewModel = new()
        {
            PagedStudyYears = studyYearsPage
        };
    }

    public async Task<IActionResult> OnPostCreate()
    {
        if (!ModelState.IsValid)
        {
            await InitializeViewModel();
            return Page();
        }

        try
        {
            var studyYear = await _studyYearService.CreateStudyYearAsync(Post.ShortName, Post.FullName, Post.Description);
        }
        catch (StudyYearAlreadyExistsException)
        {
            ModelState.AddModelError(nameof(StudyYear), "Study Year already exists");
        }

        await InitializeViewModel();
        return Page();
    }
}
