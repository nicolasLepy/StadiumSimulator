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
    
    /// <summary>
    /// The spectator is arrived in front of stadium : now he had to choose his ticket office
    /// </summary>
    public class StateGoToTicketOffice : State
    {
        private AgentTicketOffice _target;
        public StateGoToTicketOffice(StateMachine stateMachine) : base(stateMachine)
        {
            List<GameObject> ticketOffices = _stateMachine.Agent.Body.ListCloseTicketOffice(80);
            AgentTicketOffice selectedTicketOffice = null;
            int minAgents = -1;
            foreach (GameObject go in ticketOffices)
            {
                AgentTicketOffice ato = go.GetComponent<AgentBody>().agent as AgentTicketOffice;
                if (ato != null)
                {
                    int agents = ato.queue.agents.Count;
                    if (agents < minAgents || minAgents == -1)
                    {
                        selectedTicketOffice = ato;
                        minAgents = agents;
                    }
                }
            }

            _target = selectedTicketOffice;
            _stateMachine.Agent.SendMessage(_target, new MessageAskForQueue());
        }
        public override void Action()
        {
            //UnityEngine.Vector3 ato = (_stateMachine.Agent as AgentSpectator).ClosestTicketOffice().Position;
            //this._stateMachine.Agent.Body.GetComponent<NavMeshAgent>().destination = ato;
            this._stateMachine.Agent.Body.GetComponent<NavMeshAgent>().destination = _target.Position;
        }

        public override State Next()
        {
            State res = this;
            AgentSpectator a = _stateMachine.Agent as AgentSpectator;
            if (a.inQueue)
            {
                res = new StateSpectatorFollowQueue(_stateMachine, _target);
            }
            a.inQueue = false;
            return res;
        }
    }
}