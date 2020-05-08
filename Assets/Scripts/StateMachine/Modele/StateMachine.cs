using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiAgentSystem
{
    public abstract class StateMachine
    {

        protected State _current;

        private Agent _agent;

        public Agent Agent { get => _agent; }

        public StateMachine(Agent agent)
        {
            _agent = agent;
        }

        public void Action()
        {
            if (_current != null)
            {
                _current.Action();
                _current = _current.Next();
            }
        }
    }
}
