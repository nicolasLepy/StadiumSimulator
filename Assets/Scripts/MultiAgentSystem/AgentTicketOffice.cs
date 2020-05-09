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
        /// <summary>
        /// Create a ticket office agent
        /// </summary>
        /// <param name="name">Name of the agent</param>
        private AgentTicketOffice(string name) : base(name)
        {
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
                if (m.Type == MessageType.ASK_FOR_TICKET)
                {
                    askForTicket = m;
                    receivedAskForTicket = true;
                }
            }
            _mailbox.Clear();
        }

        protected override void CreateBody()
        {
            CreateBody<AgentTicketOfficeBody>("TicketOfficeBody");
        }

        public Message askForTicket { get; set; }
        public bool receivedAskForTicket { get; set; }
        
        public override void OnNext(Message value)
        {
            if (value.Receiver == this || value.Receiver == null)
            {
                _mailbox.Add(value);
            }

        }

    }
}
