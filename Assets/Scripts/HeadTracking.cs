using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.MagicLeap;
using UnityEngine.UI;


public class HeadTracking : MonoBehaviour
{
    public Camera Cam;

   // public Image Cursor; //parent object to move
    public GameObject Cursor;


    public Image dial; // image of the dial portion

    public AudioClip click;
    AudioSource source;

    public float VOL = 1.7f;

    public float speed = 12f;

    public float elapsed;

    public float timerSpeed =.85f;

    public int missCount = 0;

    public float pause = 0; //this value forces a wait after click *Assigned after click to allow normal behavior on startup*

    public int threshold = 3;
    
    #region Unity Methods

    public GameObject ErrorObject;
    public ErrorHandler handler;

    public GameObject MissionObj;
    public MissionPanel Mission;

    public GameObject PartViewObj;
    public PartViewer part;


    public Material Idle;
    public Material Hover;

    public GameObject Right;
    public GameObject Left;

    bool PartFlag; //used to tell if we should be rotating an object continuously.
    private float duration = 1.0f;
    void Start()
    {
        handler = ErrorObject.GetComponent<ErrorHandler>();
        Mission = MissionObj.GetComponent<MissionPanel>();
        part = PartViewObj.GetComponent<PartViewer>();

        source = GetComponent<AudioSource>();
    }

