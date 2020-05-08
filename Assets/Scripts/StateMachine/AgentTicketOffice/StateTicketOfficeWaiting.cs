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
            return this;
        }
    }
}