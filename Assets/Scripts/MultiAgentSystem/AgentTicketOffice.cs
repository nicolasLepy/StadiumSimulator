﻿using System;
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
        public AgentTicketOffice(string name) : base(name)
        {
        }

        public override Vector3 Position
        {
            get
            {
                return _body.transform.Find("Counter").position;
            }
        }

        public AgentTicketOffice() : this("Agent")
        {

        }

        public override void CreateBody()
        {
            CreateBody("TicketOfficeBody");
        }

    }
}
