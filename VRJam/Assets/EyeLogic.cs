using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeLogic : MonoBehaviour {

    public GameObject topLash;
    public GameObject botLash;
    public GameObject endGameScreen;
    private Vector2 topStartMin;
    private Vector2 botStartMax;
    private Vector2 botStartMin;
    private int counter;
    private float factor = 4;
    private int items;
    private int loseAmount = 5;
    public bool started = true;
    // Use this for initialization
    void Start () {
        topStartMin = topLash.GetComponent<RectTransform>().offsetMin;
        botStartMax = topLash.GetComponent<RectTransform>().offsetMax;
        botStartMin = topLash.GetComponent<RectTransform>().offsetMin;
        
    }
	
	// Update is called once per frame
	void Update () {
		if(started)
        {
            counter++;
            float thing = botStartMin.y+counter*factor;
            if (thing < 0)
            {
                thing = 0;
            }
            topLash.GetComponent<RectTransform>().offsetMin = new Vector2(topStartMin.x, thing); // left bot
            botLash.GetComponent<RectTransform>().offsetMax = new Vector2(botStartMax.x, -thing); // right top
        }
	}
    public void start()
    {
        started = true;
    }
    public void itemInYard()
    {
        items++;
        if(items> loseAmount-1)
        {
            end();
        }
    }
    public void end()
    {
        endGameScreen.SetActive(true);
    }
}
