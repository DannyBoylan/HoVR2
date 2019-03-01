using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatistics : MonoBehaviour
{
    //basic player statistics and death.
    public GameObject explosion;
    int maxHealth;
    [Range(1,300)]
    public int health;
    // Start is called before the first frame update
    void Start()
    {
        maxHealth = health;
    }

    // Update is called once per frame
    void Update()
    {
        if (health > maxHealth) health = maxHealth;
        else if (health <= 0) Die();
    }
    void Die()
    {
        Instantiate(explosion, transform.position, transform.rotation);
        StartCoroutine(GameOver());
    }
    IEnumerator GameOver()
    {
        yield return new WaitForSeconds(2);
        //0 should be changed for a game over screen or the title screen, this can be set in the unity build settings
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }
}
