using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;
using Object = System.Object;

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
        
        /// <summary>
        /// Move the agent to a destination
        /// </summary>
        /// <param name="destination">The position for the agent to go</param>
        public void MoveToDestination(Vector3 destination)
        {
            GetComponent<NavMeshAgent>().destination = destination;
        }

        /// <summary>
        /// Get ticket offices in a radius from agent position
        /// </summary>
        /// <param name="maxRadius">The max radius to detect ticket offices</param>
        /// <returns>List of ticket offices</returns>
        public List<GameObject> ListCloseTicketOffice(float maxRadius)
        {
            List<GameObject> res = new List<GameObject>();

            foreach (GameObject g in GameObject.FindGameObjectsWithTag("TicketOffice"))
            {
                float distance = Vector3.Distance(g.transform.position, _agent.Position);
                if (distance < maxRadius)
                {
                    res.Add(g);
                }
            }
            
            return res;
        }

        /// <summary>
        /// Get the distance of closest ticket office
        /// </summary>
        /// <returns>The distance between the agent and the closest ticket office</returns>
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
