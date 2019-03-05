 using System.Collections;
 using System.Collections.Generic;
 using UnityEngine;

public class MissileAttack : MonoBehaviour
{
    public GameObject Bullet_Emitter, Bullet;
    private bool allowfire = true;
    public float ReloadTimer = 2;
    void missileFire()
    {
        if (Input.GetKeyDown("space"))
        {
            //The Bullet instantiation happens here.
            GameObject Temporary_Bullet_Handler;
            Temporary_Bullet_Handler = Instantiate(Bullet, Bullet_Emitter.transform.position, Bullet_Emitter.transform.rotation) as GameObject;

            //Sometimes bullets may appear rotated incorrectly due to the way its pivot was set from the original modeling package.
            //This is EASILY corrected here, you might have to rotate it from a different axis and or angle based on your particular mesh.
            Temporary_Bullet_Handler.transform.Rotate(Vector3.left * 90);

            //Retrieve the Rigidbody component from the instantiated Bullet and control it.
            Rigidbody Temporary_RigidBody;
            Temporary_RigidBody = Temporary_Bullet_Handler.GetComponent<Rigidbody>();

            //Basic Clean Up, set the Bullets to self destruct after 10 Seconds, I am being VERY generous here, normally 3 seconds is plenty.
            StartCoroutine(Reload());
        }
    }

    IEnumerator Reload()
    {
        allowfire = false;



        // this waits for x seconds before the code runs again, uses corourines.
        yield return new WaitForSeconds(ReloadTimer);

        allowfire = true;

    }

    void Update()
    {
        if (allowfire)
        {
            missileFire();
        }
    }

}
