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


        public List<int> GetTicketsOfficesNumber()
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
        
        public void OnClickButtonStartSimulation()
        {
            _mainCanvas.SetActive(true);
            Environment.GetInstance().CreateBrain(GetTicketsOfficesNumber());
            GameObject.Find("CanvasSettings").SetActive(false);

            GameObject.Find("Environment").AddComponent<EnvironmentUI>();
            gameObject.AddComponent<MultiAgentSystem>();
            
            foreach (SpectatorSpawner sp in GameObject.FindObjectsOfType<SpectatorSpawner>())
            {
                sp.Activate();
            }
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
