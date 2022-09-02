using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using WeLearn.Data.Models.Content;
using WeLearn.Data.Models.Notifications;
using WeLearn.Data.Persistence;
using WeLearn.Importers.Dtos.Notification;
using WeLearn.Importers.Exceptions;
using WeLearn.Importers.Extensions.Models;
using WeLearn.Shared.Dtos.Paging;
using WeLearn.Shared.Exceptions.Models;
using WeLearn.Shared.Extensions.Paging;

namespace WeLearn.Importers.Services.Notification
{
    public class NotificationService : INotificationService
    {
        private readonly ApplicationDbContext _dbContext;

        public NotificationService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task CreateCommentNotification(string title, string body, string? uri, string? imageUri, Guid receiverId, NotificationOperationType operationType, Guid commentId, bool commit)
        {
            var notif = new CommentNotification(title, body, false, uri, imageUri, operationType.Value(), receiverId, commentId);
            await AddNotification(commit, notif);
        }

        public async Task CreateContentNotificationAsync(string title, string body, string? uri, string? imageUri, Guid receiverId, NotificationOperationType operationType, Guid contentId, bool commit)
        {
            var notif = new ContentNotification(title, body, false, uri, imageUri, operationType.Value(), receiverId, contentId);
            await AddNotification(commit, notif);
        }

        private async Task AddNotification(bool commit, Data.Models.Notification notif)
        {
            _dbContext.Add(notif);
            if (commit)
                await _dbContext.SaveChangesAsync();
        }

        public async Task<PagedResponseDto<GetNotificationDto>> GetAccountNotificationsAsync(Guid accountId, PageOptionsDto pageOptions)
        {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            // TODO add content thenincludes and props for them in the get dto
            var dto = await _dbContext.Notifications
                .AsNoTracking()
                .Include(n => (n as ContentNotification).Content)
                .Include(n => (n as CommentNotification).Comment)
                    .ThenInclude(c => c.Content)
                .Where(n => n.ReceiverId == accountId)
                .OrderByDescending(n => n.IsRead ? 0 : 1)
                .ThenByDescending(n => n.IsRead ? n.CreatedDate : n.UpdatedDate)
                .GetPagedResponseDtoAsync(pageOptions, MapNotificationToGetDto());
#pragma warning restore CS8602 // Dereference of a possibly null reference.

            return dto;
        }

        public async Task<int> GetUnreadNotificationsCountAsync(Guid userId)
        {
            var unread = await _dbContext.Notifications
                .AsNoTracking()
                .Where(n => n.ReceiverId == userId && n.IsRead == false)
                .CountAsync();

            return unread;
        }

        public async Task ReadNotificationAsync(Guid notificationId, Guid accountId, bool readState)
        {
            var notification = await _dbContext.Notifications.FirstOrDefaultAsync(n => n.Id == notificationId);
            if (notification is null)
                throw new NotificationNotFoundException();

            if (notification.ReceiverId != accountId)
                throw new UnauthorizedModelAccessException();

            notification.IsRead = readState;
            await _dbContext.SaveChangesAsync();
        }

        private static Expression<Func<Data.Models.Notification, GetNotificationDto>> MapNotificationToGetDto()
        {
            return n => n.MapToGetDto();
        }
    }
}
