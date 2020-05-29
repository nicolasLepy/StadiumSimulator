using System.Collections;
using System.Collections.Generic;
using MultiAgentSystem;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Windows.Speech;

public class EnvironmentUI : MonoBehaviour
{
    
    
    private GameObject _panelSeats;

    private float _avgInstallationTime;
    private Dictionary<AgentTicketOffice, float> _ticketOfficesTimes;
    private int _updateTime;
    private int _updateCycle;

    // Start is called before the first frame update
    void Start()
    {
        _updateCycle = 200;
        _updateTime = 0;
        _ticketOfficesTimes = new Dictionary<AgentTicketOffice, float>();


        foreach (var a in Environment.GetInstance().Brain.Agents)
        {
            AgentTicketOffice ato = a.Value as AgentTicketOffice;
            if (ato != null)
            {
                _ticketOfficesTimes.Add(ato, 0);
            }
        }


        _panelSeats = GameObject.Find("RightPanel");

    }

    private void AddText(string content)
    {
        GameObject txt = Resources.Load("Prefabs/UI/UITextRegular", typeof(GameObject)) as GameObject;
        txt = GameObject.Instantiate(txt, new Vector3(0, 0, 0), txt.transform.rotation);
        txt.GetComponent<Text>().text = content;
        txt.transform.SetParent(_panelSeats.transform);

    }
    
    private void ListSeats()
    {
        List<string> texts = new List<string>();

        for (int i = 1; i <= Environment.GetInstance().environment.CategoriesNumber; i++)
        {
            texts.Add("Cat " + i + " : " + Environment.GetInstance().environment.AvailableSeats(i));
        }
    
        foreach (Transform child in _panelSeats.transform)
        {
            Destroy(child.gameObject);
        }

        foreach (string m in texts)
        {
            GameObject txt = Resources.Load("Prefabs/UI/UITextRegular", typeof(GameObject)) as GameObject;
            txt = GameObject.Instantiate(txt, new Vector3(0, 0, 0), txt.transform.rotation);
            txt.GetComponent<Text>().text = m;
            txt.transform.SetParent(_panelSeats.transform);
        }
    }

    private void ListStats()
    {

        if (_updateTime == _updateCycle)
        {
            foreach (var a in Environment.GetInstance().Brain.Agents)
            {
                AgentTicketOffice ato = a.Value as AgentTicketOffice;
                if (ato != null)
                {
                    float avgTime = 0;
                    int count = 0;
                    foreach (float f in ato.times)
                    {
                        count++;
                        avgTime += f;
                    }
                    
                    if(count > 0)
                        avgTime = avgTime / (count+0.0f);

                    _ticketOfficesTimes[ato] = avgTime;
                }
            }

            
            
            _avgInstallationTime = 0f;
            List<float> times = Environment.GetInstance().Brain.timesToSitInStadium;
            foreach (float f in times)
                _avgInstallationTime += f;
            _avgInstallationTime = _avgInstallationTime / times.Count;
        }

        foreach (var a in _ticketOfficesTimes)
        {
            AddText(a.Key.Name + " : " + a.Value);
        }
        
        AddText("Avg time to sit in stadium : " + _avgInstallationTime);
    }
    
    // Update is called once per frame
    void Update()
    {
        _updateTime++;
        ListSeats();
        ListStats();
        if (_updateTime == _updateCycle)
            _updateTime = 0;
    }
}
