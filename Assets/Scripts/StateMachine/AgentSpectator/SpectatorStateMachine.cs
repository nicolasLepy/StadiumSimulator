using MultiAgentSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;

namespace MultiAgentSystem
{
    public class SpectatorStateMachine : StateMachine
    {


        
        
        public SpectatorStateMachine(AgentSpectator agent) : base(agent)
        {
            _current = new SpectatorChoseStadiumEntrance(this);
            //_current = new StateGoToTicketOffice(this);
        }
    }
}
