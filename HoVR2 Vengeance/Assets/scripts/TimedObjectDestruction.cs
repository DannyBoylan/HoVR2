using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimedObjectDestruction : MonoBehaviour {
    public float time;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        Destroy(this.gameObject, time);//Destroy the game object after x seconds.
    }
}
