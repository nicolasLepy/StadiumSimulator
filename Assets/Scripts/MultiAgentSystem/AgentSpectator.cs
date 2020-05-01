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
        /// <summary>
        /// Create an agent spectator
        /// </summary>
        /// <param name="name">Name of the agent</param>
        public AgentSpectator(string name) : base(name)
        {
        }

        public AgentSpectator() : this("Agent") {
            
        }

        public override void CreateBody()
        {
            CreateBody("SpectatorBody");
        }
    }
}
