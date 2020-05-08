using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;

namespace MultiAgentSystem
{
    [ObsoleteAttribute("This property is temporary, only for test. Will be replaced by real agent behaviour.", false)] 
    public class StateGoToTicketOffice : State
    {

        public StateGoToTicketOffice(StateMachine stateMachine) : base(stateMachine)
        {
            
        }
        public override void Action()
        {
            UnityEngine.Vector3 ato = (_stateMachine.Agent as AgentSpectator).ClosestTicketOffice().Position;
            this._stateMachine.Agent.Body.GetComponent<NavMeshAgent>().destination = ato;
        }

        public override State Next()
        {
            return this;
        }
    }
}
