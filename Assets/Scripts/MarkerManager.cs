using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarkerManager : MonoBehaviour
{
    public int currentStep = 1;
    public GameObject marker1;
    public GameObject marker2;

    // Start is called before the first frame update
    void Awake()
    {
    }

    void Start()
    {
        //Debug.Log("INFO: Marker Manager started");
        marker1.SetActive(true);
        marker2.SetActive(true);
    }

    int Next()
    {
        if (currentStep == 2)
        {
            return 1;
        } else
        {
            marker1.SetActive(false);
            marker2.SetActive(true);
            currentStep += 1;
        }
        return 0;
    }

    int Back()
    {
        if (currentStep == 1)
        {
            return 1;
        } else
        {
            marker1.SetActive(true);
            marker2.SetActive(false);
            currentStep -= 1;
        }

        return 0;
    }
}
