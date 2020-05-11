using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UIElements;

namespace MultiAgentSystem
{
    /// <summary>
    /// Represents a ticket office
    /// </summary>
    public class AgentTicketOffice : Agent
    {
        
        public Message askForTicket { get; set; }
        public bool receivedAskForTicket { get; set; }

        /// <summary>
        /// The queue in front of the ticket office
        /// </summary>
        private Queue _queue;

        public Queue queue => _queue;

        
        /// <summary>
        /// Create a ticket office agent
        /// </summary>
        /// <param name="name">Name of the agent</param>
        private AgentTicketOffice(string name) : base(name)
        {
            _queue = new Queue(this);
        }

        public override Vector3 Position => _body.transform.Find("Counter").position;

        public AgentTicketOffice() : this("AgentTicketOffice")
        {
            _stateMachine = new TicketOfficeStateMachine(this);
        }

        public override void ReadMailbox()
        {
            foreach (Message m in _mailbox)
            {
                Debug.Log(this + " received " + m.Type + " from " + m.Sender);
                switch (m.Type.messageObject())
                {
                    case MessageObject.ASK_FOR_TICKET:
                        askForTicket = m;
                        receivedAskForTicket = true;
                        break;
                    case MessageObject.ASK_FOR_QUEUE:
                        _queue.Add(m.Sender);
                        MessageType answer = new MessageSendQueuePosition(_queue.GetPositionForAgent(m.Sender));
                        SendMessage(m.Sender, answer);
                        break;
                }
            }
            _mailbox.Clear();
        }

        protected override void CreateBody()
        {
            CreateBody<AgentTicketOfficeBody>("TicketOfficeBody");
            
        }



    }
}
