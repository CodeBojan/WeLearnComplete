using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using WeLearn.Data.Persistence;
using WeLearn.Importers.Dtos.Notification;
using WeLearn.Importers.Extensions.Models;
using WeLearn.Shared.Dtos.Paging;
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

        private Expression<Func<Data.Models.Notification, GetNotificationDto>> MapNotificationToGetDto()
        {
            return n => n.MapToGetDto();
        }
    }
}
