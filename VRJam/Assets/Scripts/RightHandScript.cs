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

    private float accumulatedAngle = 0.0f;
    private float prevAngle = 0.0f;

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
            float currentAngle = getAngle(SteamVR_Input._default.inActions.TouchpadCoordinates.GetAxis(hand));
            weaponUI.SetActive(true);
            menuFlag = true;
            menuOpenedTime = Time.time;

            accumulatedAngle += currentAngle - prevAngle;
            Debug.Log(accumulatedAngle);
            
            if (accumulatedAngle >= Mathf.PI/4)
            {
                accumulatedAngle = 0;
                prevAngle = 0;
                weaponUI.GetComponent<weaponMenuManager>().rotate(false);
                chooseWeapon();
            }
            else if (accumulatedAngle <= -Mathf.PI/4)
            {
                accumulatedAngle = 0;
                prevAngle = 0;
                weaponUI.GetComponent<weaponMenuManager>().rotate(true);
                chooseWeapon();
            }
            prevAngle = currentAngle;
        }

        if (menuOpenedTime + menuTimeout < Time.time)
        {
            //Call menu closed
            chooseWeapon();
            weaponUI.SetActive(false);
            menuFlag = false;
        }

        /*
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
            

        } */

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

    float getAngle(Vector2 xy)
    {
        float x = xy.x;
        float y = xy.y;
        float ret = 0.0f;
        if (x == 0)
        {
            ret = y > 0 ? Mathf.PI / 2 : Mathf.PI * 3 / 2;
        }
        else if (x > 0)
        {
            ret = y > 0 ? Mathf.Atan(y / x) : 2*Mathf.PI + Mathf.Atan(y / x);
        }
        else
        {
            ret = y > 0 ? Mathf.PI + Mathf.Atan(y / x) : Mathf.PI + Mathf.Atan(y / x);
        }

//        ret = x == 0 ? (y > 0 ? Mathf.PI / 2 : Mathf.PI * 3 / 2) : (x > 0 ? (y > 0 ? Mathf.Atan(y / x) : 2 * Mathf.PI - Mathf.Atan(y / x)) : (y > 0 ? Mathf.PI - Mathf.Atan(y / x) : Mathf.PI + Mathf.Atan(y / x)));

        return ret;
    }
}
