using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawLazer : MonoBehaviour {

    public Transform gun;
    public GameObject projectile;



	// Use this for initialization
	void FixedUpdate ()
    {
        // Gets the commonet of line renderer on the object
    
        LineRenderer LR = gameObject.GetComponent<LineRenderer> ();

        // this sets the position of the first point which is the position of the gun
        if (gun != null)
        {
            LR.SetPosition(0, gun.transform.position);
        }
        // this sets the second position of the line render as the position of the projectile
        LR.SetPosition(1, projectile.transform.position);
        // the line will be render between both points.
    }

    // Update is called once per frame
    private void Start()
    {
        projectile = this.gameObject;
    }
    void Update ()
    {

        gun = GetComponentInParent<TransformStorage>().gun.transform;
    }
}
