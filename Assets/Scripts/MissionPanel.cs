using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro; 
public class MissionPanel : MonoBehaviour {

    public TextMeshProUGUI Task1;
    public TextMeshProUGUI Task2;
    public TextMeshProUGUI Task3;



    private int taskNum;

    bool taskFlag = false; //used to check if the task program has been checked (Helps performance)
    int prevTask; 

    // Use this for initialization
    void Start()
    {
        taskNum = 0;
        prevTask = -1;

        Task1.color = Color.green;
        Task2.color = Color.white;
        Task3.color = Color.white;
    }
      
    public void IncTask()
    {
        if (taskNum < 3)
        {
           if(taskNum == 0)
           {
                Task1.color = Color.gray;
                Task1.fontStyle = FontStyles.Strikethrough;
                Task2.color = Color.green;
                Task2.fontStyle = FontStyles.Bold;
                Task3.color = Color.white;
                taskNum++;
           }
           else if(taskNum == 1)
           {
                Task1.color = Color.gray;
                Task1.fontStyle = FontStyles.Strikethrough;
                Task2.color = Color.gray;
                Task2.fontStyle = FontStyles.Strikethrough;
                Task3.color = Color.green;
                Task3.fontStyle = FontStyles.Bold;
                taskNum++;
           }
            else if(taskNum == 2)
            {
                Task1.color = Color.gray;
                Task1.fontStyle = FontStyles.Strikethrough;
                Task2.color = Color.gray;
                Task2.fontStyle = FontStyles.Strikethrough;
                Task3.color = Color.gray;
                Task3.fontStyle = FontStyles.Strikethrough;
                taskNum++;
            }
        }
        else
        {
            //Just play the audio
        }
	}

    public void DecTask()
    {
       // imageScript.ClearScreen();
       if (taskNum > 0)
       {
            if(taskNum == 1)
           {
                Task1.color = Color.green;
                Task1.fontStyle = FontStyles.Bold;
                Task2.color = Color.white;
                Task3.color = Color.white;
                taskNum++;
           }
           else if(taskNum == 2)
           {
                Task1.color = Color.gray;
                Task1.fontStyle = FontStyles.Strikethrough;
                Task2.color = Color.green;
                Task2.fontStyle = FontStyles.Bold;
                Task3.color = Color.white;
                taskNum++;
           }
            else if(taskNum == 3)
            {
                Task1.color = Color.gray;
                Task1.fontStyle = FontStyles.Strikethrough;
                Task2.color = Color.gray;
                Task2.fontStyle = FontStyles.Strikethrough;
                Task3.color = Color.green;
                Task3.fontStyle = FontStyles.Bold;
                taskNum++;
            }
       }       
    }
}
