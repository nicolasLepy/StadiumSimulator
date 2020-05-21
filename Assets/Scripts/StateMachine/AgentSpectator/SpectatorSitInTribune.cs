using UnityEditor;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Assertions.Must;

namespace MultiAgentSystem
{
    /// <summary>
    /// State to make the spectator go out of the stadium
    /// </summary>
    public class SpectatorSitInTribune : State
    {
        private Vector3 _destination;
        
        public SpectatorSitInTribune(StateMachine stateMachine) : base(stateMachine)
        {
            //First step : chose security agent the less busied
            AgentSpectator spectator = _stateMachine.Agent as AgentSpectator;
            _destination = spectator.ticket.position;
            //_destination = Environment.GetInstance().environment.CategoryPosition(spectator.ticket.door) + Random.insideUnitSphere * 3;
            //_destination = new Vector3(250,3,-117);
            this._stateMachine.Agent.Body.MoveToDestination(_destination);
        }

        public override void Action()
        {
            float dist = Vector3.Distance(_stateMachine.Agent.Position, _destination);
            //this._stateMachine.Agent.Body.GetComponent<NavMeshAgent>().destination = _destination;
            if (dist < 3f)
            {
                Debug.Log("Suicide");
                _stateMachine.Agent.CommitSuicide();
            }
        }

        public override State Next()
        {
            return this;
        }
    }
}