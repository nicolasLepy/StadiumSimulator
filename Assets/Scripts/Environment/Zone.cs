using System.Collections;
using System.Collections.Generic;
using MultiAgentSystem;
using UnityEngine;
using UnityEngine.UI;

public class Zone : MonoBehaviour
{

    [SerializeField]
    private int _zoneNumber;

    public int zoneNumber => _zoneNumber;

    private TextMesh _textZone;
    // Start is called before the first frame update
    void Start()
    {
        _textZone = transform.Find("Indicator").GetComponent<TextMesh>();
    }

    // Update is called once per frame
    void Update()
    {
        _textZone.text = Environment.GetInstance().environment.AvailableSeats(_zoneNumber) + "";
    }
}
