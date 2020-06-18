namespace MultiAgentSystem
{
    public class SpectatorStateMachine : StateMachine
    {
        public SpectatorStateMachine(AgentSpectator agent) : base(agent)
        {
            _current = new SpectatorChoseStadiumEntrance(this);
        }
    }
}
