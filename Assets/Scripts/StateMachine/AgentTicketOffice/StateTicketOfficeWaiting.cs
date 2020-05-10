using System;
using UnityEngine;

namespace MultiAgentSystem
{
    /// <summary>
    /// Wait spectators
    /// </summary>
    public class StateTicketOfficeWaiting : State
    {
        public StateTicketOfficeWaiting(StateMachine stateMachine) : base(stateMachine)
        {
        }

        public override void Action()
        {
            //Waiting, do nothing
        }

        public override State Next()
        {
            State res = this;
            AgentTicketOffice agent = _stateMachine.Agent as AgentTicketOffice;
            Agent firstAgentInQueue = agent.queue.First();
            if (firstAgentInQueue != null)
            {
                if (Vector3.Distance(firstAgentInQueue.Position, agent.Position) < 1.7f)
                {
                    res = new StateTicketOfficeGiveTicket(_stateMachine, firstAgentInQueue);
                }
            }
            /*if (agent != null && agent.receivedAskForTicket)
                res = new StateTicketOfficeGiveTicket(_stateMachine);*/
            
            
            return res;
        }
    }
}