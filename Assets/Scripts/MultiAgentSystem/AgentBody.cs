using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using NavMeshBuilder = UnityEditor.AI.NavMeshBuilder;
using Object = System.Object;

namespace MultiAgentSystem
{
    public abstract class AgentBody : MonoBehaviour
    {

        protected Rigidbody _rigidbody;
        private GameObject _gameObject;
        private Agent _agent;
        protected NavMeshAgent _navMeshAgent;

        public Agent agent
        {
            get => _agent;
            set => _agent = value;
        }


        public GameObject GameObject;

        protected abstract void BodyUpdate();
        
        void FixedUpdate()
        {
            BodyUpdate();
        }
        
        private void Start()
        {
            _gameObject = this.gameObject;
            _rigidbody = GetComponent<Rigidbody>();

        }

        /// <summary>
        /// Move the agent to a destination
        /// </summary>
        /// <param name="destination">The position for the agent to go</param>
        public void MoveToDestination(Vector3 destination)
        {
            _navMeshAgent.destination = destination;
        }

        public void InitializeNavMesh()
        {
            _navMeshAgent = GetComponent<NavMeshAgent>();
        }

        /// <summary>
        /// Get the distance of closest ticket office
        /// </summary>
        /// <returns>The distance between the agent and the closest ticket office</returns>
        /*public float GetClosestTicketOfficeDistance()
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
        }*/

      


        

    }
}
