using System;
using System.Collections.Generic;

namespace MultiAgentSystem
{
    /// <summary>
    /// A class to allow agent to unsubscribe to the tracker
    /// </summary>
    public class Unsubscriber : IDisposable
    {
        
        private List<IObserver<Message>> _observers;
        private IObserver<Message> _observer;

        public Unsubscriber(List<IObserver<Message>> observers, IObserver<Message> observer)
        {
            _observer = observer;
            _observers = observers;
        }
        public void Dispose()
        {
            if (_observer != null && _observers.Contains(_observer))
                _observers.Remove(_observer);
        }
    }
}