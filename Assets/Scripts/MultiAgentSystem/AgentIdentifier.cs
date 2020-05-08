using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.MultiAgentSystem
{
    /// <summary>
    /// Class to store an agent identifier
    /// </summary>
    public class AgentIdentifier
    {

        private int _identifier;

        public int Identifier { get => _identifier; }

        public AgentIdentifier(int indentifier)
        {
            _identifier = indentifier;
        }

    }
}
