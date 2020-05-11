using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;

namespace MultiAgentSystem
{
    public class AgentBody : MonoBehaviour
    {

        private GameObject _gameObject;
        private Agent _agent;

        public Agent agent
        {
            get => _agent;
            set => _agent = value;
        }


        public GameObject GameObject;

        private void Start()
        {
            _gameObject = this.gameObject;
            
        }
        
        public void MoveToDestination(Vector3 destination)
        {
            GetComponent<NavMeshAgent>().destination = destination;
        }
        
        public GameObject GetClosestTicketOffice(float rayon)
        {
            GameObject res = null;
            float minDistance = -1;
            foreach (GameObject g in GameObject.FindGameObjectsWithTag("TicketOffice"))
            {
                float distance = Vector3.Distance(g.transform.position, _agent.Position);
                if (distance < minDistance || minDistance == -1)
                {
                    minDistance = distance;
                    res = g;
                }
            }

            return res;
        }
        
        public float GetClosestTicketOfficeDistance()
        {
            float minDistance = -1;
            foreach (GameObject g in GameObject.FindGameObjectsWithTag("TicketOffice"))
            {
                float distance = Vector3.Distance(g.transform.position, _agent.Position);
                if (distance < minDistance || minDistance == -1)
                {
                    minDistance = distance;
                }
            }

            return minDistance;
        }

        private void FixedUpdate()
        {
            
        }

        

    }
}