    void Update()
    {
        RaycastHit hit;

        //float lerp = Mathf.PingPong(Time.time, duration) / duration;
        float lerp = 0;

        if (Physics.Raycast(Cam.transform.position, Cam.transform.forward, out hit))
        {
            if (hit.collider.name == "PannelR" )
            {
                Cursor.transform.position = Vector3.Slerp(Cursor.transform.position, hit.point, speed); //Updates the position of the cursor to show object collided with 
                Cursor.transform.rotation = Quaternion.Slerp(Cursor.transform.rotation, Cam.transform.rotation, speed);
                Cursor.SetActive(true);
                Right.GetComponent<MeshRenderer>().material.Lerp( Hover,Idle, lerp);
                missCount = 0; //reset the miss count becuase we have hit the object 
                if (elapsed > pause)
                {
                    
                    if (dial.fillAmount != 1f) //if we have not clicked Start animation
                    {
                        
                        dial.fillAmount = dial.fillAmount + Time.deltaTime * speed;
                    }
                    else //we have clicked 
                    {
                        //Right.GetComponent<MeshRenderer>().material.Lerp( Idle,Hover, 0f);
                        Debug.Log("Right Pressed\n");
                        source.PlayOneShot(click,VOL);
                        Mission.IncTask(); //counting up 
                        pause = .5f; //setting our delay
                        dial.fillAmount = 0; //reset the dial amount
                        elapsed = 0;
                    }
                }

                elapsed += Time.deltaTime; //checking number of seconds elapsed

           
            }
            else if (hit.collider.name == "PannelL")
            {
                Cursor.transform.position = Vector3.Slerp(Cursor.transform.position, hit.point, speed); //Updates the position of the cursor to show object collided with 
                Cursor.transform.rotation = Quaternion.Slerp(Cursor.transform.rotation, Cam.transform.rotation, speed);
                Cursor.SetActive(true);
                Left.GetComponent<MeshRenderer>().material.Lerp(Hover,Idle,  lerp);
                missCount = 0; //reset the miss count becuase we have hit the object 
                if (elapsed > pause)
                {
                    
                    if (dial.fillAmount != 1f) //if we have not clicked Start animation
                    {
                        dial.fillAmount = dial.fillAmount + Time.deltaTime * speed;
                    }
                    else //we have clicked 
                    {
                        //Left.GetComponent<MeshRenderer>().material.Lerp( Idle,Hover, 0f);
                        source.PlayOneShot(click,VOL);
                        Debug.Log("Backward Pressed\n");
                        Mission.DecTask(); //counting up 
                        pause = .5f; //setting our delay
                        dial.fillAmount = 0; //reset the dial amount
                        elapsed = 0;
                    }
                }

                elapsed += Time.deltaTime; //checking number of seconds elapsed
            }
            else if (hit.collider.name == "Error")
            {
                if (handler.windowActive == true)
                {
                    Cursor.transform.position = Vector3.Slerp(Cursor.transform.position, hit.point, speed); //Updates the position of the cursor to show object collided with 
                    Cursor.transform.rotation = Quaternion.Slerp(Cursor.transform.rotation, Cam.transform.rotation, speed);
                    Cursor.SetActive(true);
                     missCount = 0; //reset the miss count becuase we have hit the object 
                    if (elapsed > pause)
                    {
                    
                        if (dial.fillAmount != 1f) //if we have not clicked Start animation
                        {
                            dial.fillAmount = dial.fillAmount + Time.deltaTime * speed;
                        }
                        else //we have clicked 
                        {
                            source.PlayOneShot(click,VOL);
                            Debug.Log("Error Pressed\n");
                            handler.CloseErrorWindow();
                            pause = .5f; //setting our delay
                            dial.fillAmount = 0; //reset the dial amount
                            elapsed = 0;
                        }
                    }

                elapsed += Time.deltaTime; //checking number of seconds elapsed
                }
            } 
            else if (hit.collider.name == "LEFT")
            {
                Cursor.transform.position = Vector3.Slerp(Cursor.transform.position, hit.point, speed); //Updates the position of the cursor to show object collided with 
                Cursor.transform.rotation = Quaternion.Slerp(Cursor.transform.rotation, Cam.transform.rotation, speed);
                Cursor.SetActive(true);
                missCount = 0; //reset the miss count becuase we have hit the object 
                if(PartFlag)
                {
                    part.Movement(0);
                }
                else if (elapsed > pause)
                {
                    if (dial.fillAmount != 1f) //if we have not clicked Start animation
                    {
                        dial.fillAmount = dial.fillAmount + Time.deltaTime * speed;
                    }
                    else //we have clicked 
                    {
                        PartFlag = true;
                        source.PlayOneShot(click,VOL);
                        Debug.Log("Left Pressed\n");
                        part.Movement(0);
                        pause = .5f; //setting our delay
                        dial.fillAmount = 0; //reset the dial amount
                        elapsed = 0;
                    }
                }

                elapsed += Time.deltaTime; //checking number of seconds elapsed
            }
            else if (hit.collider.name == "RIGHT")
            {
                Cursor.transform.position = Vector3.Slerp(Cursor.transform.position, hit.point, speed); //Updates the position of the cursor to show object collided with 
                Cursor.transform.rotation = Quaternion.Slerp(Cursor.transform.rotation, Cam.transform.rotation, speed);
                Cursor.SetActive(true);
                missCount = 0; //reset the miss count becuase we have hit the object 
                if(PartFlag)
                {
                    part.Movement(1);
                }
                else if (elapsed > pause)
                {
                    if (dial.fillAmount != 1f) //if we have not clicked Start animation
                    {
                        dial.fillAmount = dial.fillAmount + Time.deltaTime * speed;
                    }
                    else //we have clicked 
                    {
                        PartFlag = true;
                        source.PlayOneShot(click,VOL);
                        Debug.Log("Right Pressed\n");
                        part.Movement(1);
                        pause = .5f; //setting our delay
                        dial.fillAmount = 0; //reset the dial amount
                        elapsed = 0;
                    }
                }
                elapsed += Time.deltaTime; //checking number of seconds elapsed
                
            }
            else if (hit.collider.name == "UP")
            {
                Cursor.transform.position = Vector3.Slerp(Cursor.transform.position, hit.point, speed); //Updates the position of the cursor to show object collided with 
                Cursor.transform.rotation = Quaternion.Slerp(Cursor.transform.rotation, Cam.transform.rotation, speed);
                Cursor.SetActive(true);
                missCount = 0; //reset the miss count becuase we have hit the object 
                if(PartFlag)
                {
                    part.Movement(2);
                }
                else if (elapsed > pause)
                {
                    if (dial.fillAmount != 1f) //if we have not clicked Start animation
                    {
                        dial.fillAmount = dial.fillAmount + Time.deltaTime * speed;
                    }
                    else //we have clicked 
                    {
                        PartFlag = true;
                        source.PlayOneShot(click,VOL);
                        Debug.Log("Up Pressed\n");
                        part.Movement(2);
                        pause = .5f; //setting our delay
                        dial.fillAmount = 0; //reset the dial amount
                        elapsed = 0;
                    }
                }
                elapsed += Time.deltaTime; //checking number of seconds elapsed
            }
            else if (hit.collider.name == "DOWN")
            {
                Cursor.transform.position = Vector3.Slerp(Cursor.transform.position, hit.point, speed); //Updates the position of the cursor to show object collided with 
                Cursor.transform.rotation = Quaternion.Slerp(Cursor.transform.rotation, Cam.transform.rotation, speed);
                Cursor.SetActive(true);
                missCount = 0; //reset the miss count becuase we have hit the object 
                if(PartFlag)
                {
                    part.Movement(3);
                }
                else if (elapsed > pause)
                {
                    if (dial.fillAmount != 1f) //if we have not clicked Start animation
                    {
                        dial.fillAmount = dial.fillAmount + Time.deltaTime * speed;
                    }
                    else //we have clicked 
                    {
                        PartFlag = true;
                        source.PlayOneShot(click,VOL);
                        Debug.Log("Down Pressed\n");
                        part.Movement(3);
                        pause = .5f; //setting our delay
                        dial.fillAmount = 0; //reset the dial amount
                        elapsed = 0;
                    }
                }
                elapsed += Time.deltaTime; //checking number of seconds elapsed
            }
            else if (hit.collider.name == "RESET")
            {
                Cursor.transform.position = Vector3.Slerp(Cursor.transform.position, hit.point, speed); //Updates the position of the cursor to show object collided with 
                Cursor.transform.rotation = Quaternion.Slerp(Cursor.transform.rotation, Cam.transform.rotation, speed);
                Cursor.SetActive(true);
                missCount = 0; //reset the miss count becuase we have hit the object 
                if (elapsed > pause)
                {
                    if (dial.fillAmount != 1f) //if we have not clicked Start animation
                    {
                        dial.fillAmount = dial.fillAmount + Time.deltaTime * speed;
                    }
                    else //we have clicked 
                    {
                        PartFlag = true;
                        source.PlayOneShot(click,VOL);
                        Debug.Log("Reset Pressed\n");
                        part.Movement(4);
                        pause = .5f; //setting our delay
                        dial.fillAmount = 0; //reset the dial amount
                        elapsed = 0;
                    }
                }
                elapsed += Time.deltaTime; //checking number of seconds elapsed
            }
            else
            {
                if (missCount < threshold)
                {

                    missCount++;
                }
                else
                {
                    PartFlag = false;
                    Cursor.SetActive(false);
                    Right.GetComponent<MeshRenderer>().material.Lerp( Idle,Hover, lerp);
                    Left.GetComponent<MeshRenderer>().material.Lerp( Idle,Hover, lerp);
                    dial.fillAmount = 0;
                    missCount = 0;
                    elapsed = 0;
                    pause = .25f;
                }
            }
        }
        else
        {
            if (missCount < threshold)
            {

                missCount++;
            }
            else
            {
                PartFlag = false;
                Cursor.SetActive(false);
                Right.GetComponent<MeshRenderer>().material.Lerp(Idle, Hover, lerp);
                Left.GetComponent<MeshRenderer>().material.Lerp(Idle, Hover, lerp);
                dial.fillAmount = 0;
                missCount = 0;
                elapsed = 0;
                pause = .25f;
            }
        }
        #endregion
    }
}