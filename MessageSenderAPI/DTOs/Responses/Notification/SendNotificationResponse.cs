﻿using NotificationMessageSender.Core.Common.Domain.Entities;
using NotificationMessageSender.Core.Common.Enums;

namespace NotificationMessageSender.API.DTOs.Responses.Notification
{
    public class SendNotificationResponse
    {
        public List<NotificationEntity> Requests { get; set; }
    }
}
