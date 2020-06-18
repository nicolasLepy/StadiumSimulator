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
            if (agent.receivedAskForTicket)
            {
                Agent firstAgentInQueue = agent.queue.First();
                res = new StateTicketOfficeGiveTicket(_stateMachine,firstAgentInQueue);
            }
            return res;
        }
    }
}