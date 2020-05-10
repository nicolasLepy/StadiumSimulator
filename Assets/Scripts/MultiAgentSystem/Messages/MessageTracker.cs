using System;
using System.Collections.Generic;

namespace MultiAgentSystem
{
    public class MessageTracker : IObservable<Message>
    {
        private List<IObserver<Message>> _observers;

        public MessageTracker()
        {
            _observers = new List<IObserver<Message>>();
        }

        public IDisposable Subscribe(IObserver<Message> observer)
        {
            if (!_observers.Contains(observer))
                _observers.Add(observer);
            return new Unsubscriber(_observers, observer);
        }

        public void TrackMessage(Nullable<Message> msg)
        {
            foreach (var observer in _observers)
            {
                if (!msg.HasValue)
                    observer.OnError(new MessageUnknownException());
                else
                {
                    observer.OnNext(msg.Value);
                }
            }
        }

        public void EndTransmission()
        {
            foreach (var observer in _observers.ToArray())
            {
                if(_observers.Contains(observer))
                    observer.OnCompleted();
            }
            _observers.Clear();
        }
        
    }
}