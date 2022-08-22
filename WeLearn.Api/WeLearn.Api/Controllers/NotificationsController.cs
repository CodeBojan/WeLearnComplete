using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WeLearn.Api.Dtos.Notification;
using WeLearn.Importers.Dtos.Notification;
using WeLearn.Importers.Exceptions;
using WeLearn.Importers.Services.Notification;
using WeLearn.Shared.Dtos.Paging;
using WeLearn.Shared.Exceptions.Models;

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

    [HttpGet("me/unread")]
    public async Task<ActionResult<GetUnreadNotificationsDto>> GetUnreadNotificationsCountAsync()
    {
        var count = await _notificationService.GetUnreadNotificationsCountAsync(UserId);
        return Ok(new GetUnreadNotificationsDto { Unread = count });
    }

    [HttpPost("{notificationId}/read")]
    public async Task<IActionResult> PostReadNotificationAsync([FromRoute] Guid notificationId, [FromBody] PostNotificationReadStatusDto postDto)
    {
        if (notificationId != postDto.NotificationId)
            return BadRequest();

        try
        {
            await _notificationService.ReadNotificationAsync(notificationId, UserId, postDto.ReadState);
            return Ok();
        }
        catch (NotificationNotFoundException)
        {
            return NotFound();
        }
        catch (UnauthorizedModelAccessException)
        {
            return NotFound();
        }
    }
}
