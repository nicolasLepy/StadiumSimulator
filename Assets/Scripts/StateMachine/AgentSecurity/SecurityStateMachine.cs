using MultiAgentSystem;

namespace AgentSecurity
{
    public class SecurityStateMachine : StateMachine
    {
        public SecurityStateMachine(MultiAgentSystem.AgentSecurity agent) : base(agent)
        {
            _current = new SecurityCheckTickets(this);
        }
        
        
    }
}