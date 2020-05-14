using System;
using System.Collections;
using System.Collections.Generic;
using MultiAgentSystem;
using UnityEngine;
using UnityEngine.UI;

public class AgentUI : MonoBehaviour
{
    public Agent agent { get; set; }

    private Text _txtAgentName;
    private Text _txtAgentGuid;
    private Text _txtBornAt;
    private Text _txtLivesSince;
    private Text _txtCurrentState;
    private GameObject _panelMessages;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<AgentBody>().agent;
        _txtAgentName = GameObject.Find("TxtAgentName").GetComponent<Text>();
        _txtAgentGuid = GameObject.Find("TxtAgentGuid").GetComponent<Text>();
        _txtBornAt = GameObject.Find("TxtBorn").GetComponent<Text>();
        _txtLivesSince = GameObject.Find("TxtLivesSince").GetComponent<Text>();
        _txtCurrentState = GameObject.Find("TxtCurrentState").GetComponent<Text>();
        _panelMessages = GameObject.Find("PanelMessages");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void ListMessages()
    {
        List<string> messages = new List<string>();

        foreach (Message m in agent.archivedMailbox)
        {
            messages.Add(m.ToString());
        }
        
        foreach (Transform child in _panelMessages.transform)
        {
            Destroy(child.gameObject);
        }
        
        
        
        foreach (string m in messages)
        {
            GameObject txt = Resources.Load("Prefabs/UI/UITextRegular", typeof(GameObject)) as GameObject;
            txt = GameObject.Instantiate(txt, new Vector3(0, 0, 0), txt.transform.rotation);
            txt.GetComponent<Text>().text = m;
            txt.transform.SetParent(_panelMessages.transform);
        }
    }

    void OnMouseOver()
    {
        //if (Input.GetMouseButtonDown(0))
        //{
            _txtAgentName.text = agent.Name;
            _txtAgentGuid.text = agent.AgentId.ToString();
            _txtBornAt.text = "Spawn time : " + agent.SpawnTime.ToString("F2") + " sec";
            _txtLivesSince.text = "Lives since : " + agent.GetSimulationTime().ToString("F2") + " sec";
            _txtCurrentState.text = "Current state : " + agent.StateMachine;
            ListMessages();
        //}
    }
}
