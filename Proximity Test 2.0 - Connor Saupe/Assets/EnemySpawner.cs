using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

    public GameObject objectToSpawn;
    public int numberToSpawn;
    public int spawnLimit;
    public float proximity;
    public int startDelay;

    private float checkRate;
    private float nextCheck;
    private Transform myTransform;
    private Transform playerTransform;
    private Vector3 spawnPosition;
    private int cloneCount;

    void SetInitialReferences()
    {
        myTransform = transform;
        playerTransform = GameObject.Find("Player").transform;
        checkRate = Random.Range(0.8f, 1.2f);
    }

    void CheckDistance()
    {
        if (Time.time > nextCheck)
        {
            nextCheck = Time.time + checkRate;
            if (Vector3.Distance(myTransform.position, playerTransform.position) < proximity)
            {
                if (startDelay > 0)
                {
                    startDelay -= 1;
                    // Debug.Log(startDelay);
                }
                else
                {
                    SpawnCountCheck();
                    startDelay = 5;
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
            startDelay = 5;
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
}
