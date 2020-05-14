using UnityEngine;

namespace MultiAgentSystem
{
    /// <summary>
    /// An agent is front of the ticket office, the ticket office sell a ticket to an agent
    /// </summary>
    public class StateTicketOfficeGiveTicket : State
    {
        
        private int time = 0;
        private Agent _agent;
        
        public StateTicketOfficeGiveTicket(StateMachine stateMachine, Agent agent) : base(stateMachine)
        {
            _agent = agent;
        }

        public override void Action()
        {
            time++;
            //This timer might be awfully ugly
            if (time > 100)
            {
                AgentTicketOffice agent = _stateMachine.Agent as AgentTicketOffice;
                agent.receivedAskForTicket = false;
                Ticket ticket = Environment.GetInstance().environmentTest.RequestSeat(2);
                if (ticket != null)
                {
                    agent.SendMessage(agent.queue.Pop(), new MessageGiveTicket(ticket));
                    //agent.SendMessage(agent.askForTicket.Sender, new MessageGiveTicket());
                }
                else
                {
                    agent.SendMessage(agent.queue.Pop(), new MessageNoTicketAvailable());
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
            if (time > 100)
                res = new StateTicketOfficeWaiting(_stateMachine);
            return res;
        }
    }
}