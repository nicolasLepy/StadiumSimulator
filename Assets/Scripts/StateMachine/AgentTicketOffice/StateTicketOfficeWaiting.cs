using System;
using UnityEngine;

namespace MultiAgentSystem
{
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
            if (agent != null && agent.receivedAskForTicket)
                res = new StateTicketOfficeGiveTicket(_stateMachine);
            
            return res;
        }
    }
}