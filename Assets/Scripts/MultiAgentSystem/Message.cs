using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiAgentSystem
{

    /// <summary>
    /// Message structure
    /// </summary>
    public struct Message
    {
        /// <summary>
        /// Sender of the message
        /// </summary>
        private Agent _sender;
        /// <summary>
        /// Agent targeted for this message
        /// </summary>
        private Agent _receiver;
        /// <summary>
        /// Type of the message
        /// </summary>
        private MessageType _type;

        public Agent Sender { get => _sender; }
        public Agent Receiver { get => _receiver; }
        public MessageType Type { get => _type; }

        public Message(Agent sender, Agent receiver, MessageType type)
        {
            _sender = sender;
            _receiver = receiver;
            _type = type;
        }
    }

    public enum MessageType
    {
        ASK_FOR_TICKET,
        GIVE_TICKET
    }

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
    public class MessageUnknownException : Exception
    {
        internal MessageUnknownException(){}
    }

    
}
