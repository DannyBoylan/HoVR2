﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideScript : MonoBehaviour
{

    //script will hide the object it's attached to whn the game starts by turning
    //the renderer for it off.
    public bool debug = false;
    Renderer rend;
    // Start is called before the first frame update
    void Start()
    {
        rend = this.GetComponent<MeshRenderer>();
        if (!debug)
        {
            rend.enabled = false;
        }
    }

}