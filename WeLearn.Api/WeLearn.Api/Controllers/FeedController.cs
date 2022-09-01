using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WeLearn.Api.Dtos.Content;
using WeLearn.Api.Dtos.Feed;
using WeLearn.Api.Services.Feed;
using WeLearn.Auth.Controllers;
using WeLearn.Shared.Dtos.Paging;

namespace WeLearn.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class FeedController : UserAuthorizedController
{
    private readonly IFeedService _feedService;

    public FeedController(IFeedService feedService)
    {
        _feedService = feedService;
    }

    [HttpGet]
    public async Task<ActionResult<PagedResponseDto<GetContentDto>>> GetUserFeedAsync([FromQuery] PageOptionsDto pageOptions)
    {
        var dto = await _feedService.GetUserFeedAsync(UserId, pageOptions);

        return Ok(dto);
    }
}
