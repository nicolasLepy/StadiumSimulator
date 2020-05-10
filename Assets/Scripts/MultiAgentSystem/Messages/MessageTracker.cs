using System;
using System.Collections.Generic;

namespace MultiAgentSystem
{
    /// <summary>
    /// Track messages feed
    /// </summary>
    public class MessageTracker : IObservable<Message>
    {
        /// <summary>
        /// Agents following the feed
        /// </summary>
        private List<IObserver<Message>> _observers;

        public MessageTracker()
        {
            _observers = new List<IObserver<Message>>();
        }

        /// <summary>
        /// Add an agent to follow the feed
        /// </summary>
        /// <param name="observer">The agent</param>
        /// <returns>A Unsubscriber to allow agent to unfollow the feed</returns>
        public IDisposable Subscribe(IObserver<Message> observer)
        {
            if (!_observers.Contains(observer))
                _observers.Add(observer);
            return new Unsubscriber(_observers, observer);
        }

        /// <summary>
        /// Broadcast a message
        /// </summary>
        /// <param name="msg">The message to broadcast</param>
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

        /// <summary>
        /// Shutdown the feed and remove all agents attached to the tracker
        /// </summary>
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