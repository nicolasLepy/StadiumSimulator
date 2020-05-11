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
        private MessageTracker _provider;

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
        
        /// <summary>
        /// Initialize the multi-agent brain
        /// </summary>
        public Brain()
        {
            _messages = new List<Message>();
            _agents = new List<KeyValuePair<Guid, Agent>>();
            _provider = new MessageTracker();
            
            
            foreach (GameObject ticketOfficeSpawner in GameObject.FindGameObjectsWithTag("SpectatorSpawner"))
            {
                for(int i = 0; i < 20; i++)
                {
                    float x = UnityEngine.Random.Range(-60, 60) + ticketOfficeSpawner.transform.position.x;
                    float z = UnityEngine.Random.Range(-60, 60) + ticketOfficeSpawner.transform.position.z;
                    SpawnAgent<AgentSpectator>(new Vector3(x, 5, z));
                }
            }

            
            foreach (GameObject ticketOfficeSpawner in GameObject.FindGameObjectsWithTag("TicketOfficeSpawner"))
            {
                Agent ato = SpawnAgent<AgentTicketOffice>(ticketOfficeSpawner.transform.position);
                ato.Body.gameObject.transform.rotation = ticketOfficeSpawner.transform.rotation;
            }
        }

        /// <summary>
        /// Add a message in the "mail box"
        /// </summary>
        /// <param name="message">Message to broadcast</param>
        public void AddMessage(Message message)
        {
            _provider.TrackMessage(message);
            //_messages.Add(message);
        }

        /// <summary>
        /// Main cycle of brain execution
        /// </summary>
        public void Loop()
        {
            foreach (KeyValuePair<Guid, Agent> agent in _agents)
            {
                agent.Value.ReadMailbox();
            }

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
            _provider.Subscribe(newAgent);
            newAgent.Body.transform.position = position;
            return newAgent;
        }
    }

}
