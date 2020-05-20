using AgentSecurity;
using UnityEngine;

namespace MultiAgentSystem
{
    public class AgentSecurity : Agent
    {

        private Queue _queue;
        
        
        public AgentSecurity(string name) : base(name)
        {
        }

        public AgentSecurity() : base("AgentSecurity")
        {
            
        }

        public override void CreateStateMachine()
        {
            _stateMachine = new SecurityStateMachine(this);
        }

        public override Vector3 Position { get; }
        protected override void CreateBody()
        {
            CreateBody<AgentSecurityBody>("SecurityBody");
        }

        public override void ProcessMessage(Message message)
        {
            
        }
    }
}