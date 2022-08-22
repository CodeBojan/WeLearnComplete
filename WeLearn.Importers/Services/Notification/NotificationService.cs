using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
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

        public Task<GetNotificationDto> CreateNotificationAsync(Guid userId, string text, string type)
        {
            throw new NotImplementedException();
        }

        public async Task<PagedResponseDto<GetNotificationDto>> GetAccountNotificationsAsync(Guid accountId, PageOptionsDto pageOptions)
        {
            var dto = await _dbContext.Notifications
                .AsNoTracking()
                .Where(n => n.ReceiverId == accountId)
                .OrderByDescending(n => n.UpdatedDate)
                .GetPagedResponseDtoAsync(pageOptions, MapNotificationToGetDto());

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

        private Expression<Func<Data.Models.Notification, GetNotificationDto>> MapNotificationToGetDto()
        {
            return n => n.MapToGetDto();
        }
    }
}
