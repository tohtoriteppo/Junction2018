using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class WeaponLogic : MonoBehaviour {

    public List<string> rightItems;
    //    public AudioClip hitSound;
    public float hitMag = 1f;
    public float hitWeight = 10.0f;
    private int lifeTime = 600;
    public bool weapon = true;
    private GameObject soundEngine;
    private bool collisionFlag = false;
    public bool fenceStamp = false;
    public float value = 0f;
    private Vector3 lastPos;
    private Vector3 speed;
    public PhysicMaterial bouncy;

    // Use this for initialization

    void Start ()
    {
        bouncy = Resources.Load("Materials/bouncy") as PhysicMaterial;
        soundEngine = GameObject.FindGameObjectWithTag("SoundEngineTag");
    }

	// Update is called once per frame
	void Update () {

        speed = lastPos - transform.position;
        lastPos = transform.position;
        if (!weapon)
        {
            lifeTime--;
            if(lifeTime<0)
            {
                if(fenceStamp)
                {
                    GameObject.FindGameObjectWithTag("canvas").GetComponent<EyeLogic>().itemInYard();
                }
                Destroy(gameObject);
            }
        }
	}
    void OnCollisionEnter(Collision collision)
    {
        if(weapon)
        {
            GameObject collided = collision.collider.gameObject;
            var colliderLogic = collided.GetComponent<WeaponLogic>();
            colliderLogic.value = 1f;
            //collided.GetComponent<Rigidbody>().velocity = Vector3.zero;
            if (rightItems.Contains(collision.collider.name.Substring(0, collision.collider.name.Length - 7)))
            {
                colliderLogic.value *= 3f;
                //collided.GetComponent<Rigidbody>().velocity = (collision.collider.transform.position - collision.contacts[0].point).normalized * 2f * collision.relativeVelocity.magnitude;
                collision.gameObject.GetComponent<Collider>().material = bouncy;
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
            //collided.GetComponent<Rigidbody>().velocity += (collision.collider.transform.position - collision.contacts[0].point).normalized * collision.relativeVelocity.magnitude;
            Debug.Log("VELOCITY " + speed.magnitude);
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
