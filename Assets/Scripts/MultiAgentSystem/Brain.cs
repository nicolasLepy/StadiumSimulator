using System;
using Assets.Scripts.MultiAgentSystem;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
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

        private List<float> _timesToSitInStadium;
        public List<float> timesToSitInStadium => _timesToSitInStadium;
        
        private MessageTracker _provider;

        /// <summary>
        /// Agents of the multiagent system
        /// </summary>
        private Dictionary<Guid,Agent> _agents;

        public Dictionary<Guid,Agent> Agents => _agents;

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
            _timesToSitInStadium = new List<float>();
            _askedForASuicide = new List<Agent>();
            _messages = new List<Message>();
            _agents = new Dictionary<Guid,Agent>();
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
                Guid agent;
                foreach (KeyValuePair<Guid, Agent> kvp in _agents)
                {
                    if (kvp.Value == a) agent = kvp.Key;
                }

                _agents.Remove(agent);
            }
            _askedForASuicide.Clear();

            //Every ten seconds
            if (Time.frameCount % 500 == 0)
            {
                foreach (KeyValuePair<Guid, Agent> a in _agents)
                {
                    AgentTicketOffice ato = a.Value as AgentTicketOffice;
                    if (ato != null)
                    {
                         ato.queue.peoplesInQueue.Add(ato.queue.agents.Count);
                    }
                    AgentSecurity sec = a.Value as AgentSecurity;
                    if (sec != null)
                    {
                        sec.queue.peoplesInQueue.Add(sec.queue.agents.Count);
                    }
                }
            }

        }

        /// <summary>
        /// Spawn an agent in the scene
        /// </summary>
        /// <param name="position">Spawn position</param>
        public Agent SpawnAgent<T>(Vector3 position) where T : Agent, new()
        {
            T newAgent = new T();
            _agents.Add(newAgent.AgentId, newAgent);
            newAgent.Subscribe(_provider);
            newAgent.Body.transform.position = position;
            newAgent.CreateStateMachine();
            return newAgent;
        }

        public void AgentCommitSuicide(Agent a)
        {
            _askedForASuicide.Add(a);
        }

        /// <summary>
        /// Export simulation data in csv file
        /// </summary>
        public void ExportData()
        {
            var csv_queues = new StringBuilder();
            var csv_waiting = new StringBuilder();

            
            foreach (var agent in _agents)
            {
                if (agent.Value is AgentTicketOffice ato)
                {
                    int i = 0;
                    foreach (int nb in ato.queue.peoplesInQueue)
                    {
                        var newLine = $"{i * 10},{ato.ToString()},{nb}";
                        csv_queues.AppendLine(newLine);
                        i++;
                    }

                    float total = 0f;
                    foreach (float f in ato.times)
                    {
                        total += f;
                    }

                    csv_waiting.AppendLine($"{ato.ToString()},{total / ato.times.Count}");
                }

                if (agent.Value is AgentSecurity sec)
                {
                    int i = 0;
                    foreach (int nb in sec.queue.peoplesInQueue)
                    {
                        var newLine = $"{i * 10},{sec.ToString()},{nb}";
                        csv_queues.AppendLine(newLine);
                        i++;
                    }
                }
            }
            
            var csv_sit = new StringBuilder();
            foreach (float f in _timesToSitInStadium)
            {
                csv_sit.AppendLine($"{f}");
            }


            File.WriteAllText("data_queues.csv",csv_queues.ToString());
            File.WriteAllText("data_sit.csv",csv_sit.ToString());
            File.WriteAllText("data_waiting.csv",csv_waiting.ToString());

        }
    }

}
