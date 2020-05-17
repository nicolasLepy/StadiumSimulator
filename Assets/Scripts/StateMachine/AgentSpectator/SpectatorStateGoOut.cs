﻿using UnityEngine;
using UnityEngine.AI;

namespace MultiAgentSystem
{
    /// <summary>
    /// State to make the spectator go out of the stadium
    /// </summary>
    public class SpectatorStateGoOut : State
    {
        private Vector3 _destination;
        
        public SpectatorStateGoOut(StateMachine stateMachine) : base(stateMachine)
        {
            _destination = (stateMachine.Agent as AgentSpectator).spawnLocation;
        }

        public override void Action()
        {
            this._stateMachine.Agent.Body.MoveToDestination(_destination);
        }

        public override State Next()
        {
            return this;
        }
    }
}