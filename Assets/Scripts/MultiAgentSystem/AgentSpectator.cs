using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace MultiAgentSystem
{
    /// <summary>
    /// Represents a spectator
    /// </summary>
    public class AgentSpectator : Agent
    {

        public override Vector3 Position
        {
            get
            {
                return _body.transform.position;
            }
        }

        /// <summary>
        /// Create an agent spectator
        /// </summary>
        /// <param name="name">Name of the agent</param>
        public AgentSpectator(string name) : base(name)
        {
            _stateMachine = new SpectatorStateMachine(this);
        }

        public AgentSpectator() : this("Agent") {
            
        }

        public override void CreateBody()
        {
            CreateBody("SpectatorBody");
        }

        /// <summary>
        /// Temp : il faut gérer le champ de vision de l'agent
        /// </summary>
        /// <returns></returns>
        public AgentTicketOffice ClosestTicketOffice()
        {
            AgentTicketOffice res = null;
            foreach(Agent a in Environment.GetInstance().Brain.Agents)
            {
                if(a as AgentTicketOffice != null)
                {
                    res = a as AgentTicketOffice;
                }
            }
            return res;
        }
    }
}
