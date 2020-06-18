using UnityEngine;
using UnityEngine.AI;

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

        private bool isMoving = false;
        private Vector3 target;
        
        void FixedUpdate()
        {
            BodyUpdate();

            if (isMoving)
            {
                float step =  9 * Time.deltaTime;
                transform.position = Vector3.MoveTowards(transform.position, target, step);
            }
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
            if (Environment.GetInstance().settings.noNavMesh)
            {
                target = destination;
                isMoving = true;
            }
            else
            {
                _navMeshAgent.destination = destination;
            }
        }
        
        public void InitializeNavMesh()
        {
            _navMeshAgent = GetComponent<NavMeshAgent>();
        }
    }
}
