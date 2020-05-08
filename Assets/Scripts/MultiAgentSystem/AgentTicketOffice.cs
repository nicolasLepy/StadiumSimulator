using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace MultiAgentSystem
{
    /// <summary>
    /// Represents a ticket office
    /// </summary>
    public class AgentTicketOffice : Agent
    {
        /// <summary>
        /// Create a ticket office agent
        /// </summary>
        /// <param name="name">Name of the agent</param>
        private AgentTicketOffice(string name) : base(name)
        {
        }

        public override Vector3 Position => _body.transform.Find("Counter").position;

        public AgentTicketOffice() : this("Agent")
        {
            _stateMachine = new TicketOfficeStateMachine(this);
        }

        public override void CreateBody()
        {
            CreateBody("TicketOfficeBody");
        }

    }
}
