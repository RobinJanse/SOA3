using Domain.Notifications;
using System;
using System.Collections.Generic;

namespace Domain.SCM
{
    public class Branch : INotificationObservable
    {
        private string _name;
        private DateTime _changeDate;
        private Code _code;

        private List<INotificationObserver> _observers;

        public Branch(string name)
        {
            this._name = name;
            this._changeDate = DateTime.Now;
            this._code = new Code("");
            this._observers = new List<INotificationObserver>();
        }


        public string GetName()
        {
            return this._name;
        }

        public DateTime GetChangeDate()
        {
            return this._changeDate;
        }

        public Code GetCode()
        {
            return this._code;
        }

        public void PushCommit(Commit commit)
        {
                this._code = commit.GetCode();
                this.Notify($"new commit pushed has been pushed to {_name}");
                this._changeDate = DateTime.Now;
        }

        public void Register(INotificationObserver observer)
        {
            _observers.Add(observer);
        }

        public void UnRegister(INotificationObserver observer)
        {
            _observers.Remove(observer);
        }

        public void Notify(string message)
        {
           foreach(INotificationObserver observer in _observers)
            {
                observer.Update(message);
            }
        }
    }
}