using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sound : MonoBehaviour {
    AudioSource audioData;

    public bool start = false;
    public bool speedup = false;

    // Use this for initialization
    void Start () {
        audioData = GetComponent<AudioSource>();
        audioData.pitch = 0;
    }
	
	// Update is called once per frame
	void Update ()
    {
		if (start == true)
        {
            if (audioData.pitch < 0.5)
            {
                audioData.pitch += 0.1f * Time.deltaTime;
            }
         
            if (speedup == true)
            {
                if (audioData.pitch < 1.0)
                {
                    audioData.pitch += 0.05f * Time.deltaTime;
                }
            }
            else if(audioData.pitch > 0.5)
            {
                audioData.pitch -= 0.05f * Time.deltaTime;
            }
        }
        else if (audioData.pitch > 0.0)
        {
            audioData.pitch -= 0.1f * Time.deltaTime;
        }


	}
}
