using Domain.Backlogs.BacklogItemStates;
using Domain.Developers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Notifications
{
    public class Notificator : INotificationObserver
    {
        private Developer _developer;

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
