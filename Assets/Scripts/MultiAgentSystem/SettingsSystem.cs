using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UI;

namespace MultiAgentSystem
{

    public class SettingsSystem : MonoBehaviour
    {

        private GameObject _mainCanvas;


        private int GetInputValue(string inputBoxName)
        {
            InputField inputValue = GameObject.Find(inputBoxName).GetComponent<InputField>();
            return int.Parse(inputValue.text);
        }
        
        private int GetAwaySpectatorsPourcentage()
        {
            return GetInputValue("InputAwaySpectators");
        }
        
        private int GetArrivalTime()
        {
            InputField inputSpectatorTime = GameObject.Find("InputSpectatorTime").GetComponent<InputField>();
            return int.Parse(inputSpectatorTime.text);
        }

        private int GetTicketTime()
        {
            InputField inputTicketTime = GameObject.Find("InputTicketTime").GetComponent<InputField>();
            return int.Parse(inputTicketTime.text);
        }

        private int GetTicketProportion()
        {
            InputField inputTicket = GameObject.Find("InputTicket").GetComponent<InputField>();
            return int.Parse(inputTicket.text);
        }
        private List<int> GetTicketsOfficesNumber()
        {
            List<int> res = new List<int>();
            InputField inputFieldNorth = GameObject.Find("InputFieldNorth").GetComponent<InputField>();
            InputField inputFieldSud = GameObject.Find("InputFieldSouth").GetComponent<InputField>();
            InputField inputFieldWest = GameObject.Find("InputFieldWest").GetComponent<InputField>();
            InputField inputFieldEast = GameObject.Find("InputFieldEast").GetComponent<InputField>();
            res.Add(int.Parse(inputFieldNorth.text));
            res.Add(int.Parse(inputFieldSud.text));
            res.Add(int.Parse(inputFieldWest.text));
            res.Add(int.Parse(inputFieldEast.text));
            return res;
        }


        private void CreateTramLocation(string poleIndication, Vector3 position)
        {
            if (GameObject.Find("ToggleTram" + poleIndication).GetComponent<Toggle>().isOn)
            {
                int every = int.Parse(GameObject.Find("InputTram"+poleIndication+"Sec").GetComponent<InputField>().text);
                int min = int.Parse(GameObject.Find("InputTram"+poleIndication+"Min").GetComponent<InputField>().text);
                int max = int.Parse(GameObject.Find("InputTram"+poleIndication+"Max").GetComponent<InputField>().text);
                GameObject sp = Resources.Load("Prefabs/SpectatorSpawner",typeof(GameObject)) as GameObject;
                sp = Instantiate(sp, position, Quaternion.identity);
                sp.GetComponent<SpectatorSpawner>().noRandom = true;
                sp.GetComponent<SpectatorSpawner>().agentGroupMax = max;
                sp.GetComponent<SpectatorSpawner>().agentGroupMin = min;
                sp.GetComponent<SpectatorSpawner>().fixedSpawnIntervalInSec = every;
                sp.GetComponent<SpectatorSpawner>().ticketPercentage = GetTicketProportion();
                sp.GetComponent<SpectatorSpawner>().spawiningDurationInSec = GetArrivalTime();
                sp.GetComponent<SpectatorSpawner>().radius = 5;
                sp.GetComponent<SpectatorSpawner>().awaySpectatorPercentage = GetAwaySpectatorsPourcentage();
                sp.GetComponent<SpectatorSpawner>().Activate();
            }
   
        }
        private void CreateTramSpawner()
        {
            Vector3 position = GameObject.Find("StadiumEntranceNorth").transform.position;
            position = new Vector3(position.x - 150, position.y, position.z);
            CreateTramLocation("North",position);

            position = GameObject.Find("StadiumEntranceSouth").transform.position;
            position = new Vector3(position.x + 150, position.y, position.z);
            CreateTramLocation("South",position);
            
            position = GameObject.Find("StadiumEntranceWest").transform.position;
            position = new Vector3(position.x, position.y, position.z-150);
            CreateTramLocation("West",position);

            position = GameObject.Find("StadiumEntranceEast").transform.position;
            position = new Vector3(position.x, position.y, position.z+150);
            CreateTramLocation("East",position);
            
        }
        
        public void OnClickButtonStartSimulation()
        {
            _mainCanvas.SetActive(true);

            Settings settings = new Settings(GetTicketTime());
            Environment.GetInstance().CreateBrain(GetTicketsOfficesNumber(), settings);

            GameObject.Find("Environment").AddComponent<EnvironmentUI>();
            gameObject.AddComponent<MultiAgentSystem>();

            int ticketProportion = GetTicketProportion();
            foreach (SpectatorSpawner sp in GameObject.FindObjectsOfType<SpectatorSpawner>())
            {
                sp.spawiningDurationInSec = GetArrivalTime();
                sp.awaySpectatorPercentage = GetAwaySpectatorsPourcentage();
                sp.ticketPercentage = ticketProportion;
                sp.Activate();
            }
            CreateTramSpawner();
            GameObject.Find("CanvasSettings").SetActive(false);

        }

        public void OnClickExportData()
        {
            Environment.GetInstance().Brain.ExportData();
        }

    
        // Start is called before the first frame update
        void Start()
        {
            _mainCanvas = GameObject.Find("CanvasMain");
            _mainCanvas.SetActive(false);
        }

        // Update is called once per frame
        void Update()
        {
        
        }
    }

    
}
