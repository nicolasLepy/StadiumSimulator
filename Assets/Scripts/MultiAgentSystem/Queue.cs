using System.Collections.Generic;
using UnityEngine;

namespace MultiAgentSystem
{
    /// <summary>
    /// FIFO Queue
    /// </summary>
    public class Queue
    {
        private List<int> _peoplesInQueue;

        public List<int> peoplesInQueue => _peoplesInQueue;

        private List<Agent> _agents;
        private Agent _owner;

        public Queue(Agent owner)
        {
            _peoplesInQueue = new List<int>();
            _agents = new List<Agent>();
            _owner = owner;
        }

        public Vector3 position
        {
            get
            {
                var res = _agents.Count > 0 ? _agents[_agents.Count - 1].Position : _owner.Position;
                return res;
            }
        }

        /// <summary>
        /// Add an agent to the queue
        /// </summary>
        /// <param name="agent">The agent to add</param>
        public void Add(Agent agent)
        {
            _agents.Add(agent);
        }

        public List<Agent> agents => _agents;
        
        /// <summary>
        /// Delete the first agent of the queue and return it
        /// </summary>
        /// <returns>The first agent of the queue</returns>
        public Agent Pop()
        {
            Agent res = null;
            if (_agents.Count > 0)
            {
                res = _agents[0];
                _agents.RemoveAt(0);
            }
            return res;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="agent"></param>
        /// <returns></returns>
        public Vector3 GetPositionForAgent(Agent agent)
        {
            Vector3 position = _owner.Position;
            int idx = 0;
            foreach (Agent a in _agents)
            {
                if (a == agent)
                {
                    position = _owner.Position + -idx * 1.8f * _owner.Body.transform.forward;
                }
                idx++;
            }

            return position;
        }

        /// <summary>
        /// Return first agent of the queue
        /// </summary>
        /// <returns>First agent of the queue or null if queue is empty</returns>
        public Agent First()
        {
            Agent res = null;
            if (_agents.Count > 0)
                res = _agents[0];
            return res;
        }
        /// <summary>
        /// Return last agent of the queue
        /// </summary>
        /// <returns>Last agent of the queue or null if queue is empty</returns>
        public Agent Last()
        {
            Agent res = null;
            if (_agents.Count > 0)
                res = _agents[_agents.Count - 1];
            return res;
        }

        public int GetNumberInQueueForAgent(Agent agent)
        {
            return _agents.IndexOf(agent);
        }
    }
}