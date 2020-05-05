using MultiAgentSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiAgentSystem
{
    public class TicketOfficeStateMachine : StateMachine
    {
        public TicketOfficeStateMachine(AgentTicketOffice agent) : base(agent)
        {

        }
    }
}
