using System;
using UnityEngine;
using UnityEngine.XR.MagicLeap;
public class HeadLockScript : MonoBehaviour
{ 

    #region Public Variables
    public GameObject Camera;
    public GameObject obj;

    public Vector3 Difference;
    public Quaternion DefaultRot;

    public AudioClip reset;
    AudioSource source;

    public float VOL = 1.7f;

    public bool buttonPressed = false;

    public bool UsingEmulator= false;

    private ControllerConnectionHandler _controllerConnectionHandler;

    #endregion

    public void Start()
    {
        _controllerConnectionHandler = GetComponent<ControllerConnectionHandler>();
        MLInput.OnTriggerDown += HandleOnTriggerDown;
        source = GetComponent<AudioSource>();
    }

    #region Private Methods
    public void Update()
    {
        if (UsingEmulator == true)  //For testing purpose only!
        {
            obj.transform.position = Camera.transform.position;
            obj.transform.rotation= Camera.transform.rotation;
        }
        else if (_controllerConnectionHandler.IsControllerValid())
        {
            MLInputController controller = _controllerConnectionHandler.ConnectedController;

            Difference = (Camera.transform.position - controller.Position);

            if(buttonPressed) //dont worry about this. It will be fixed with the accelerometer
            {
               //Debug.Log("Button Pressed");
               DefaultRot = controller.Orientation;
               Difference = (Camera.transform.position - controller.Position);
               obj.transform.rotation = Camera.transform.rotation;
               buttonPressed = false;
            }
            float speed = Time.deltaTime * 12.0f;

            if (controller.Type == MLInputControllerType.Control)
            {
                // For Control, raw input is enough
                Vector3 temp = controller.Position + Difference;
                    
                obj.transform.position = Vector3.Slerp(obj.transform.position, temp, speed);
                    
                Quaternion rot = (controller.Orientation * Quaternion.Inverse(DefaultRot));
                obj.transform.rotation = Quaternion.Slerp(obj.transform.rotation, rot, speed);
            }
        }
    }
    #endregion

    #region Event Handlers
    /// <summary>
    /// Handles the event for trigger down.
    /// </summary>
    /// <param name="controller_id">The id of the controller.</param>
    /// <param name="value">The value of the trigger button.</param>
    private void HandleOnTriggerDown(byte controllerId, float value)
    {
        MLInputController controller = _controllerConnectionHandler.ConnectedController;
        if (controller != null && controller.Id == controllerId)
        {
            source.PlayOneShot(reset,VOL);
            MLInputControllerFeedbackIntensity intensity = (MLInputControllerFeedbackIntensity)((int)(value * 2.0f));
            controller.StartFeedbackPatternVibe(MLInputControllerFeedbackPatternVibe.Buzz, intensity);
            buttonPressed = true;
        }
    }
    #endregion
}