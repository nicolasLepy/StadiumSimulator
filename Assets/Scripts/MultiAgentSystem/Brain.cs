using System;
using Assets.Scripts.MultiAgentSystem;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
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

        private List<Agent> _askedForASuicide;

        /// <summary>
        /// "Mail box" for all agents who wants send a message to other agents
        /// Messages are treated every loop in the simulation (~30-50 time / sec)
        /// </summary>
        private List<Message> _messages;

        private void SpawnTicketsOffices(GameObject ticketOfficesLocation, int number)
        {
            for (int i = 0; i < number; i++)
            {
                Vector3 position = ticketOfficesLocation.transform.position + (i*8 * ticketOfficesLocation.gameObject.transform.forward);
                Agent ato = SpawnAgent<AgentTicketOffice>(position);
                ato.Body.gameObject.transform.rotation = Quaternion.Euler(new Vector3(0,ticketOfficesLocation.transform.rotation.eulerAngles.y + 90,0));  
            }
        }
        
        /// <summary>
        /// Initialize the multi-agent brain
        /// </summary>
        public Brain(List<int> ticketsOffices)
        {
            _askedForASuicide = new List<Agent>();
            _messages = new List<Message>();
            _agents = new List<KeyValuePair<Guid, Agent>>();
            _provider = new MessageTracker();
            
            //North ticket offices
            SpawnTicketsOffices(GameObject.Find("NorthTicketOfficeSpawner"), ticketsOffices[0]);
            //South ticket offices
            SpawnTicketsOffices(GameObject.Find("SouthTicketOfficeSpawner"), ticketsOffices[1]);
            //West ticket offices
            SpawnTicketsOffices(GameObject.Find("WestTicketOfficeSpawner"), ticketsOffices[2]);
            //East ticket offices
            SpawnTicketsOffices(GameObject.Find("EastTicketOfficeSpawner"), ticketsOffices[3]);

            /*
            foreach (GameObject ticketOfficeSpawner in GameObject.FindGameObjectsWithTag("TicketOfficeSpawner"))
            {
                Agent ato = SpawnAgent<AgentTicketOffice>(ticketOfficeSpawner.transform.position);
                ato.Body.gameObject.transform.rotation = ticketOfficeSpawner.transform.rotation;
            }*/

            foreach (GameObject securitySpawner in GameObject.FindGameObjectsWithTag("SecuritySpawner"))
            {
                Agent ags = SpawnAgent<AgentSecurity>(securitySpawner.transform.position);
                ags.Body.gameObject.transform.rotation = securitySpawner.transform.rotation;
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
            foreach(KeyValuePair<Guid,Agent> a in _agents)
            {
                a.Value.StateMachine.Action();
            }

            foreach (Agent a in _askedForASuicide)
            {
                KeyValuePair<Guid, Agent> agent;
                foreach (KeyValuePair<Guid, Agent> kvp in _agents)
                {
                    if (kvp.Value == a) agent = kvp;
                }

                _agents.Remove(agent);
            }
            _askedForASuicide.Clear();

        }

        /// <summary>
        /// Spawn an agent in the scene
        /// </summary>
        /// <param name="position">Spawn position</param>
        public Agent SpawnAgent<T>(Vector3 position) where T : Agent, new()
        {
            T newAgent = new T();
            KeyValuePair<Guid,Agent> agent = new KeyValuePair<Guid, Agent>(newAgent.AgentId,newAgent);
            _agents.Add(agent);
            newAgent.Subscribe(_provider);
            newAgent.Body.transform.position = position;
            newAgent.CreateStateMachine();
            return newAgent;
        }

        public void AgentCommitSuicide(Agent a)
        {
            _askedForASuicide.Add(a);
        }
    }

}
