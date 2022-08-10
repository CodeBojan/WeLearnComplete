﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeLearn.Importers.Services.Notification;

public interface INotificationService
{
    public Task<GetNotificationDto> CreateNotificationAsync(Guid userId, string text, string type);
}