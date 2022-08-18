using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WeLearn.Shared.Exceptions.Models;
using WeLearn.Shared.Services.StudyYear;

namespace WeLearn.IdentityServer.Pages.Admin.StudyYears;

public class StudyYearModel : PageModel
{
    private readonly IStudyYearService _studyYearService;

    public StudyYearModel(IStudyYearService studyYearService)
    {
        _studyYearService = studyYearService;
    }

    [BindProperty(SupportsGet = true)]
    public Guid StudyYearId { get; set; }

    public StudyYearViewModel ViewModel { get; set; }

    public async Task<IActionResult> OnGet()
    {
        try
        {
            var dto = await _studyYearService.GetStudyYearAsync(StudyYearId);

            ViewModel = new()
            {
                StudyYear = dto
            };
            return Page();
        }
        catch (StudyYearNotFoundException)
        {
            return RedirectToPage("/Admin/StudyYears/Index");
        }
    }
}
