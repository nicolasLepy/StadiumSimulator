using UnityEngine.AI;

namespace MultiAgentSystem
{
    public class SpectatorExitStadium : State
    {
        private AgentSpectator spectator;
        private bool isMoving;
        public SpectatorExitStadium(StateMachine stateMachine) : base(stateMachine)
        {
            isMoving = false;
            spectator = stateMachine.Agent as AgentSpectator;
        }

        public override void Action()
        {
            if (!isMoving && spectator != null)
            {
                if (Environment.GetInstance().settings.noNavMesh || (!Environment.GetInstance().settings.noNavMesh && spectator.Body.gameObject.GetComponent<NavMeshAgent>() != null))
                {
                    isMoving = true;
                    spectator.Body.MoveToDestination(spectator.spawnLocation);
                }
            }
        }

        public override State Next()
        {
            return this;
        }
    }
}