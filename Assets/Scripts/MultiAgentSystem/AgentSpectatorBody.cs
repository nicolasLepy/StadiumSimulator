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
                Agent ticketOffice = other.transform.parent.GetComponent<AgentBody>().agent;
                agent.SendMessage(ticketOffice, MessageType.ASK_FOR_TICKET);
            }
        }
    }
}