using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewSpawner : MonoBehaviour {

    public GameObject testCube;
    public float tmr;
    public string folder;
    private Object[] images;
    private List<GameObject> objects;

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
        if (tmr >= 1.0)
        {
            tmr = 0;
            //var cube = Instantiate(testCube);
            GameObject obj = Instantiate(images[Random.Range(0,images.Length)]) as GameObject;
            var body = obj.GetComponent<Rigidbody>();
            obj.transform.position = transform.position;
            body.velocity = new Vector3(0, 2, 5);
            obj.GetComponent<WeaponLogic>().setUp(false);
        }
    }

    

    }
