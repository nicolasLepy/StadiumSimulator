using MultiAgentSystem;

namespace AgentSecurity
{
    public class SecurityCheckTickets : State
    {
        public SecurityCheckTickets(StateMachine stateMachine) : base(stateMachine)
        {
        }

        public override void Action()
        {
            
        }

        public override State Next()
        {
            return this;
        }
    }
}