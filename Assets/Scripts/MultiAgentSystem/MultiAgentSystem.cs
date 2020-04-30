using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MultiAgentSystem
{
    /// <summary>
    /// Physical bridge between multiagent system and 3D scene
    /// </summary>
    public class MultiAgentSystem : MonoBehaviour
    {

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            Environment.GetInstance().Brain.Loop();
        }

    }
}
