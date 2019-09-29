using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarkerManager : MonoBehaviour
{
    public int currentStep = 0;

    public GameObject[] markers;

    // Start is called before the first frame update
    void Awake()
    {

    }

    void Start()
    {

        Debug.Log("INFO: Marker Manager started");
        foreach (GameObject obj in markers)
        {
            obj.SetActive(false);
        }

        markers[0].SetActive(true);

        currentStep = 0;
    }

    public int Next()
    {
        Debug.Log("VAL 1: " + currentStep + " VAL 2: " + markers.Length);

        if (currentStep == (markers.Length))
        {
            return 1;
        } else
        
        for(int i = 0; i < markers.Length; i++)
        {
           markers[i].SetActive(false);
        }
        if(markers[currentStep+1] != null)
        {
           markers[currentStep + 1].SetActive(true);
        }
        currentStep++;
        return 0;
    }

    public int Back()
    {
        if (currentStep == 0)
        {
            return 1;
        }
        else
        {
            Debug.Log("INFO: Back - pos: " + currentStep+1);
            for (int i=0; i<markers.Length; ++i)
            {
                markers[i].SetActive(false);
            }
            if(markers[currentStep-1] != null)
            {
                markers[currentStep - 1].SetActive(true);
            }
            currentStep -= 1;
        }

        return 0;
    }
}
