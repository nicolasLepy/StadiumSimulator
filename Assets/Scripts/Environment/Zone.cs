using System;
using System.Collections.Generic;
using UnityEngine;
using Environment = MultiAgentSystem.Environment;

public class Zone : MonoBehaviour
{
    [SerializeField]
    private int _zoneNumber = 0;

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

    /// <summary>
    /// Get a list of places locations in the tribunes
    /// </summary>
    /// <returns></returns>
    public List<Vector3> GetPlacesLocations()
    {
        List<Vector3> res = new List<Vector3>();
        float startX = -10000;
        float startZ = -10000;
        float endX = -10000;
        float endZ = -10000;

        Vector3 min = transform.GetComponent<Renderer>().bounds.min;
        Vector3 max = transform.GetComponent<Renderer>().bounds.max;
        startX = min.x;
        startZ = min.z;
        endX = max.x;
        endZ = max.z;

        int stepXNumber = (int)(Math.Abs(startX - endX) / 4.0f);
        int stepZNumber = (int)(Math.Abs(startZ - endZ) / 4.0f);
        float stepX = Math.Abs(startX - endX)/(stepXNumber + 0.0f);
        float stepZ = Math.Abs(startZ - endZ)/(stepZNumber + 0.0f);
        
        for (int i = 0; i < stepXNumber; i++)
        {
            for (int j = 0; j < stepZNumber; j++)
            {
                Vector3 place = new Vector3(startX + i*stepX, transform.position.y, startZ + j*stepZ);
                res.Add(place);
            }
        }
        
        return res;
    }
}