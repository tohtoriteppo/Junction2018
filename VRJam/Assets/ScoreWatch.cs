using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreWatch : MonoBehaviour {

    public GameObject fence;
    public float score = 0f;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
        foreach (Transform child in transform)
        {
            var logic = child.GetComponent<WeaponLogic>();

            // if object has flown over the fence
            if (child.position.z > fence.transform.position.z)
            {
                logic.fenceStamp = true;
            }
            // if player managed to hit it back
            else if (logic.fenceStamp)
            {
                Debug.Log(logic.value);
                logic.fenceStamp = false;
                score += logic.value;
                GameObject.FindGameObjectWithTag("score").GetComponent<Text>().text = score.ToString();
            }
        }
	}
}
