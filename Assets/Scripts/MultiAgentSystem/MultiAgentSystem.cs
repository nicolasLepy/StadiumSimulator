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
            //Hydrade zones position with physical location of zones
            foreach (Zone z in GameObject.FindObjectsOfType<Zone>())
            {
                Environment.GetInstance().environment.SetCategoryPosition(z.zoneNumber,z.transform.position);
            }
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            Environment.GetInstance().Brain.Loop();
        }

    }
}
