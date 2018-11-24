using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

    public GameObject testCube;
    public float tmr;

	// Use this for initialization
	void Start () {
        tmr = 0;
	}

    // Update is called once per frame
    void Update () {
        tmr += Time.deltaTime;
        if (tmr >= 1.0)
        {
            tmr = 0;
            var cube = Instantiate(testCube);
            var body = cube.GetComponent<Rigidbody>();
            cube.transform.position = transform.position;
            body.velocity = new Vector3(0, 2, 5);
        }
	}
}
