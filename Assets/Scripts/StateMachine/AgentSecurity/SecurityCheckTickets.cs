using System;
using MultiAgentSystem;
using UnityEngine;

namespace MultiAgentSystem
{
    public class SecurityCheckTickets : State
    {
        
        private int _checkingTime;
        private int _currentTime;
        private AgentSecurity _agent;
        public SecurityCheckTickets(StateMachine stateMachine) : base(stateMachine)
        {
            _checkingTime = 60;
            _currentTime = 0;
            _agent = _stateMachine.Agent as AgentSecurity;

        }

        public override void Action()
        {
            if (_agent.queue.agents.Count > 0 && Vector3.Distance(_agent.Position, _agent.queue.agents[0].Position) < 1f)
            {
                _currentTime++;
                if (_currentTime == _checkingTime)
                {
                    _currentTime = 0;
                    Agent agentChecked = _agent.queue.Pop();
                    agentChecked.SendMessage(agentChecked, new MessageChecked());
                    foreach (Agent a in _agent.queue.agents)
                    {
                        _agent.SendMessage(a, new MessageSendQueuePosition(_agent.queue.GetPositionForAgent(a)));
                    }
                }
            }
        }

        public override State Next()
        {
            return this;
        }
    }
}