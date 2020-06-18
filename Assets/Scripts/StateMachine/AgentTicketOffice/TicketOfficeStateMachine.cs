namespace MultiAgentSystem
{
    public class TicketOfficeStateMachine : StateMachine
    {
        public TicketOfficeStateMachine(AgentTicketOffice agent) : base(agent)
        {
            _current = new StateTicketOfficeWaiting(this);
        }
    }
}
