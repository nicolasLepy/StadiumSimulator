using UnityEngine;

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
            _stateMachine.Agent.Body.MoveToDestination(new Vector3(_destination.x, 2, _destination.z));
        }

        public override void Action()
        {
            float dist = Vector3.Distance(_stateMachine.Agent.Position, _destination);
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