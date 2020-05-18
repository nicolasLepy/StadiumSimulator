using System;
using System.Collections.Generic;
using MultiAgentSystem;
using UnityEngine;
using UnityEngine.AI;

namespace MultiAgentSystem
{
    public class AgentSpectatorBody : AgentBody
    {

        private List<AgentTicketOffice> _inLineOfVision;

        public List<AgentTicketOffice> inLineOfVision => _inLineOfVision;

        /// <summary>
        /// Get the distance of closest ticket office
        /// </summary>
        /// <returns>The distance between the agent and the closest ticket office</returns>
        public float GetClosestTicketOfficeDistance()
        {
            float minDistance = -1;
            foreach (AgentTicketOffice ato in _inLineOfVision)
            {
                GameObject g = ato.Body.gameObject;
                float distance = Vector3.Distance(g.transform.position, agent.Position);
                if (distance < minDistance || minDistance == -1)
                {
                    minDistance = distance;
                }
            }

            return minDistance == -1 ? 1000 : minDistance;
        }
        
        private void Awake()
        {
            _inLineOfVision = new List<AgentTicketOffice>();
        }
        
        private void OnTriggerEnter(Collider other){

            if (other.tag == "Counter")
            {
                _inLineOfVision.Add(other.transform.parent.GetComponent<AgentBody>().agent as AgentTicketOffice);
            }
            /*if (other.tag == "Counter")
            {
                AgentSpectator spec = agent as AgentSpectator;
                if (!spec.inQueue && !spec.ticket)
                {
                    Agent ticketOffice = other.transform.parent.GetComponent<AgentBody>().agent;
                    //agent.SendMessage(ticketOffice, new MessageAskForQueue());
                }
            }*/
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.tag == "Counter")
            {
                AgentTicketOffice exited = other.transform.parent.GetComponent<AgentBody>().agent as AgentTicketOffice;
                if (_inLineOfVision.Contains(exited))
                    _inLineOfVision.Remove(exited);
            }

        }
    }
}