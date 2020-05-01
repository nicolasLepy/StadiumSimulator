using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MultiAgentSystem { 

    /// <summary>
    /// Represents an agent
    /// </summary>
    public abstract class Agent {

        /// <summary>
        /// Name of the agent
        /// </summary>
        private string _name;
        protected GameObject _body;
        
        public string Name { get => _name; set => _name = value; }

        public GameObject Body { get => _body; }

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
        /// <param name="name">Name of the agent</param>
        public Agent(string name)
        {
            _name = name;
            CreateBody();
        }


        /// <summary>
        /// Create physical body of the agent
        /// </summary>
        /// <param name="prefabName">Name of the prefab located in the <b>Prefabs/</b> folder</param>
        public void CreateBody(string prefabName)
        {
            _body = Resources.Load("Prefabs/" + prefabName, typeof(GameObject)) as GameObject;
            _body = GameObject.Instantiate(_body, new Vector3(0, 0, 0), _body.transform.rotation);
            _body.transform.name = _name + "Body";
        }

        public abstract void CreateBody();


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
