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
            bool hasTicket = Environment.GetInstance().environmentTest.RequestSeat();
            if (hasTicket)
            {
                agent.SendMessage(agent.askForTicket.Sender, MessageType.GIVE_TICKET);
            }
            else
            {
                agent.SendMessage(agent.askForTicket.Sender, MessageType.NO_TICKET_AVAIABLE);
            }
            return new StateTicketOfficeWaiting(_stateMachine);
        }
    }
}