using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewSpawner : MonoBehaviour {

    public GameObject testCube;
    public float tmr;
    public string folder;
    private Object[] images;
    private List<GameObject> objects;
    private float CD = 7.0f;
    // Use this for initialization
    void Start()
    {
        tmr = 0;
        images = Resources.LoadAll(folder, typeof(Object));
        objects = new List<GameObject>();
        for (int i = 0; i < images.Length; i++)
        {
            //objects.Add(Instantiate(images[i], transform) as GameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        tmr += Time.deltaTime;
        if (tmr >= CD + Random.Range(0,1))
        {
            tmr = 0;
            var obj = Instantiate(images[Random.Range(0,images.Length)], transform) as GameObject;
            var body = obj.GetComponent<Rigidbody>();
            obj.transform.position = transform.position;
            body.velocity = new Vector3(0, 1, 2);
            float factor = 3.0f;
            body.angularVelocity = new Vector3(Random.Range(-factor,factor), Random.Range(-factor, factor), Random.Range(-factor, factor));
            obj.GetComponent<WeaponLogic>().setUp(false);
            CD = CD*0.98f;
        }
    }

    

}
