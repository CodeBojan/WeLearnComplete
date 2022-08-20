using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WeLearn.Api.Dtos.Notification;
using WeLearn.Importers.Services.Notification;
using WeLearn.Shared.Dtos.Paging;

namespace WeLearn.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class NotificationsController : UserAuthorizedController
{
    private readonly INotificationService _notificationService;

    public NotificationsController(INotificationService notificationService)
    {
        _notificationService = notificationService;
    }

    [HttpGet("me")]
    public async Task<ActionResult<PagedResponseDto<GetNotificationDto>>> GetAccountNotificationsAsync([FromQuery] PageOptionsDto pageOptions)
    {
        var dto = await _notificationService.GetAccountNotificationsAsync(UserId, pageOptions);
        return Ok(dto);
    }
}
