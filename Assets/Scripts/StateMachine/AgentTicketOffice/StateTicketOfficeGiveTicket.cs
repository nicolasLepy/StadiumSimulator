using System.Collections;
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
            _transaction_duration = Environment.GetInstance().settings.timeToBuyTicket * 50; // 50 is the number of frames in 1 sec
            _agent = agent;
            AgentTicketOffice ato = _stateMachine.Agent as AgentTicketOffice;
            ato.transactionFinished = false;
        }
        
        
        public override void Action()
        {
            time++;
            //This timer might be awfully ugly
            if (time > _transaction_duration)
            {
                AgentTicketOffice agent = _stateMachine.Agent as AgentTicketOffice;
                Ticket ticket = Environment.GetInstance().environment.RequestSeat((agent.askForTicket.Type as MessageAskForTicket).door);
                agent.receivedAskForTicket = false;
                if (ticket != null)
                {
                    agent.SendMessage(agent.queue.Pop(), new MessageGiveTicket(ticket));
                    agent.transactionFinished = true;
                    //agent.SendMessage(agent.askForTicket.Sender, new MessageGiveTicket());
                }
                else
                {
                    List<int> availableCategories =
                        Environment.GetInstance().environment.StillAvailableCategories((agent.queue.First() as AgentSpectator).side);
                    agent.SendMessage(agent.queue.First(), new MessageNoTicketAvailable(availableCategories,(agent.askForTicket.Type as MessageAskForTicket).door));
                    //agent.SendMessage(agent.askForTicket.Sender, new MessageNoTicketAvailable());
                    //No seat available, the transaction is finished
                    if (availableCategories.Count == 0)
                    {
                        agent.queue.Pop(); //Eliminate the first agent in the queue
                        agent.transactionFinished = true;
                    }
                }
            
                
            }

        }

        public override State Next()
        {
            AgentTicketOffice agent = _stateMachine.Agent as AgentTicketOffice;
            State res = this;
            if (time > _transaction_duration && agent.transactionFinished)
            {
                //Envoyer la nouvelle position à tout le monde
                foreach (Agent a in agent.queue.agents)
                {
                    agent.SendMessage(a,new MessageSendQueuePosition(agent.queue.GetPositionForAgent(a)));
                }
                res = new StateTicketOfficeWaiting(_stateMachine);
            }
            else if (time > _transaction_duration)
                time = 0;
            return res;
        }
    }
}