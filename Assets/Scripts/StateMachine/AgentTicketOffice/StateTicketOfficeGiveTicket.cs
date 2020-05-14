using System.Collections.Generic;
using UnityEngine;

namespace MultiAgentSystem
{
    /// <summary>
    /// An agent is front of the ticket office, the ticket office sell a ticket to an agent
    /// </summary>
    public class StateTicketOfficeGiveTicket : State
    {

        private int _transaction_duration;
        private int time = 0;
        private Agent _agent;
        
        public StateTicketOfficeGiveTicket(StateMachine stateMachine, Agent agent) : base(stateMachine)
        {
            _transaction_duration = 100;
            _agent = agent;
        }

        public override void Action()
        {
            time++;
            //This timer might be awfully ugly
            if (time > _transaction_duration)
            {
                AgentTicketOffice agent = _stateMachine.Agent as AgentTicketOffice;
                Ticket ticket = Environment.GetInstance().environmentTest.RequestSeat((agent.askForTicket.Type as MessageAskForTicket).door);
                agent.receivedAskForTicket = false;
                if (ticket != null)
                {
                    agent.SendMessage(agent.queue.Pop(), new MessageGiveTicket(ticket));
                    //agent.SendMessage(agent.askForTicket.Sender, new MessageGiveTicket());
                }
                else
                {
                    List<int> availableCategories =
                        Environment.GetInstance().environmentTest.StillAvailableCategories();
                    agent.SendMessage(agent.queue.Pop(), new MessageNoTicketAvailable(availableCategories));
                    //agent.SendMessage(agent.askForTicket.Sender, new MessageNoTicketAvailable());
                }
            
                //Envoyer la nouvelle position à tout le monde
                foreach (Agent a in agent.queue.agents)
                {
                    agent.SendMessage(a,new MessageSendQueuePosition(agent.queue.GetPositionForAgent(a)));
                }
            }

        }

        public override State Next()
        {
            State res = this;
            if (time > _transaction_duration)
                res = new StateTicketOfficeWaiting(_stateMachine);
            return res;
        }
    }
}