using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeLearn.Data.Models.Notifications;
using WeLearn.Importers.Dtos.Notification;
using WeLearn.Shared.Dtos.Paging;

namespace WeLearn.Importers.Services.Notification;

public interface INotificationService
{
    Task CreateCommentNotification(string title, string body, string? uri, string? imageUri, Guid receiverId, NotificationOperationType operationType, Guid commentId, bool commit);
    Task CreateContentNotificationAsync(string title, string body, string? uri, string? imageUri, Guid receiverId, NotificationOperationType operationType, Guid contentId, bool commit);
    Task<PagedResponseDto<GetNotificationDto>> GetAccountNotificationsAsync(Guid accountId, PageOptionsDto pageOptions);
    Task<int> GetUnreadNotificationsCountAsync(Guid userId);
    Task ReadNotificationAsync(Guid notificationId, Guid accountId, bool readState);
}
