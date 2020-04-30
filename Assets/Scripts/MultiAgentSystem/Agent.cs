using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MultiAgentSystem { 

    /// <summary>
    /// Represents an agent
    /// </summary>
    public class Agent {

        /// <summary>
        /// Name of the agent
        /// </summary>
        private string _nom;
        
        public string Nom { get => _nom; }

        /// <summary>
        /// Get the multi-agents environment
        /// </summary>
        private Environment GetEnvironment
        {
            get => Environment.GetInstance();
        }


        private List<Event> _events;

        /// <summary>
        /// Create an agent
        /// </summary>
        /// <param name="nom">Name of the agent</param>
        public Agent(string nom)
        {
            _nom = nom;
        }

        /// <summary>
        /// Send a message to another agent
        /// </summary>
        /// <param name="receiver">The targeted agent</param>
        /// <param name="message">Type of the message</param>
        private void SendMessage(Agent receiver, MessageType message)
        {
            GetEnvironment.Brain.AddMessage(this, receiver, message);
        }

    }

}
