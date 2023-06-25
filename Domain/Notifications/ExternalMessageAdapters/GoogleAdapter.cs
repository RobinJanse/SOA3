using Domain.Developers;
using System;

namespace Domain.Notifications.ExternalMessageServices
{
    public class GoogleAdapter : INotificatorService
    {
        public void SendNotification(string message, Developer developer)
        {
            Console.WriteLine($"Google message: {message} send to {developer.GetName()}");
        }
    }
}