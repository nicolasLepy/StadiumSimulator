using System.Collections.Generic;
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

        private bool GetPedestrianActivated(string pole)
        {
            return GameObject.Find("TogglePedestrians" + pole).GetComponent<Toggle>().isOn;
        }

        private bool GetDontUseNavMesh()
        {
            return GameObject.Find("ToggleMode").GetComponent<Toggle>().isOn;
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
            settings.noNavMesh = GetDontUseNavMesh();
            Environment.GetInstance().CreateBrain(GetTicketsOfficesNumber(), settings);

            GameObject.Find("Environment").AddComponent<EnvironmentUI>();
            gameObject.AddComponent<MultiAgentSystem>();

            int ticketProportion = GetTicketProportion();
            if (!GetPedestrianActivated("North")) GameObject.Destroy(GameObject.Find("NorthStreet"));
            if (!GetPedestrianActivated("South")) GameObject.Destroy(GameObject.Find("SouthStreet"));
            if (!GetPedestrianActivated("West")) GameObject.Destroy(GameObject.Find("WestStreet"));
            if (!GetPedestrianActivated("East")) GameObject.Destroy(GameObject.Find("EastStreet"));
            
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

        public void OnClickScenario1()
        {
            GameObject.Find("InputTramNorthSec").GetComponent<InputField>().text = "120";
            GameObject.Find("InputTramNorthMin").GetComponent<InputField>().text = "30";
            GameObject.Find("InputTramNorthMax").GetComponent<InputField>().text = "60";
            GameObject.Find("InputTramSouthSec").GetComponent<InputField>().text = "180";
            GameObject.Find("InputTramSouthMin").GetComponent<InputField>().text = "30";
            GameObject.Find("InputTramSouthMax").GetComponent<InputField>().text = "50";
            GameObject.Find("InputTramEastSec").GetComponent<InputField>().text = "180";
            GameObject.Find("InputTramEastMin").GetComponent<InputField>().text = "25";
            GameObject.Find("InputTramEastMax").GetComponent<InputField>().text = "60";
            GameObject.Find("InputTramWestSec").GetComponent<InputField>().text = "180";
            GameObject.Find("InputTramWestMin").GetComponent<InputField>().text = "25";
            GameObject.Find("InputTramWestMax").GetComponent<InputField>().text = "60";
            GameObject.Find("ToggleTramNorth").GetComponent<Toggle>().isOn = true;
            GameObject.Find("ToggleTramSouth").GetComponent<Toggle>().isOn = true;
            GameObject.Find("ToggleTramEast").GetComponent<Toggle>().isOn = false;
            GameObject.Find("ToggleTramWest").GetComponent<Toggle>().isOn = false;

            GameObject.Find("InputFieldNorth").GetComponent<InputField>().text = "2";
            GameObject.Find("InputFieldSouth").GetComponent<InputField>().text = "2";
            GameObject.Find("InputFieldWest").GetComponent<InputField>().text = "2";
            GameObject.Find("InputFieldEast").GetComponent<InputField>().text = "2";
        }

        public void OnClickScenario2()
        {
            GameObject.Find("InputTramNorthSec").GetComponent<InputField>().text = "45";
            GameObject.Find("InputTramNorthMin").GetComponent<InputField>().text = "40";
            GameObject.Find("InputTramNorthMax").GetComponent<InputField>().text = "60";
            GameObject.Find("InputTramSouthSec").GetComponent<InputField>().text = "180";
            GameObject.Find("InputTramSouthMin").GetComponent<InputField>().text = "30";
            GameObject.Find("InputTramSouthMax").GetComponent<InputField>().text = "50";
            GameObject.Find("InputTramEastSec").GetComponent<InputField>().text = "180";
            GameObject.Find("InputTramEastMin").GetComponent<InputField>().text = "25";
            GameObject.Find("InputTramEastMax").GetComponent<InputField>().text = "60";
            GameObject.Find("InputTramWestSec").GetComponent<InputField>().text = "180";
            GameObject.Find("InputTramWestMin").GetComponent<InputField>().text = "25";
            GameObject.Find("InputTramWestMax").GetComponent<InputField>().text = "60";
            GameObject.Find("ToggleTramNorth").GetComponent<Toggle>().isOn = true;
            GameObject.Find("ToggleTramSouth").GetComponent<Toggle>().isOn = true;
            GameObject.Find("ToggleTramEast").GetComponent<Toggle>().isOn = false;
            GameObject.Find("ToggleTramWest").GetComponent<Toggle>().isOn = false;

            GameObject.Find("InputFieldNorth").GetComponent<InputField>().text = "4";
            GameObject.Find("InputFieldSouth").GetComponent<InputField>().text = "2";
            GameObject.Find("InputFieldWest").GetComponent<InputField>().text = "2";
            GameObject.Find("InputFieldEast").GetComponent<InputField>().text = "2";
        }

        public void OnClickScenario3()
        {
            GameObject.Find("InputTramNorthSec").GetComponent<InputField>().text = "45";
            GameObject.Find("InputTramNorthMin").GetComponent<InputField>().text = "40";
            GameObject.Find("InputTramNorthMax").GetComponent<InputField>().text = "60";
            GameObject.Find("InputTramSouthSec").GetComponent<InputField>().text = "180";
            GameObject.Find("InputTramSouthMin").GetComponent<InputField>().text = "30";
            GameObject.Find("InputTramSouthMax").GetComponent<InputField>().text = "50";
            GameObject.Find("InputTramEastSec").GetComponent<InputField>().text = "180";
            GameObject.Find("InputTramEastMin").GetComponent<InputField>().text = "25";
            GameObject.Find("InputTramEastMax").GetComponent<InputField>().text = "60";
            GameObject.Find("InputTramWestSec").GetComponent<InputField>().text = "180";
            GameObject.Find("InputTramWestMin").GetComponent<InputField>().text = "25";
            GameObject.Find("InputTramWestMax").GetComponent<InputField>().text = "60";
            GameObject.Find("ToggleTramNorth").GetComponent<Toggle>().isOn = true;
            GameObject.Find("ToggleTramSouth").GetComponent<Toggle>().isOn = true;
            GameObject.Find("ToggleTramEast").GetComponent<Toggle>().isOn = false;
            GameObject.Find("ToggleTramWest").GetComponent<Toggle>().isOn = false;

            GameObject.Find("InputFieldNorth").GetComponent<InputField>().text = "2";
            GameObject.Find("InputFieldSouth").GetComponent<InputField>().text = "2";
            GameObject.Find("InputFieldWest").GetComponent<InputField>().text = "2";
            GameObject.Find("InputFieldEast").GetComponent<InputField>().text = "2";
        }
    
        // Start is called before the first frame update
        void Start()
        {
            _mainCanvas = GameObject.Find("CanvasMain");
            _mainCanvas.SetActive(false);
        }
    }
}