using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;


public class RightHandScript : MonoBehaviour {

    public SteamVR_Action_Boolean touchpadTouch;
    public SteamVR_Action_Boolean triggerClick;
    public SteamVR_Action_Boolean touchpadClick;
    public SteamVR_Action_Single triggerPull;
    public SteamVR_Action_Vector2 touchpadCoordinates;
    public SteamVR_Action_Boolean squeeze;
    public SteamVR_Action_Vibration haptic;

    public float menuTimeout = 1.0f; //Time after which the menu closes
    public float menuScrollModifier = 1.0f; // 0 - 1
    public float deadZoneX = 1.0f / 2;

    public GameObject kuutio;
    private bool menuFlag = false;
    private float menuOpenedTime = 0.0f;
    private float menuScrollTimer = 0.0f;

    // Use this for initialization
    void Start () {
    }
	
	// Update is called once per frame
	void Update ()
    {
        
        if (SteamVR_Input._default.inActions.TouchpadTouch.GetState(SteamVR_Input_Sources.RightHand))
        {
            menuFlag = true;
            menuOpenedTime = Time.time;
        }

        if (menuOpenedTime + menuTimeout < Time.time)
        {
            menuFlag = false;
        }

        float touchpadX = SteamVR_Input._default.inActions.TouchpadCoordinates.GetAxis(SteamVR_Input_Sources.RightHand).x;

        if (Mathf.Abs(touchpadX) > deadZoneX)
        {
            float posFromCenter = touchpadX < 0 ? touchpadX + deadZoneX : touchpadX - deadZoneX; //Value between -1 to 1
            if (menuScrollTimer + 0.7f - posFromCenter / (1 - deadZoneX) * 0.6f <= Time.time)
            {
                kuutio.transform.Rotate(new Vector3(0.0f, 15.0f, 0.0f), Space.World);
                menuScrollTimer = Time.time;
            }

        }

    }
}
