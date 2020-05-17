using System.Collections;
using System.Collections.Generic;
using MultiAgentSystem;
using UnityEngine;
using UnityEngine.UI;

public class EnvironmentUI : MonoBehaviour
{
    
    
    private GameObject _panelSeats;
    
    // Start is called before the first frame update
    void Start()
    {

        _panelSeats = GameObject.Find("RightPanel");

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
    
    // Update is called once per frame
    void Update()
    {
        ListSeats();
    }
}
