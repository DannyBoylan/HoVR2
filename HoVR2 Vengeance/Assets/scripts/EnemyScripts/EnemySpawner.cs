using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

    public Transform PlayerObject;
    public GameObject objectToSpawn;
    [Range(1.0f,10.0f)]
    public int numberToSpawn;
    [Range(1.0f, 10.0f)]
    public int spawnLimit;
    [Range(1.0f, 1000.0f)]
    public float proximity;
    [Range(0.0f, 10.0f)]
    public int Delay = 0;
    [Range(0.0f, 10.0f)]
    public int DelayCounter = 0;

    private float checkRate;
    private float nextCheck;
    private Transform myTransform;
    private Vector3 spawnPosition;
    private int cloneCount;
    public bool destroyAfterSpawn = false;

    void SetInitialReferences()
    {
        myTransform = transform;
        PlayerObject = GameObject.Find("playerController").transform;
        checkRate = Random.Range(0.8f, 1.2f);
    }

    void CheckDistance()
    {
        if (Time.time > nextCheck)
        {
            nextCheck = Time.time + checkRate;
            if (Vector3.Distance(myTransform.position, PlayerObject.position) < proximity)
            {
                if (DelayCounter > 0)
                {
                    DelayCounter -= 1;
                    // Debug.Log(startDelay);
                }
                else
                {
                    SpawnCountCheck();
                    DelayCounter = Delay;
                }
            }
        }
    }

    void SpawnCountCheck()
    {
        if (cloneCount < spawnLimit)
        {
            SpawnObjects();
        }
        else if (cloneCount >= spawnLimit)
        {
            DelayCounter = Delay;
        }
    }

    void SpawnObjects()
    {
        for (int i = 0; i < numberToSpawn; i++)
        {
            spawnPosition = myTransform.position + Random.insideUnitSphere * 5;
            GameObject EnemySpawn = (GameObject)Instantiate(objectToSpawn, spawnPosition, myTransform.rotation);
            cloneCount++;
            EnemySpawn.GetComponent<Enemy>().enemySpawner = this.gameObject;
        }
        if (destroyAfterSpawn)
        {
            Destroy(this.gameObject);
        }
    }

    // Use this for initialization
    void Start()
    {
        SetInitialReferences();
    }

    // Update is called once per frame
    void Update()
    {
        CheckDistance();
    }

    public void lowerCount()
    {
        cloneCount -= 1;
    }

    void OnDrawGizmosSelected()
    {
        // draws a wirefrane sphear to show the range of the spawner
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, proximity);
    }
}
