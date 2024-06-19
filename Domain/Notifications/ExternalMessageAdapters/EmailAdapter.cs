using Domain.Developers;
using Domain.Notifications.Interfaces;
using System;

namespace Domain.Notifications.ExternalMessageServices
{
	public class EmailAdapter : INotificatorService
	{
		public void SendNotification(string message, Developer developer)
		{
			Console.WriteLine($"Email with message: {message} send to {developer.GetName()}");
		}
	}
}
