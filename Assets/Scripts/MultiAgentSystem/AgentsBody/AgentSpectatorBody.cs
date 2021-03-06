﻿using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace MultiAgentSystem
{
    public class AgentSpectatorBody : AgentBody
    {
        private bool _agentIsDead = false;
        private List<AgentTicketOffice> _ticketOfficeInLineOfVision;
        private List<AgentSecurity> _securityInLineOfVision;

        public List<AgentTicketOffice> ticketOfficeInLineOfVision => _ticketOfficeInLineOfVision;

        public List<AgentSecurity> securityInLineOfVision => _securityInLineOfVision;

        /// <summary>
        /// Get the distance of closest ticket office
        /// </summary>
        /// <returns>The distance between the agent and the closest ticket office</returns>
        public float GetClosestTicketOfficeDistance()
        {
            float minDistance = -1;
            foreach (AgentTicketOffice ato in _ticketOfficeInLineOfVision)
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
            _ticketOfficeInLineOfVision = new List<AgentTicketOffice>();
            _securityInLineOfVision = new List<AgentSecurity>();
        }

        protected override void BodyUpdate()
        {
            if (_agentIsDead)
            {
                transform.position = new Vector3(transform.position.x, 3, transform.position.z);
            }
        }
        
        private void OnTriggerEnter(Collider other){

            if (other.tag == "Counter")
            {
                _ticketOfficeInLineOfVision.Add(other.transform.parent.GetComponent<AgentBody>().agent as AgentTicketOffice);
            }
            else if (other.tag == "Security")
            {
                _securityInLineOfVision.Add(other.transform.GetComponent<AgentBody>().agent as AgentSecurity);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.tag == "Counter")
            {
                AgentTicketOffice exited = other.transform.parent.GetComponent<AgentBody>().agent as AgentTicketOffice;
                if (_ticketOfficeInLineOfVision.Contains(exited))
                    _ticketOfficeInLineOfVision.Remove(exited);
            }
            else if (other.tag == "Security")
            {
                AgentSecurity exited = other.transform.GetComponent<AgentBody>().agent as AgentSecurity;
                if (_securityInLineOfVision.Contains(exited))
                    _securityInLineOfVision.Remove(exited);
            }
        }
        
        public void Detach()
        {
            _agentIsDead = true;
            Destroy(GetComponent<Rigidbody>());
            Destroy(GetComponent<NavMeshAgent>());
        }

        public void Reattach()
        {
            _agentIsDead = false;
            _rigidbody = gameObject.AddComponent<Rigidbody>();
            _rigidbody.mass = 1;

            if (!Environment.GetInstance().settings.noNavMesh)
            {
                _navMeshAgent = gameObject.AddComponent<NavMeshAgent>();
                _navMeshAgent.speed = 9;
                _navMeshAgent.angularSpeed = 120;
                _navMeshAgent.acceleration = 5;
                _navMeshAgent.autoBraking = true;
            }
        }
    }
}