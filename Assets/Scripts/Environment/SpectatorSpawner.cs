using System.Collections;
using UnityEngine;

namespace MultiAgentSystem
{

    public class SpectatorSpawner : MonoBehaviour
    {

        [SerializeField] private int _averageAgentsGroupByMinute = 0;
        [SerializeField] private int _agentGroupMin = 0;
        [SerializeField] private int _agentGroupMax = 0;
        [SerializeField] private int _spawiningDurationInSec = 0;
        [SerializeField] private int _ticketPercentage = 0;
        [SerializeField] private int _radius = 15;
        [SerializeField] private int _awaySpectatorPercentage = 20;
        [SerializeField] private bool _noRandom = false;
        [SerializeField] private int _fixedSpawnIntervalInSec = 0;

        public int awaySpectatorPercentage
        {
            get => _awaySpectatorPercentage;
            set => _awaySpectatorPercentage = value;
        }
        
        public int radius
        {
            get => _radius;
            set => _radius = value;
        }

        public int spawiningDurationInSec
        {
            get => _spawiningDurationInSec;
            set => _spawiningDurationInSec = value;
        }
        
        public int ticketPercentage
        {
            get => _ticketPercentage;
            set => _ticketPercentage = value;
        }

        public int fixedSpawnIntervalInSec
        {
            get => _fixedSpawnIntervalInSec;
            set => _fixedSpawnIntervalInSec = value;
        }

        public bool noRandom
        {
            get => _noRandom;
            set => _noRandom = value;
        }

        public int agentGroupMin
        {
            get => _agentGroupMin;
            set => _agentGroupMin = value;
        }

        public int agentGroupMax
        {
            get => _agentGroupMax;
            set => _agentGroupMax = value;
        }

        private int _time;

        // Start is called before the first frame update
        void Start()
        {
            _time = 0;
        }

        public void Activate()
        {
            StartCoroutine(CoroutineSpawnAgent());
        }
        
        IEnumerator CoroutineSpawnAgent()
        {
            //Agent arrive during 1 minutes
            for (int i = 0; i < _spawiningDurationInSec; i++)
            {
                _time++;
                int probAgents = (int) (60f / _averageAgentsGroupByMinute);
                if ((!_noRandom && UnityEngine.Random.Range(0, probAgents - 1) == 0) || (_noRandom && _time == _fixedSpawnIntervalInSec))
                {
                    int agentsToSpawn = UnityEngine.Random.Range(_agentGroupMin, _agentGroupMax);
                    for (int j = 0; j < agentsToSpawn; j++)
                    {
                        float x = UnityEngine.Random.Range(-_radius, _radius) + transform.position.x;
                        float z = UnityEngine.Random.Range(-_radius, _radius) + transform.position.z;
                        AgentSpectator spawnedAgent = (AgentSpectator) Environment.GetInstance().Brain
                            .SpawnAgent<AgentSpectator>(new Vector3(x, 5, z));

                        if (UnityEngine.Random.Range(1, 100) <= _awaySpectatorPercentage)
                        {
                            spawnedAgent.side = Team.AWAY;
                        }
                        else
                        {
                            spawnedAgent.side = Team.HOME;
                        }
                        //We gave to him categories for his side
                        spawnedAgent.hisSideCategories.AddRange(Environment.GetInstance().environment
                            .CategoriesAvailableForSide(spawnedAgent.side));
                        
                        if (UnityEngine.Random.Range(1, 100) <= _ticketPercentage)
                        {
                            Ticket t = Environment.GetInstance().environment.RequestSeat(spawnedAgent.GetCategory());
                            if(t!=null)
                                spawnedAgent.ticket = t;
                        }
                    }
                }
                if (_time == _fixedSpawnIntervalInSec)
                    _time = 0;
                yield return new WaitForSeconds(1f);
            }
            print("Spectators don't come anymore");
            yield return new WaitForSeconds(1f);
            print("Coroutine finished.");
        }
    }
}