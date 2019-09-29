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

        if (currentStep >= (markers.Length-1))
        {
            Debug.Log("Error");
            currentStep++;
            return 1;
        }
        
        for(int i = 0; i < markers.Length; i++)
        {
            Debug.Log("Loop x2");
           markers[i].SetActive(false);
        }

        if((currentStep) != ((markers.Length)-1))
        {
            Debug.Log("Setting marker active");
           markers[currentStep + 1].SetActive(true);
        }
        currentStep++;
        return 0;
    }

    public int Back()
    {
        Debug.Log("CURR STEP: " + currentStep);
        if (currentStep == 0)
        {
            currentStep--;
            return 1;
        }
        else if(currentStep > 1)
        {
            currentStep--;
            return 0;
        }
        else
        {
             Debug.Log("INFO: Back - pos: " + (currentStep+1));
                for (int i=0; i<markers.Length; ++i)
                {
                    markers[i].SetActive(false);
                }
                if((currentStep) != 0)
                {
                     Debug.Log("Setting marker active");
                    markers[currentStep - 1].SetActive(true);
                }
                currentStep -= 1;
    

                return 0;
        }
        
    }
}
