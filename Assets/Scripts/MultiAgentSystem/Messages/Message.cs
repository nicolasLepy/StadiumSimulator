using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

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

        public override string ToString()
        {
            return "To : " + _receiver.Name + ". Subject : " + _type.ToString();
        }
    }

    /// <summary>
    /// Type of message
    /// Maybe replace this with an interface is embedding other information is necessary
    /// </summary>
    public interface MessageType
    {
        MessageObject messageObject();
    }

    public enum MessageObject
    {
        ASK_FOR_TICKET,
        GIVE_TICKET,
        NO_TICKET_AVAILABLE,
        ASK_FOR_QUEUE,
        GET_QUEUE_POSITION,
        IN_THE_QUEUE
    }

    public class MessageAskForTicket : MessageType
    {
        private int _door;
        public int door => _door;

        public MessageAskForTicket(int door)
        {
            _door = door;
        }
        
        public MessageObject messageObject()
        {
            return MessageObject.ASK_FOR_TICKET;
        }
    }
    
    public class MessageGiveTicket : MessageType
    {
        private Ticket _ticket;
        public Ticket ticket => _ticket;

        public MessageGiveTicket(Ticket ticket)
        {
            _ticket = ticket;
        }
        public MessageObject messageObject()
        {
            return MessageObject.GIVE_TICKET;
        }
    }
    
    public class MessageNoTicketAvailable : MessageType
    {
        public MessageObject messageObject()
        {
            return MessageObject.NO_TICKET_AVAILABLE;
        }
    }
    
    public class MessageAskForQueue : MessageType
    {
        public MessageObject messageObject()
        {
            return MessageObject.ASK_FOR_QUEUE;
        }
    }
    
    /// <summary>
    /// Message to get queue position of an agent (ticket office for example)
    /// </summary>
    public class MessageSendQueuePosition : MessageType
    {
        private Vector3 _position;
        public Vector3 position => _position;
        public MessageSendQueuePosition(Vector3 position)
        {
            _position = position;
        }
        public MessageObject messageObject()
        {
            return MessageObject.GET_QUEUE_POSITION;
        }
    }
    

    
    public class MessageUnknownException : Exception
    {
        internal MessageUnknownException(){}
    }

    
}
