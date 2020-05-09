using UnityEngine;

namespace MultiAgentSystem
{
    public class StateTicketOfficeGiveTicket : State
    {
        public StateTicketOfficeGiveTicket(StateMachine stateMachine) : base(stateMachine)
        {
        }

        public override void Action()
        {
            
        }

        public override State Next()
        {
            AgentTicketOffice agent = _stateMachine.Agent as AgentTicketOffice;
            agent.receivedAskForTicket = false;
            agent.SendMessage(agent.askForTicket.Sender, MessageType.GIVE_TICKET);
            return new StateTicketOfficeWaiting(_stateMachine);
        }
    }
}