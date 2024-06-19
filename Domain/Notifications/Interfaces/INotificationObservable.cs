namespace Domain.Notifications.Interfaces
{
	public interface INotificationObservable
	{
		void Register(INotificationObserver observer);
		void UnRegister(INotificationObserver observer);

		void Notify(string message);
	}
}
