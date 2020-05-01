using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public override void CreateBody()
        {
            CreateBody("TicketOfficeBody");
        }

    }
}
