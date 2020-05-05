using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
