using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;


public class LeftHandScript : MonoBehaviour
{

    public SteamVR_Action_Boolean touchpadTouch;
    public SteamVR_Action_Boolean triggerClick;
    public SteamVR_Action_Boolean touchpadClick;
    public SteamVR_Action_Single triggerPull;
    public SteamVR_Action_Vector2 touchpadCoordinates;
    public SteamVR_Action_Boolean squeeze;
    public SteamVR_Action_Vibration haptic;

    public float menuTimeout = 1.0f; //Time after which the menu closes

    private bool menuFlag = false;
    private float menuTime = 0.0f;
    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {


        if (SteamVR_Input._default.inActions.TouchpadTouch.GetStateDown(SteamVR_Input_Sources.LeftHand))
            menuFlag = true;

        if (SteamVR_Input._default.inActions.TouchpadTouch.GetState(SteamVR_Input_Sources.LeftHand))
        {
            menuFlag = true;
            menuTime = Time.time;
        }
        
        if (menuTime + menuTimeout > Time.time)
        {
            menuFlag = false;
        }


    }
}
