using System;
using MultiAgentSystem;
using UnityEngine;

namespace MultiAgentSystem
{
    public class AgentSpectatorBody : AgentBody
    {
        private void OnTriggerEnter(Collider other){
            if (other.tag == "TicketOffice")
            {
                AgentSpectator spec = agent as AgentSpectator;
                if (!spec.inQueue && !spec.ticket)
                {
                    Agent ticketOffice = other.transform.parent.GetComponent<AgentBody>().agent;
                    agent.SendMessage(ticketOffice, new MessageAskForTicket());
                    agent.SendMessage(ticketOffice, new MessageAskForQueue());
                }
            }
        }
    }
}