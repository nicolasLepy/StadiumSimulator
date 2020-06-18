using MultiAgentSystem;
using UnityEngine;
using UnityEngine.UI;

public class MoveCamera : MonoBehaviour
{
    private bool _fpsView;
    private Vector3 _oldPosition;

    private Quaternion _oldRotation;
    // Start is called before the first frame update
    void Start()
    {
        _fpsView = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.E))
        {
            Environment.GetInstance().Brain.ReactivateAllAgents();
        }
        
        if (Input.GetKey(KeyCode.Space))
        {
            Camera.main.transform.localPosition += new Vector3(0, 0.3f, 0);
        }
        if (Input.GetKey(KeyCode.Backspace))
        {
            Camera.main.transform.localPosition += new Vector3(0, -0.3f, 0);
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            _fpsView = !_fpsView;
            if (_fpsView)
            {
                _oldPosition = Camera.main.transform.position;
                _oldRotation = Camera.main.transform.rotation;
                string guid = GameObject.Find("TxtAgentGuid").GetComponent<Text>().text;
                foreach (var agent in Environment.GetInstance().Brain.Agents)
                {
                    if (guid == agent.Value.AgentId.ToString())
                    {
                        Camera.main.transform.SetParent(agent.Value.Body.transform);
                        Camera.main.transform.localPosition = new Vector3(0, 1, -2);
                        Camera.main.transform.localRotation = Quaternion.Euler(0, 20, 0);
                    }
                }
            }
            else
            {
                Camera.main.transform.SetParent(null);
                Camera.main.transform.position = _oldPosition;
                Camera.main.transform.rotation = _oldRotation;
            }
        }
        
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        
        var movement = new Vector3(moveHorizontal, 0f, moveVertical);
        
        transform.Translate(movement);
    }
}