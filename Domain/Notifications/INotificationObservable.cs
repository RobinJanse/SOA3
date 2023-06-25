using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Notifications
{
    public interface INotificationObservable
    {
        void Register(INotificationObserver observer);
        void UnRegister(INotificationObserver observer);

        void Notify(string message);
    }
}
