namespace MultiAgentSystem
{
    public abstract class State
    {
        protected StateMachine _stateMachine;

        public State(StateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }

        public abstract void Action();
        public abstract State Next();
    }
}
