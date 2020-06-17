namespace MultiAgentSystem
{
    public class SpectatorExitStadium : State
    {

        public SpectatorExitStadium(StateMachine stateMachine) : base(stateMachine)
        {

            AgentSpectator spectator = stateMachine.Agent as AgentSpectator;
            if (spectator != null)
            {
                spectator.Body.MoveToDestination(spectator.spawnLocation);
            }
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