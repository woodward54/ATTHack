using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarkerManager : MonoBehaviour
{
    private int currentStep = 0;

    public GameObject[] markers;

    // Start is called before the first frame update
    void Awake()
    {

    }

    void Start()
    {
        //Debug.Log("INFO: Marker Manager started");
        foreach (GameObject obj in markers)
        {
            obj.SetActive(false);
        }

        markers[0].SetActive(true);
    }

    public int Next()
    {
        if (currentStep == markers.Length)
        {
            return 1;
        } else
        {
            Debug.Log("INFO: Next - pos: " + currentStep+1);
            for (int i=markers.Length; i>0; --i)
            {
                if (markers[i].activeInHierarchy)
                {
                    markers[i + 1].SetActive(true);
                }
                markers[i].SetActive(false);
            }
            currentStep += 1;
        }
        return 0;
    }

    public int Back()
    {
        if (currentStep == 0)
        {
            return 1;
        } else
        {
            Debug.Log("INFO: Back - pos: " + currentStep+1);
            for (int i=0; i<markers.Length; ++i)
            {
                if (markers[i].activeInHierarchy)
                {
                    markers[i - 1].SetActive(true);
                }
                markers[i].SetActive(false);
            }
            currentStep -= 1;
        }

        return 0;
    }
}
