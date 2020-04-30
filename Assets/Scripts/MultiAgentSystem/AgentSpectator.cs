using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        /// <param name="nom">Name of the agent</param>
        public AgentSpectator(string nom) : base(nom)
        {
        }
    }
}
