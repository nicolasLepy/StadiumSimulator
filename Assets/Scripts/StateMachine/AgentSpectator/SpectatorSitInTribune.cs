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

        private int isSit;
        
        private Vector3 _destination;
        
        public SpectatorSitInTribune(StateMachine stateMachine) : base(stateMachine)
        {
            isSit = 0;
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
                Environment.GetInstance().Brain.timesToSitInStadium.Add(Time.time - (_stateMachine.Agent as AgentSpectator).SpawnTime);
                isSit = 1;
                _stateMachine.Agent.Deactivate();
            }
        }

        public override State Next()
        {
            State res = this;
            //Prevent one frame because there is one more frame where state machine is called before
            //deactivate the agent, but with no NavMesh so the State must not go the ExitStadium state directly
            if(isSit > 0) res = new SpectatorExitStadium(_stateMachine);
            return res;
        }
    }
}