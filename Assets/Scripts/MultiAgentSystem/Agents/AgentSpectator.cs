using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.MultiAgentSystem;
using UnityEngine;

namespace MultiAgentSystem
{
    /// <summary>
    /// Represents a spectator
    /// </summary>
    public class AgentSpectator : Agent
    {

        /// <summary>
        /// If has a ticket or not
        /// </summary>
        public Ticket ticket { get; set; }
        public bool inQueue { get; set; }
        public Vector3 queuePosition { get; set; }

        
        public override Vector3 Position => _body.transform.position;

        /// <summary>
        /// Create an agent spectator
        /// </summary>
        /// <param name="name">Name of the agent</param>
        private AgentSpectator(string name) : base(name)
        {
        }

        public override void CreateStateMachine()
        {
            _stateMachine = new SpectatorStateMachine(this);
        }
        
        private static int nameCounter = 0;
        public AgentSpectator() : this("AgentSpectator_" + ++nameCounter) {
            
        }

        /// <summary>
        /// Read mailbox of the agent
        /// </summary>
        public override void ReadMailbox()
        {
            foreach (Message m in _mailbox)
            {
                Debug.Log(this + " received " + m.Type + " from " + m.Sender);
                switch (m.Type.messageObject())
                {
                    //Spectator get ticket office queue position
                    case MessageObject.GET_QUEUE_POSITION:
                        MessageSendQueuePosition msg = m.Type as MessageSendQueuePosition;
                        inQueue = true;
                        queuePosition = msg.position;
                        break;
                    //A ticket was given by the ticket office
                    case MessageObject.GIVE_TICKET:
                        MessageGiveTicket mgt = m.Type as MessageGiveTicket;
                        ticket = mgt.ticket;
                        break;
                    //There is no available ticket anymore
                    case MessageObject.NO_TICKET_AVAILABLE:
                        //ticket is set to true only to make the agent move out the ticket office
                        ticket = new Ticket(1); //Set no ticket behaviour
                        break;
                }
            }

            ClearMailbox();

            if (_stateMachine.current is SpectatorChoseStadiumEntrance)
            {
                Material blue = Resources.Load("Materials/Blue", typeof(Material)) as Material;
                _body.gameObject.GetComponent<Renderer>().material = blue;
            }
            if (_stateMachine.current is StateGoToTicketOffice)
            {
                Material blue = Resources.Load("Materials/Green", typeof(Material)) as Material;
                _body.gameObject.GetComponent<Renderer>().material = blue;
            }
            if (_stateMachine.current is StateSpectatorFollowQueue)
            {
                Material blue = Resources.Load("Materials/Grey", typeof(Material)) as Material;
                _body.gameObject.GetComponent<Renderer>().material = blue;
            }
            if (_stateMachine.current is SpectatorStateGoOut)
            {
                Material blue = Resources.Load("Materials/Red", typeof(Material)) as Material;
                _body.gameObject.GetComponent<Renderer>().material = blue;
            }
        }

        
        protected override void CreateBody()
        {
            CreateBody<AgentSpectatorBody>("SpectatorBody");
        }
        

        /// <summary>
        /// Temp : il faut gérer le champ de vision de l'agent
        /// </summary>
        /// <returns></returns>
        public AgentTicketOffice ClosestTicketOffice()
        {
            AgentTicketOffice res = null;
            float minDistance = -1;
            foreach (KeyValuePair<Guid,Agent> agent in Environment.GetInstance().Brain.Agents)
            {
                AgentTicketOffice ato = agent.Value as AgentTicketOffice;
                if (ato != null)
                {
                    float distance = Vector3.Distance(ato.Position, this.Position);
                    if (minDistance > distance || minDistance == -1)
                    {
                        res = ato;
                        minDistance = distance;
                    }
                }
            }
            return res;
        }
    }
}
