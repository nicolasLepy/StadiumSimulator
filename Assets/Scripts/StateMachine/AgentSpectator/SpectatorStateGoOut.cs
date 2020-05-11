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
            _destination = new Vector3(109,3,-24);
        }

        public override void Action()
        {
            this._stateMachine.Agent.Body.GetComponent<NavMeshAgent>().destination = _destination;
        }

        public override State Next()
        {
            return this;
        }
    }
}