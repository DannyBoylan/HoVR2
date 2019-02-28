 using System.Collections;
 using System.Collections.Generic;
 using UnityEngine;

public class MissileAttack : MonoBehaviour
{
    public GameObject Bullet_Emitter;
    public GameObject Bullet;

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

            //Basic Clean Up, set the Bullets to self destruct after 10 Seconds, I am being VERY generous here, normally 3 seconds is plenty.
            Destroy(Temporary_Bullet_Handler, 5.0f);
        }
    }

    void start()
    {
    }

    void Update()
    {
    missileFire();
    }

}
