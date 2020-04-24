using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MultiAgentSystem
{
    public class Agent { 

        private Environment GetEnvironment
        {
            get => Environment.GetInstance();
        }

        private List<Event> _events;

        private void SendMessage(Agent receiver, Message message)
        {
            GetEnvironment.Brain.AddMessage(this, receiver, message);
        }
    }

}
