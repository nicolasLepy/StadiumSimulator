using System;
using System.Collections;
using System.Collections.Generic;
using MultiAgentSystem;
using UnityEngine;
using Environment = MultiAgentSystem.Environment;

namespace MultiAgentSystem
{

    public class SpectatorSpawner : MonoBehaviour
    {

        [SerializeField] private int _averageAgentsGroupByMinute;
        [SerializeField] private int _agentGroupMin;
        [SerializeField] private int _agentGroupMax;
        [SerializeField] private int _spawiningDurationInSec;
        [SerializeField] private int _ticketPercentage;
        [SerializeField] private int _radius;

        // Start is called before the first frame update
        void Start()
        {
            StartCoroutine(CoroutineSpawnAgent());
        }

        // Update is called once per frame
        void FixedUpdate()
        {

        }

        IEnumerator CoroutineSpawnAgent()
        {
            //Agent arrive during 1 minutes
            for (int i = 0; i < _spawiningDurationInSec; i++)
            {
                int probAgents = (int) (60f / _averageAgentsGroupByMinute);
                if (UnityEngine.Random.Range(0, probAgents - 1) == 0)
                {
                    int agentsToSpawn = UnityEngine.Random.Range(_agentGroupMin, _agentGroupMax);
                    for (int j = 0; j < agentsToSpawn; j++)
                    {
                        float x = UnityEngine.Random.Range(-_radius, _radius) + transform.position.x;
                        float z = UnityEngine.Random.Range(-_radius, _radius) + transform.position.z;
                        AgentSpectator spawnedAgent = (AgentSpectator) Environment.GetInstance().Brain
                            .SpawnAgent<AgentSpectator>(new Vector3(x, 5, z));
                        if (UnityEngine.Random.Range(1, 100) <= _ticketPercentage)
                        {
                            spawnedAgent.ticket = new Ticket(UnityEngine.Random.Range(0,12));
                        }
                    }
                }

                yield return new WaitForSeconds(1f);
            }

            print("Spectators don't come anymore");
            yield return new WaitForSeconds(1f);
            print("Coroutine finished.");
        }
    }
}