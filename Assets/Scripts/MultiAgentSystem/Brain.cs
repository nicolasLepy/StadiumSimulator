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
        private List<Agent> _agents;
        /// <summary>
        /// "Mail box" for all agents who wants send a message to other agents
        /// Messages are treated every loop in the simulation (~30-50 time / sec)
        /// </summary>
        private List<Message> _messages;

        /// <summary>
        /// Initialize the multi-agent brain
        /// </summary>
        public Brain()
        {
            _agents = new List<Agent>();
            _messages = new List<Message>();
            SpawnAgent<AgentSpectator>();
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
                Debug.Log("message traité de " + m.Sender.Name + " vers " + m.Receiver + " (" + m.Type.ToString() + ")");
            }
            _messages.Clear();
        }

        /// <summary>
        /// Spawn an agent in the scene
        /// </summary>
        /// <param name="agent">The agent to create</param>
        public Agent SpawnAgent<T>() where T : Agent, new()
        {
            T newAgent = new T();
            _agents.Add(newAgent);
            return newAgent;
        }
    }

}
