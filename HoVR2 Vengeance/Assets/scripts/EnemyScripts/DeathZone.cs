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

        float distance = Math.Distance(player.transform.position, this.transform.position);
        if (distance < range)
        {
            counter -= 1 * Time.deltaTime;
            float add = 10 - counter;
            rend.material.color = new Color(add/40, +0, +0, add/9);
        }
        if (counter < count && distance > range)
        {
            {
                counter = count;
                rend.material.color = new Color(+0, +0, +0, 0);
            }
        }

        if (counter <= 0)
        {
             UnityEngine.SceneManagement.SceneManager.LoadScene(0);
        }
        

    }

    void OnDrawGizmosSelected()
    {
        // draws a wirefrane sphear to show the range of the spawner
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
