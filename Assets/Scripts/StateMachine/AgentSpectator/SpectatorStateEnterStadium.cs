﻿using UnityEngine;
using UnityEngine.AI;

namespace MultiAgentSystem
{
    /// <summary>
    /// State to make the spectator go out of the stadium
    /// </summary>
    public class SpectatorStateEnterStadium : State
    {
        private Vector3 _destination;
        
        public SpectatorStateEnterStadium(StateMachine stateMachine) : base(stateMachine)
        {
            _destination = new Vector3(250,3,-117);
        }

        public override void Action()
        {
            this._stateMachine.Agent.Body.MoveToDestination(_destination);
            //this._stateMachine.Agent.Body.GetComponent<NavMeshAgent>().destination = _destination;
        }

        public override State Next()
        {
            return this;
        }
    }
}