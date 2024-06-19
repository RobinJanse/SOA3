namespace Domain.Notifications.Interfaces
{
	public interface INotificationObserver
	{
		void Update(string message);
	}
}
