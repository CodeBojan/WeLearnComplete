using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeLearn.Importers.Dtos.Notification;
using WeLearn.Shared.Dtos.Paging;

namespace WeLearn.Importers.Services.Notification;

public interface INotificationService
{
    public Task<GetNotificationDto> CreateNotificationAsync(Guid userId, string text, string type);
    Task<PagedResponseDto<GetNotificationDto>> GetAccountNotificationsAsync(Guid accountId, PageOptionsDto pageOptions);
    Task<int> GetUnreadNotificationsCountAsync(Guid userId);
    Task ReadNotificationAsync(Guid notificationId, Guid accountId, bool readState);
}
