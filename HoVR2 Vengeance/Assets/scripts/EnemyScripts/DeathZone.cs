using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathZone : MonoBehaviour
{
    public GameObject deathFade;
    Renderer rend;
    public float range = 50;
    GameObject player;
    float counter = 5;
    float count = 10;
    // Start is called before the first frame update
    void Start()
    {
        rend = deathFade.GetComponent<Renderer>();
       
        counter = count;
        player = GameObject.Find("playerController");
    }

    // Update is called once per frame
    void Update()
    {
        //this checks that the player is in range and will then begin a count down, while also making a the screen fade black by increasing
        //the alpha of a plane attached in front of the camera
        float distance = Math.Distance(player.transform.position, this.transform.position);
        if (distance < range)
        {
            counter -= 1 * Time.deltaTime;
            float add = 10 - counter;
            rend.material.color = new Color(add/40, +0, +0, add/9);
        }

        //if the player isn't in range, then it resets the counter and make the plane invisible again
        if (counter < count && distance > range)
        {
            {
                counter = count;
                rend.material.color = new Color(+0, +0, +0, 0);
            }
        }

        //this is what happens when the timer runs out. 0 should be replaced with the scene wanted
        //such as a game over screen, or just back to the title. these can be set in the unity
        //build settings.
        if (counter <= 0)
        {
             UnityEngine.SceneManagement.SceneManager.LoadScene(0);
        }
        

    }


    //a gizmo for seeing the range in the editor, to know how big the range is.
    void OnDrawGizmosSelected()
    {
        // draws a wirefrane sphear to show the range of the spawner
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
