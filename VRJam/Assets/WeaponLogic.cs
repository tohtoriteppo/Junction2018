using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class WeaponLogic : MonoBehaviour {

    public List<string> rightItems;
//    public AudioClip hitSound;
    public float hitWeight = 10.0f;
    private int lifeTime = 600;
    public bool weapon = true;
    private GameObject soundEngine;
    private bool collisionFlag = false;
    // Use this for initialization
    
    void Start ()
    {
        soundEngine = GameObject.FindGameObjectWithTag("SoundEngineTag");
    }
	
	// Update is called once per frame
	void Update () {
		if(!weapon)
        {
            lifeTime--;
            if(lifeTime<0)
            {
                Destroy(gameObject);
            }
        }
	}
    void OnCollisionEnter(Collision collision)
    {
        if(weapon)
        {
            GameObject collided = collision.collider.gameObject;
            if (rightItems.Contains(collision.collider.name.Substring(0, collision.collider.name.Length - 7)))
            {
                collided.GetComponent<Rigidbody>().velocity = (collision.collider.transform.position - transform.position) * 10f;
                if (!collided.GetComponent<WeaponLogic>().collisionFlag)
                {
                    //Call sound
                    //Call hit marker
                    //Call haptics
                    soundEngine.GetComponent<SoundEngineScript>().playGood();
                    transform.parent.GetComponent<RightHandScript>().vibrate(0.07f, 150.0f);
                    collided.GetComponent<WeaponLogic>().collisionFlag = true;
                }

            }
            else
            {
                if (!collided.GetComponent<WeaponLogic>().collisionFlag)
                {
                    //Call sound
                    //Call hit marker
                    //Call haptics
                    soundEngine.GetComponent<SoundEngineScript>().playBad();
                    transform.parent.GetComponent<RightHandScript>().vibrate(0.3f, 90.0f);
                    collided.GetComponent<WeaponLogic>().collisionFlag = true;
                }
            }
            
        }
    }
    public void setUp(bool isWeapon)
    {
        weapon = isWeapon;
        if(isWeapon)
        {
            GetComponent<Rigidbody>().isKinematic = true;
        }
        else
        {
            GetComponent<Rigidbody>().isKinematic = false;
        }
    }
}
