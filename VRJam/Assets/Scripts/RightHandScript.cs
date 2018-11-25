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
    public GameObject weaponUI;

    public float menuTimeout = 1.0f; //Time after which the menu closes
    public float deadZoneX = 1.0f / 2;
    public bool isRight;
    public string weaponFolder;
    
    private bool menuFlag = false;
    private float menuOpenedTime = 0.0f;
    private float menuScrollTimer = 0.0f;
    private SteamVR_Input_Sources hand;
    private string weaponInHand;
    private float scrollCounter = 0;
    private float scrollLimit = 10f;

    private Dictionary<string, GameObject> weapons;

    // Use this for initialization
    void Start () {
        loadWeapons();
        if(isRight)
        {
            hand = SteamVR_Input_Sources.RightHand;
        }
        else
        {
            hand = SteamVR_Input_Sources.LeftHand;
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (SteamVR_Input._default.inActions.TouchpadTouch.GetState(hand))
        {
            //Call menu open
            weaponUI.SetActive(true);
            menuFlag = true;
            menuOpenedTime = Time.time;
        }

        if (menuOpenedTime + menuTimeout < Time.time)
        {
            //Call menu closed
            chooseWeapon();
            weaponUI.SetActive(false);
            menuFlag = false;
        }

        //Raw touchpad X coordinate
        float touchpadX = SteamVR_Input._default.inActions.TouchpadCoordinates.GetAxis(hand).x;

        if (Mathf.Abs(touchpadX) > deadZoneX)
        {
            float posFromCenter = touchpadX < 0 ? Mathf.Abs(touchpadX + deadZoneX) : touchpadX - deadZoneX; //Value between -1 to 1
            //call touch visualizer
            //scrollMenu(posFromCenter);
            
            if (menuScrollTimer + 0.7f - posFromCenter / (1 - deadZoneX) * 0.6f <= Time.time)
            {
                menuScrollTimer = Time.time;
                //Call menu scroller
                if(touchpadX > 0)
                {
                    weaponUI.GetComponent<weaponMenuManager>().rotate(true);
                }
                else
                {
                    weaponUI.GetComponent<weaponMenuManager>().rotate(false);
                }
                chooseWeapon();
            }
            

        }

    }
    void chooseWeapon()
    {
        weapons[weaponInHand].SetActive(false);
        weaponInHand = weaponUI.GetComponent<weaponMenuManager>().getSelectedWeapon();
        
        weapons[weaponInHand].SetActive(true);
    }
    void loadWeapons()
    {
        weapons = new Dictionary<string, GameObject>();
        Object[] objs = Resources.LoadAll(weaponFolder, typeof(Object));
        foreach (Object obj in objs)
        {
            weaponInHand = obj.name;
            weapons.Add(weaponInHand, Instantiate(obj,transform) as GameObject);
            weapons[weaponInHand].transform.parent = transform;
            weapons[weaponInHand].transform.localPosition = Vector3.zero;
            weapons[weaponInHand].GetComponent<WeaponLogic>().setUp(true);
            weapons[weaponInHand].SetActive(false);
        }
        weapons[weaponInHand].SetActive(true);
    }
    void scrollMenu(float dir)
    {
        if(dir>0 && scrollCounter < 0)
        {
            scrollCounter = 0;
        }
        else if (dir < 0 && scrollCounter > 0)
        {
            scrollCounter = 0;
        }
        scrollCounter += dir;
        if(scrollCounter > scrollLimit)
        {
            weaponUI.GetComponent<weaponMenuManager>().rotate(true);
        }
        else if (scrollCounter < scrollLimit)
        {
            weaponUI.GetComponent<weaponMenuManager>().rotate(true);
        }

    }
    public void vibrate(float time, float frequency)
    {
        haptic.Execute(0.01f, time, frequency, 1.0f, hand);
    }
}
