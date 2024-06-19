using Domain.Developers;
using Domain.Notifications.Interfaces;
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