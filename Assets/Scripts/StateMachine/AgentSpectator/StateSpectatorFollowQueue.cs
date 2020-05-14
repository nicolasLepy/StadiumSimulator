using UnityEngine;
using UnityEngine.AI;

namespace MultiAgentSystem
{
    /// <summary>
    /// The spectator follow a queue for something (ticket,...)
    /// </summary>
    public class StateSpectatorFollowQueue : State
    {

        private AgentTicketOffice _ticketOffice;
        private bool _askedForTicket;
        
        public StateSpectatorFollowQueue(StateMachine stateMachine, AgentTicketOffice ticketOffice) : base(stateMachine)
        {
            _askedForTicket = false;
            _ticketOffice = ticketOffice;
        }

        public override void Action()
        {
            Vector3 target = (_stateMachine.Agent as AgentSpectator).queuePosition;
            this._stateMachine.Agent.Body.GetComponent<NavMeshAgent>().destination = target;
        }

        public override State Next()
        {
            AgentSpectator agent = _stateMachine.Agent as AgentSpectator;
            State res = this;

            if (!_askedForTicket && _ticketOffice.queue.First() == agent)
            {
                if (Vector3.Distance(agent.Position, _ticketOffice.Position) < 1.7f)
                {
                    agent.SendMessage(_ticketOffice,new MessageAskForTicket(UnityEngine.Random.Range(0,12)));
                    _askedForTicket = true;
                }
            }
            if(agent.ticket != null)
                res = new SpectatorStateGoOut(_stateMachine);
            if (agent.ticketRefused != null)
                res = new SpectatorStateGoOut(_stateMachine);
            return res;
        }
    }
}