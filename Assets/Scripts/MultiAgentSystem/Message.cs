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

    public interface MessageType
    {

    }
}
