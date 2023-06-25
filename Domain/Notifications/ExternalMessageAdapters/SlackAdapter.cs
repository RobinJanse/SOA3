﻿using Domain.Developers;
using Domain.Notifications;
using Domain.Notifications.ExternalMessageServices;
using System;

namespace Domain.MessageServices
{
    public class SlackMessageAdapter : INotificatorService
    {
        public void SendNotification(string message , Developer developer)
        {
            Console.WriteLine($"Slack message: {message} send to {developer.GetName()}");
        }
    }
}