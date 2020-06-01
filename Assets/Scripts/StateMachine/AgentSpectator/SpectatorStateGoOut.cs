using UnityEngine;
using UnityEngine.AI;

namespace MultiAgentSystem
{
    /// <summary>
    /// State to make the spectator go out of the stadium
    /// </summary>
    public class SpectatorStateGoOut : State
    {
        private Vector3 _destination;
        
        public SpectatorStateGoOut(StateMachine stateMachine) : base(stateMachine)
        {
            _destination = (stateMachine.Agent as AgentSpectator).spawnLocation;
            //Put in constructor for performance issue
            this._stateMachine.Agent.Body.MoveToDestination(_destination);
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