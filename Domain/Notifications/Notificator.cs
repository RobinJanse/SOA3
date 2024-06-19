using Domain.Developers;
using Domain.Notifications.Interfaces;

namespace Domain.Notifications
{
	public class Notificator : INotificationObserver
	{
		private readonly Developer _developer;

		//to check if it is running with tests
		private int messagesSend = 0;

		public Notificator(Developer developer)
		{
			_developer = developer;
		}

		public int GetMessagesSend()
		{
			return messagesSend;
		}

		public void Update(string message)
		{
			messagesSend++;
			_developer.SendNotification($"Hey {_developer} your backlogItem has changed fase to {message}");
		}
	}
}
