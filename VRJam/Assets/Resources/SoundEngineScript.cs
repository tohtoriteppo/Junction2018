using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEngineScript : MonoBehaviour {

    private AudioSource source;
    public AudioClip goodHitSound;
    public AudioClip badHitSound;
    private void Awake()
    {
        source = GetComponent<AudioSource>();
    }
    // Use this for initialization
    void Start () {
//        goodHitSound = (AudioClip)Resources.Load("goodhit.mp3");
//        badHitSound = (AudioClip)Resources.Load("badhit.mp3");
    }

    public void playGood()
    {
        source.PlayOneShot(goodHitSound, 1.0f);
    }

    public void playBad()
    {
        source.PlayOneShot(badHitSound, 1.0f);
    }

    // Update is called once per frame
    void Update () {
		
	}
}
