using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.MultiAgentSystem;
using UnityEngine;

namespace MultiAgentSystem { 

    /// <summary>
    /// Represents an agent
    /// </summary>
    public abstract class Agent : ISimulationClock
    {


        private float _spawnTime;
        /// <summary>
        /// Time when an agent is spawned
        /// </summary>
        public float SpawnTime => _spawnTime;
        
        /// <summary>
        /// Name of the agent
        /// </summary>
        private string _name;
        protected GameObject _body;

        /// <summary>
        /// Behaviour state machine of the agent
        /// </summary>
        protected StateMachine _stateMachine;

        public string Name { get => _name; set => _name = value; }

        public GameObject Body { get => _body; }

        public StateMachine StateMachine { get => _stateMachine; }
        /// <summary>
        /// Get the multi-agents environment
        /// </summary>
        private Environment GetEnvironment
        {
            get => Environment.GetInstance();
        }

        /// <summary>
        /// Get position of the agent body in the scene
        /// </summary>
        public abstract Vector3 Position { get; }

        private List<Event> _events;

        /// <summary>
        /// Create an agent
        /// </summary>
        /// <param name="name">Name of the agent</param>
        public Agent(string name)
        {
            _spawnTime = Time.time;
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

        protected abstract void CreateBody();


        /// <summary>
        /// Send a message to another agent
        /// </summary>
        /// <param name="receiver">The targeted agent</param>
        /// <param name="message">Type of the message</param>
        protected void SendMessage(Agent receiver, MessageType message)
        {
            GetEnvironment.Brain.AddMessage(this, receiver, message);
        }


        /// <summary>
        /// Current simulation time of the agent
        /// </summary>
        /// <returns></returns>
        public float GetSimulationTime()
        {
            return Time.time - _spawnTime;
        }
    }

}
