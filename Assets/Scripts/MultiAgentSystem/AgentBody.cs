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

        public GameObject GameObject;

        private void Start()
        {
            _gameObject = this.gameObject;
        }

        private void Update()
        {
            
        }

    }
}
