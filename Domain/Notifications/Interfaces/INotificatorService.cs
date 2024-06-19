using Domain.Developers;

namespace Domain.Notifications.Interfaces
{
	public interface INotificatorService
	{
		void SendNotification(string message, Developer developer);
	}
}
