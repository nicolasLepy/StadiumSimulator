﻿using System.Collections.Generic;
using UnityEngine;

namespace MultiAgentSystem
{
    /// <summary>
    /// Represents a ticket office
    /// </summary>
    public class AgentTicketOffice : Agent
    {
        public Message askForTicket { get; set; }
        public bool receivedAskForTicket { get; set; }
        
        public bool transactionFinished { get; set; }

        /// <summary>
        /// The queue in front of the ticket office
        /// </summary>
        private Queue _queue;

        public Queue queue => _queue;

        private List<float> _times;

        public List<float> times => _times;

        
        /// <summary>
        /// Create a ticket office agent
        /// </summary>
        /// <param name="name">Name of the agent</param>
        private AgentTicketOffice(string name) : base(name)
        {
            _times = new List<float>();
            _queue = new Queue(this);
        }

        public override Vector3 Position => _body.transform.Find("Counter").position;

        public AgentTicketOffice() : this("AgentTicketOffice")
        {
        }
        
        /// <summary>
        /// Create the state machine managing behaviour of the agent mind
        /// </summary>
        public override void CreateStateMachine()
        {
            _stateMachine = new TicketOfficeStateMachine(this);
        }

        protected override void CreateBody()
        {
            CreateBody<AgentTicketOfficeBody>("TicketOfficeBody");
        }

        public override void ProcessMessage(Message message)
        {
            if(Environment.GetInstance().settings.showMessagesLog) Debug.Log(this + " received " + message.Type + " from " + message.Sender);
            switch (message.Type.messageObject())
            {
                case MessageObject.ASK_FOR_TICKET:
                    askForTicket = message;
                    receivedAskForTicket = true;
                    break;
                case MessageObject.ASK_FOR_QUEUE:
                    _queue.Add(message.Sender);
                    MessageType answer = new MessageSendQueuePosition(_queue.GetPositionForAgent(message.Sender), _queue.GetNumberInQueueForAgent(message.Sender));
                    SendMessage(message.Sender, answer);
                    break;
            }
            archivedMailbox.Add(message);
        }
    }
}
