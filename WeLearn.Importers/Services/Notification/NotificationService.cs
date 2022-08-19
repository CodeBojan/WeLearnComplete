using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeLearn.Data.Persistence;
using WeLearn.Importers.Dtos.Notification;

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
    }
}
