using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponLogic : MonoBehaviour {

    public List<string> rightItems;
    private int lifeTime = 600;
    private bool weapon = true;
	// Use this for initialization
	void Start () {
		
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
            if (rightItems.Contains(collision.collider.name.Substring(0, collision.collider.name.Length - 7)))
            {
                collision.collider.gameObject.GetComponent<Rigidbody>().velocity = (collision.collider.transform.position - transform.position) * 10f;
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
