using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cubeMove : MonoBehaviour {

    public float value = 0.1f;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = new Vector3(transform.position.x + Random.Range(-value, value), transform.position.y + Random.Range(-value, value), transform.position.z + Random.Range(-value, value));
	}
}
