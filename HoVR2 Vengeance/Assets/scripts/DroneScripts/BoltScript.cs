using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoltScript : MonoBehaviour {
    Renderer rend;
    float scrollSpeed = -2.0f;
    // Use this for initialization
    void Start () {
        rend = GetComponent<Renderer>();
        rend.material.mainTextureOffset = new Vector2(0, 0);
    }
	
	// Update is called once per frame
	void Update () {
        float offset = Time.time * scrollSpeed;
        rend.material.mainTextureOffset = new Vector2(offset, 0);
    }
}
