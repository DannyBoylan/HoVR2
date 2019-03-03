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
        //basic sets the max health to the health set in the unity editor.
        maxHealth = health;
    }
    public void takeDamage(float damage)
    {
        health -= (int)damage;
    }
    // Update is called once per frame
    void Update()
    {
        if (health > maxHealth) health = maxHealth;
        //death script, if it runs out of health, then make it explode, remove it from the slot if it has one, and then destroy the drone object
        if (health <= 0)
        {
            Instantiate(explosion, transform.position, transform.rotation);
            if (slot != null) {
                slot.GetComponent<Slot>().DroneDestroyed();
                    }
            Destroy(this.gameObject);
        }

        //allows health to be depleted by using a bool for debug
        if (debugDeath) health -= 1;
    }
}
