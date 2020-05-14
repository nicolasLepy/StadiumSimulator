using UnityEngine;
using UnityEngine.AI;

namespace MultiAgentSystem
{
    /// <summary>
    /// The spectator follow a queue for something (ticket,...)
    /// </summary>
    public class StateSpectatorFollowQueue : State
    {
        
        public StateSpectatorFollowQueue(StateMachine stateMachine) : base(stateMachine)
        {
        }

        public override void Action()
        {
            Vector3 target = (_stateMachine.Agent as AgentSpectator).queuePosition;
            this._stateMachine.Agent.Body.GetComponent<NavMeshAgent>().destination = target;
        }

        public override State Next()
        {
            AgentSpectator agent = _stateMachine.Agent as AgentSpectator;
            State res = this;
            if(agent.ticket != null)
                res = new SpectatorStateGoOut(_stateMachine);
            return res;
        }
    }
}