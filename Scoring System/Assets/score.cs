using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class score : MonoBehaviour
{
    public float Score = 0;
    public float previousscore = 0;
    public Text sscore;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Score = previousscore + 1;
            previousscore = Score;
            sscore.text = Score.ToString();
        }

    }
}










