using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneLife : MonoBehaviour
{
    public GameObject explosion, slot;
    [Range(50,100)]
    public int health = 100;
    int maxHealth;
    public bool debugDeath = false;
    // Start is called before the first frame update
    void Start()
    {
        maxHealth = health;
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            Instantiate(explosion, transform.position, transform.rotation);
            if (slot != null) {
                slot.GetComponent<Slot>().DroneDestroyed();
                    }
            Destroy(this.gameObject);
        }
        if (debugDeath) health -= 1;
    }
}
