using System.Collections.Generic;

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
            List<AgentTicketOffice> ticketOfficesAgent =
                (_stateMachine.Agent.Body as AgentSpectatorBody).ticketOfficeInLineOfVision;

            int minAgents = -1;
            AgentTicketOffice selectedTicketOffice = null;
            foreach (AgentTicketOffice ato in ticketOfficesAgent)
            {
                int agents = ato.queue.agents.Count;
                if (agents < minAgents || minAgents == -1)
                {
                    selectedTicketOffice = ato;
                    minAgents = agents;
                }
            }
            
            _target = selectedTicketOffice;
            _stateMachine.Agent.SendMessage(_target, new MessageAskForQueue());
        }
        public override void Action()
        {
            _stateMachine.Agent.Body.MoveToDestination(_target.Position);
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