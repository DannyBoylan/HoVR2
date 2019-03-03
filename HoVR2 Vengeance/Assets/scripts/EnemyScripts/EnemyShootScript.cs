using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShootScript : MonoBehaviour
{
    [Range(1,20)]
    public float damage = 10f;
    float range;
    // this is so can attach shooting to camera for fps, change to turret hole later
    public GameObject Emitter,ThisObject;
    
    //this is for lasor beams and shooting
    public float ReloadTimer;  // want reload timer to be 2.5x cool down timer

    public GameObject impact;
    // this is the new dely system
    private bool allowfire = true;
    // Start is called before the first frame update
    public DroneLife target;

    void Start()
    {
        ThisObject = this.gameObject;
        
    }

    IEnumerator Reload()
    {
        allowfire = false;



        // this waits for x seconds before the code runs again, uses corourines.
        yield return new WaitForSeconds(ReloadTimer);

        allowfire = true;

    }
    // Update is called once per frame
    void Update()
    {
        //Debug.DrawRay(Emitter.transform.position, Emitter.transform.forward * 100f, Color.red);
        range = ThisObject.GetComponent<EnemyTargeting>().range;
        // putting shoot on fire button just for testing
        if (allowfire == true)
        {

            Shoot();
        }
    }

    void Shoot()
    {

        Debug.Log("start shoot script");
        //creates a ray called hit
        RaycastHit hit;
        // this actually shoots out the ray. first variable is where the ray shoots from.  2nd is the direction (forward). 3nd gathers info and puts it in hit, 4th is the range.
        Debug.Log(range);
        if (Physics.Raycast(Emitter.transform.position, Emitter.transform.forward, out hit))
        {
            Debug.Log("entered range");
            Debug.Log(hit.transform.name);
            DroneLife target = hit.transform.GetComponent<DroneLife>();
            //Debug.Log(Player.GetComponent<ProximityDetection>().rangeInKM / rangeDivider);



            GameObject spark = Instantiate(impact, hit.point, transform.rotation);
            spark.GetComponent<TransformStorage>().gun = ThisObject;




            if (target != null) // need to put something here that is less than range 
            {
                Debug.Log("damageing target");
                //This sends the damage variable of the drone
                target.takeDamage(damage);

            }

            StartCoroutine(Reload());




        }

    }
}
