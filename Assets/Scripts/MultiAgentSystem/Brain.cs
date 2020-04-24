using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MultiAgentSystem
{
    public class Brain
    {
        private List<Agent> _agents;

        public Brain()
        {
            _agents = new List<Agent>();
        }

        public void AddMessage(Agent sender, Agent receiver, Message message)
        {

        }
    }

}
