using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using Assets.Scripts.MultiAgentSystem;
using UnityEngine;
using UnityEngine.AI;

namespace MultiAgentSystem { 

    /// <summary>
    /// Represents an agent
    /// </summary>
    public abstract class Agent : ISimulationClock, IObserver<Message>
    {

        private IDisposable unsubscriber;

        private Guid _agentId;

        public Guid AgentId => _agentId;

        private float _spawnTime;
        /// <summary>
        /// Time when an agent is spawned
        /// </summary>
        public float SpawnTime => _spawnTime;
        
        /// <summary>
        /// Name of the agent
        /// </summary>
        private string _name;
        public string Name => _name;

        protected GameObject _body;

        /// <summary>
        /// Behaviour state machine of the agent
        /// </summary>
        protected StateMachine _stateMachine;


        public AgentBody Body { get => _body.GetComponent<AgentBody>(); }

        public StateMachine StateMachine { get => _stateMachine; }

        public abstract void CreateStateMachine();

        /// <summary>
        /// Get position of the agent body in the scene
        /// </summary>
        public abstract Vector3 Position { get; }
        
        protected List<Message> _archivedMailbox;

        public List<Message> archivedMailbox => _archivedMailbox;



        /// <summary>
        /// Create an agent
        /// </summary>
        /// <param name="name">Name of the agent</param>
        public Agent(string name)
        {
            _archivedMailbox = new List<Message>();
            _spawnTime = Time.time;
            _name = name;
            _agentId = Guid.NewGuid();
            CreateBody();
        }


        /// <summary>
        /// Create physical body of the agent
        /// </summary>
        /// <param name="prefabName">Name of the prefab located in the <b>Prefabs/</b> folder</param>
        public void CreateBody<T>(string prefabName) where T : AgentBody
        {
            _body = Resources.Load("Prefabs/" + prefabName, typeof(GameObject)) as GameObject;
            _body = GameObject.Instantiate(_body, new Vector3(0, 0, 0), _body.transform.rotation);
            _body.transform.name = _name + "Body";
            _body.AddComponent<T>();
            _body.GetComponent<T>().agent = this;
            _body.AddComponent<AgentUI>();
        }

        protected abstract void CreateBody();
        

        /// <summary>
        /// Send a message to another agent
        /// </summary>
        /// <param name="receiver">The targeted agent</param>
        /// <param name="message">Type of the message</param>
        public void SendMessage(Agent receiver, MessageType message)
        {
            Message msg = new Message(this, receiver, message);
            Environment.GetInstance().Brain.AddMessage(msg);
        }

        public override string ToString()
        {
            return Name + "(" + _agentId + ")";
        }

        /// <summary>
        /// Current simulation time of the agent
        /// </summary>
        /// <returns></returns>
        public float GetSimulationTime()
        {
            return Time.time - _spawnTime;
        }

        public virtual void Subscribe(IObservable<Message> provider)
        {
            if (provider != null)
                unsubscriber = provider.Subscribe(this);
        }

        public virtual void Unsubscribe()
        {
            unsubscriber.Dispose();
        }

        public virtual void OnCompleted()
        {
            Debug.Log("The message tracker has completed transmitting data to " + _name);
            Unsubscribe();
        }

        public virtual void OnError(Exception e)
        {
            Debug.Log(_name + "The message cannot be determined");
        }

        /// <summary>
        /// Do an action depending on message and agent type
        /// </summary>
        /// <param name="message">The message to process</param>
        public abstract void ProcessMessage(Message message);

        /// <summary>
        /// Receive a message from tracker
        /// </summary>
        /// <param name="value">The message</param>
        public void OnNext(Message value)
        {
            if (value.Receiver == this || value.Receiver == null)
            {
                ProcessMessage(value);
            }

        }

        public void CommitSuicide()
        {
            Environment.GetInstance().Brain.AgentCommitSuicide(this);
            (Body as AgentSpectatorBody).Detach();



        }
    }

}
