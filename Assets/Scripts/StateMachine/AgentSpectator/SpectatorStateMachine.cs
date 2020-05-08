using MultiAgentSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiAgentSystem
{
    public class SpectatorStateMachine : StateMachine
    {

        public SpectatorStateMachine(AgentSpectator agent) : base(agent)
        {
            _current = new StateGoToTicketOffice(this);
        }
    }
}
