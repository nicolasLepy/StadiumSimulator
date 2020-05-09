using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

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


        private void Start()
        {
            _gameObject = this.gameObject;
            
        }

        private void FixedUpdate()
        {
            
        }

        

    }
}
