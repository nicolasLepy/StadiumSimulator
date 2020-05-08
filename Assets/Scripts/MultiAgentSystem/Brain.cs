using System;
using Assets.Scripts.MultiAgentSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MultiAgentSystem
{
    /// <summary>
    /// "Brain" of the multiagent system
    /// Has a view on all agents
    /// </summary>
    public class Brain
    {

        /// <summary>
        /// Agents of the multiagent system
        /// </summary>
        private List<KeyValuePair<Guid, Agent>> _agents;

        public List<KeyValuePair<Guid, Agent>> Agents => _agents;

        /// <summary>
        /// "Mail box" for all agents who wants send a message to other agents
        /// Messages are treated every loop in the simulation (~30-50 time / sec)
        /// </summary>
        private List<Message> _messages;

        private int _currentIdentifier;

        /// <summary>
        /// Initialize the multi-agent brain
        /// </summary>
        public Brain()
        {
            _currentIdentifier = 0;
            _agents = new List<KeyValuePair<Guid, Agent>>();
            _messages = new List<Message>();
            for(int i = 0; i < 400; i++)
            {
                int x = UnityEngine.Random.Range(-100, 100);
                int z = UnityEngine.Random.Range(-100, 100);
                SpawnAgent<AgentSpectator>(new Vector3(x, 5, z));
            }
            SpawnAgent<AgentTicketOffice>(new Vector3(0, 5, 0));
        }

        /// <summary>
        /// Add a message in the "mail box"
        /// </summary>
        /// <param name="sender">Agent who send the message</param>
        /// <param name="receiver">Targeted agent for the message</param>
        /// <param name="message">Type of the message</param>
        public void AddMessage(Agent sender, Agent receiver, MessageType message)
        {
            _messages.Add(new Message(sender, receiver, message));
        }

        /// <summary>
        /// Main cycle of brain execution
        /// </summary>
        public void Loop()
        {
            foreach(Message m in _messages)
            {
                Debug.Log("message treated from " + m.Sender.Name + " to " + m.Receiver + " (" + m.Type.ToString() + ")");
            }
            _messages.Clear();

            foreach(KeyValuePair<Guid,Agent> a in _agents)
            {
                a.Value.StateMachine.Action();
            }
        }

        /// <summary>
        /// Spawn an agent in the scene
        /// </summary>
        /// <param name="position">Spawn position</param>
        private Agent SpawnAgent<T>(Vector3 position) where T : Agent, new()
        {
            T newAgent = new T();
            KeyValuePair<Guid,Agent> agent = new KeyValuePair<Guid, Agent>(newAgent.AgentId,newAgent);
            _agents.Add(agent);
            newAgent.Body.transform.position = position;
            return newAgent;
        }
    }

}
