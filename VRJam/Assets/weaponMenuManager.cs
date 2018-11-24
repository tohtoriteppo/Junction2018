using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weaponMenuManager : MonoBehaviour
{
    public string folder;

    private string weaponSelected;
    private float rotateSpeed = 1f;
    private float radius = 210f;
    private Vector2 rotatePoint;
    private Object[] images;
    private List<GameObject> objects;
    private int index;
    private int leftBound = -2;
    private int rightBound = 3;
    private float scaleFactor = 0.2f;
    private float middleAngle = 0;
    private List<float> leftAngles;
    private List<float> rightAngles;
    // Use this for initialization
    void Start()
    {
        rotatePoint = Vector2.zero;
        images = Resources.LoadAll(folder, typeof(Object));
        setUp();
        setAngles();
        setPositions();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("OW "+transform.localPosition);
        /*
        if (Input.GetButtonDown("left"))
        {
            rotate(false);
        }
        if (Input.GetButtonDown("right"))
        {
            rotate(true);
        }
        */

    }
    public void rotate(bool right)
    {
        int dir = right == true ? 1 : -1;
        int indexChange = -dir;
        /*
        for (int i = leftBound; i < rightBound; i++)
        {
            int j = Mathf.Max(Mathf.Min(index + i,objects.Count-1),0);
            objects[j].GetComponent<ImageLogic>().angle += dir * rotateSpeed * Time.deltaTime;
            var offset = new Vector2(Mathf.Sin(objects[j].GetComponent<ImageLogic>().angle), Mathf.Cos(objects[j].GetComponent<ImageLogic>().angle)) * radius;
            if(Mathf.Rad2Deg*objects[j].GetComponent<ImageLogic>().angle > 90)
            {
                indexChange = -1;
            }
            else if(Mathf.Rad2Deg * objects[j].GetComponent<ImageLogic>().angle < -90)
            {
                indexChange = 1;
            }
            objects[j].transform.position = rotatePoint + offset;
        }*/
        if(indexChange == 1)
        {
            //setActives();
            //int ind = Mathf.Max(index + leftBound, 0);
            int inde = Mathf.Abs((index + leftBound) % (objects.Count - 1))-1;
            int ind = index + leftBound < 0 ? objects.Count - 1-inde : index + leftBound;
            objects[ind].SetActive(false);
            inde = Mathf.Abs((index + rightBound) % (objects.Count - 1))-1;
            ind = index + rightBound > objects.Count-1 ? inde : index + rightBound;
            objects[ind].SetActive(true);
            index = index +1 > objects.Count - 1 ?  0: index + 1;
        }
        else if(indexChange == -1)
        {
            //setActives();
            int inde = Mathf.Abs((index + rightBound - 1) % (objects.Count - 1))-1;
            int ind = index + rightBound - 1 > objects.Count - 1 ? inde : index + rightBound - 1;
            objects[ind].SetActive(false);
            inde = Mathf.Abs((index + leftBound - 1) % (objects.Count - 1))-1;
            ind = index + leftBound - 1 < 0 ? objects.Count-1-inde : index + leftBound - 1;
            objects[ind].SetActive(true);
            index = index - 1 < 0 ? objects.Count - 1 : index - 1;

        }
        setPositions();

    }
    void setActives()
    {

    }
    void setAngles()
    {
        leftAngles = new List<float>();
        rightAngles = new List<float>();
        for (int i = 0; i < -leftBound; i++)
        {
            leftAngles.Add(-1.4f / (-leftBound)*(i+1));
            
        }
        for (int i = 0; i < rightBound-1; i++)
        {
            rightAngles.Add(1.4f / (rightBound-1) * (i + 1));
        }
    }
    void setPositions()
    {
        var offset = new Vector2(Mathf.Sin(middleAngle), Mathf.Cos(middleAngle)) * radius;
        Debug.Log(rotatePoint + offset);
        objects[index].transform.localPosition = rotatePoint + offset;
        
        //objects[index].transform.localPosition = Camera.main.scree(rotatePoint + offset);
        objects[index].transform.localScale = new Vector3(1, 1);
        weaponSelected = objects[index].name.Substring(0, objects[index].name.Length - 7);
   
        //objects[index].transform.position = new Vector3
        for (int i = 0; i < leftAngles.Count; i++)
        {
            offset = new Vector2(Mathf.Sin(leftAngles[i]), Mathf.Cos(leftAngles[i])) * radius;
            int thing = index - i - 1;
            int inde = Mathf.Abs((thing) % (objects.Count - 1)) - 1;
            int ind = thing < 0 ? objects.Count - 1 - inde : thing;
            objects[ind].transform.localPosition = rotatePoint + offset;
            objects[ind].transform.localScale = new Vector3(1 - (i + 1) * scaleFactor, 1 - (i + 1) * scaleFactor);
        }
        for (int i = 0; i < rightAngles.Count; i++)
        {
            offset = new Vector2(Mathf.Sin(rightAngles[i]), Mathf.Cos(rightAngles[i])) * radius;
            int thing = index + i + 1;
            int inde = Mathf.Abs((thing) % (objects.Count - 1)) - 1;
            int ind = thing > objects.Count -1 ? inde : thing;
            objects[ind].transform.localPosition = rotatePoint + offset;
            objects[ind].transform.localScale = new Vector3(1 - (i + 1) * scaleFactor, 1 - (i + 1) * scaleFactor);
        }
    }
    void setToAngle()
    {
        for (int i = leftBound; i < rightBound; i++)
        {
            int j = Mathf.Max(Mathf.Min(index + i, objects.Count - 1), 0);
            if (index < 0)
            {
                j = objects.Count - i;
            }
            objects[j].GetComponent<ImageLogic>().angle = i * 1;
            var offset = new Vector2(Mathf.Sin(objects[j].GetComponent<ImageLogic>().angle), Mathf.Cos(objects[j].GetComponent<ImageLogic>().angle)) * radius;
            objects[j].transform.position = rotatePoint + offset;
        }
        for(int i = 0; i < index+leftBound; i++)
        {
            objects[i].SetActive(false);
            objects[i].GetComponent<ImageLogic>().angle = -1.5f;
        }
        for (int i = index + rightBound; i < objects.Count; i++)
        {
            objects[i].SetActive(false);
            objects[i].GetComponent<ImageLogic>().angle = 1.5f;
        }
    }
    void setUp()
    {
        objects = new List<GameObject>();
        for (int i = 0; i < images.Length; i++)
        {
            objects.Add(Instantiate(images[i], transform) as GameObject);
        }
        index = objects.Count / 2;

        setToAngle();

    }
    public string getSelectedWeapon()
    {
        return weaponSelected;
    }
}
