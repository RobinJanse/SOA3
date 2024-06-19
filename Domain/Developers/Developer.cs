using Domain.Developers.Enums;
using Domain.Notifications.ExternalMessageServices;
using Domain.Notifications.Interfaces;

namespace Domain.Developers
{
	public class Developer
	{
		private readonly string name;
		private readonly RoleType role;
		private INotificatorService notificatorService;
		public Developer(string name, RoleType role)
		{
			this.name = name;
			this.role = role;
			notificatorService = new EmailAdapter();
		}

		public RoleType GetRole()
		{
			return role;
		}

		public void SendNotification(string message)
		{
			notificatorService.SendNotification(message, this);
		}

		public void setNotificatorService(INotificatorService notificatorService)
		{
			this.notificatorService = notificatorService;
		}

		public string GetName()
		{
			return name;
		}
	}
}
