﻿using System.Collections.Generic;
using UnityEngine;

namespace MultiAgentSystem
{
    /// <summary>
    /// State to make the spectator go out of the stadium
    /// </summary>
    public class SpectatorStateEnterStadium : State
    {
        private Vector3 _destination;

        private AgentSecurity _target;
        private AgentSpectator _himSelf;
        
        public SpectatorStateEnterStadium(StateMachine stateMachine) : base(stateMachine)
        {
            _himSelf = stateMachine.Agent as AgentSpectator;
            List<AgentSecurity> securityAgents =
                (_stateMachine.Agent.Body as AgentSpectatorBody).securityInLineOfVision;

            int minAgents = -1;
            AgentSecurity selectedSecurity = null;
            foreach (AgentSecurity ags in securityAgents)
            {
                int agents = ags.queue.agents.Count;
                if (agents < minAgents || minAgents == -1)
                {
                    selectedSecurity = ags;
                    minAgents = agents;
                }
            }
            _target = selectedSecurity;
            _stateMachine.Agent.SendMessage(_target, new MessageAskForQueue());
        }

        public override void Action()
        {
            AgentSpectator spectator = _stateMachine.Agent as AgentSpectator;
            if (spectator.inQueue)
            {
                _stateMachine.Agent.Body.MoveToDestination(spectator.queuePosition);
            }
            else if(_himSelf.isChecked)
            {
                _stateMachine.Agent.Body.MoveToDestination(spectator.ticket.position);
            }
        }

        public override State Next()
        {
            State res = this;
            if (_himSelf.isChecked)
            {
                res = new SpectatorSitInTribune(_stateMachine);
            }
            return res;
        }
    }
}