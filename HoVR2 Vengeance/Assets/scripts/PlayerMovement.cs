using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;


public class PlayerMovement : MonoBehaviour
{
	GameObject tracker, forwardVector,d1,d2,Direction;
    bool engineOn = true;
    
    float throttle;
    float acceleration;
    public float speed;   
    public float maxSpeed;

    [Range(500.0f, 1500.0f)]
    public float accelerationMultiplier = 1000.0f;
    [Range(100.0f, 500.0f)]
    public float maxSpeedMultiplier = 300;
	
    float slowDown;

    float initialYAngle;
    float leanDistance;
    float leanAngle;

    [Range(0.0f, 5.0f)]
    public float leanThreshhold = 0.5f;
    [Range(50.0f, 500.0f)]
    public float leanAdjustment = 1.0f;


    float initialXAngle;
    float pitchDistance;
    float pitchAngle;

    public Vector3 xzoffset, yoffset, offset;

    [Range(0.0f, 5.0f)]
    public float pitchThreshhold = 0.5f;
    [Range(1.0f, 5.0f)]
    public float pitchAdjustment = 1.0f;

    float friction = 1.0f;

    private Valve.VR.EVRButtonId startSwitch    = Valve.VR.EVRButtonId.k_EButton_SteamVR_Touchpad;
    private Valve.VR.EVRButtonId bikeLevers     = Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger;
    private Valve.VR.EVRButtonId leftFoot       = Valve.VR.EVRButtonId.k_EButton_Grip;
    private Valve.VR.EVRButtonId rightFoot      = Valve.VR.EVRButtonId.k_EButton_ApplicationMenu;

    public GameObject up, down, left, right;

    public bool debugTestSpeed;
    AudioSource audioData;
    Vector3 directionNormalzized;

    SteamVR_TrackedObject trackedObject;

    Vector3 direction2;
    float direction2Length;
    Vector3 direction2Normalzized;
    bool directionSet = false;
    bool directionFix = false;

    public float distance;
    [Range(1.0f, 50.0f)]
    public float radius = 5;

    public bool W, A, S, D = false;

    void Start()
    {
        //This is just going to set up the Game objects, looking for the names that are set in the prefab.
        tracker = transform.Find("tracker").gameObject;
        forwardVector = transform.Find("tracker/forwardVector").gameObject;
        audioData = GetComponent<AudioSource>();
        audioData.pitch = 0;
        throttle = Input.GetAxis("Throttle");
        Direction = transform.Find("Direction").gameObject;
        d1 = transform.Find("Direction/d1").gameObject;
        d2 = transform.Find("Direction/d2").gameObject;
        transform.localPosition += new Vector3(0, 0.01f, 0);
        up = transform.Find("tracker/up").gameObject;
        down = transform.Find("tracker/down").gameObject;
        left = transform.Find("tracker/left").gameObject;
        right = transform.Find("tracker/right").gameObject;
    }

    private void Update()
    {


        //THis is an initial Start function, that doesn't work in start, since i assume, it hasnt picked up the tracker yet.
        if (trackedObject == null)
        {
            trackedObject = tracker.GetComponent<SteamVR_TrackedObject>();

            //Gets the directionn that we want the bike to face, represented by an arrow in the scene
            direction2 = d2.transform.position - d1.transform.position;
            direction2Length = Mathf.Sqrt((direction2.x * direction2.x) + (direction2.y * direction2.y) + (direction2.z * direction2.z));
            direction2Normalzized = direction2 / direction2Length;
        }
        var vr_device = SteamVR_Controller.Input((int)trackedObject.index);
        //This the same as before, but only runs one when the trackers found. this is for the initial values of the tracker;
        if ((trackedObject != null) && (initialYAngle == 0))
        {
            initialYAngle = trackedObject.transform.localEulerAngles.y;
            initialXAngle = trackedObject.transform.localEulerAngles.x;
            Vector3 direction = forwardVector.transform.position - tracker.transform.position;
            float directionLength = Mathf.Sqrt((direction.x * direction.x) + (direction.y * direction.y) + (direction.z * direction.z));
            directionNormalzized = direction / directionLength;


        }
        //This moves the bike around so that it always faces in the directionplaced, if the bike is facing another direction, 
        //it will rotate round till it is the right direction

        while (directionSet == false) {
            Vector3 direction1 = forwardVector.transform.position - tracker.transform.position;
            float direction1Length = Mathf.Sqrt((direction1.x * direction1.x) + (direction1.y * direction1.y) + (direction1.z * direction1.z));
            Vector3 direction1Normalzized = direction1 / direction1Length;

            float dotProduct = ((direction1Normalzized.x * direction2Normalzized.x) + (direction1Normalzized.y * direction2Normalzized.y) + (direction1Normalzized.z * direction2Normalzized.z));
            if ((dotProduct < 0.9999f) && (directionSet == false))
            {
                direction1 = forwardVector.transform.position - tracker.transform.position;
                direction1Length = Mathf.Sqrt((direction1.x * direction1.x) + (direction1.y * direction1.y) + (direction1.z * direction1.z));
                direction1Normalzized = direction1 / direction1Length;
                transform.Rotate(Vector3.up, (10.01f));
                directionFix = true;
            }
            if ((dotProduct > 0.9999f) && (directionFix == true))
            {
                Destroy(Direction);
                directionSet = true;

            }
        }


        //This turns the engine on or off,
        //if (vr_device.GetPress(startSwitch)) {
        if (Input.GetKeyDown(KeyCode.M)) {
            switch (engineOn)
            {
                case (true):
                    { engineOn = false; break; }
                case (false):
                    { engineOn = true; break; }
            }
        }

        //If the engine is on, this will set the speed of the bike, as wel as adjusting the pitch of the sound it makes.
        if (engineOn == true)
        {
            if (audioData.pitch < 0.5)
            {
                audioData.pitch += 1.0f * Time.deltaTime;
            }
            /*This is the code that decides acceleration and max speed, this allows 
             * for a variable speed depending on how far the throttle is open.*/
            if (Input.GetAxis("Throttle") > throttle)
            {
                acceleration = (Input.GetAxis("Throttle") * accelerationMultiplier) * Time.deltaTime;
                maxSpeed = (Input.GetAxis("Throttle") * maxSpeedMultiplier);
            }
        }
        else
        {
            if (audioData.pitch > 0.0)
            {
                audioData.pitch -= 1.0f * Time.deltaTime;
                acceleration = 0.0f;
                maxSpeed = 0.0f;

            }
        }

        distance = this.GetComponent<SplineWalkerPlayer>().distance;
        if (distance >= radius)
        {
            offset = GetComponent<SplineWalkerPlayer>().moveToCentre(offset);
            xzoffset = offset;
            yoffset = offset;
            yoffset.x = 0;
            yoffset.z = 0;
            xzoffset.y = 0;
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            W = true;
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            S = true;
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            A = true;
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            D = true;
        }
        if (Input.GetKeyUp(KeyCode.W))
        {
            W = false;
        }
        if (Input.GetKeyUp(KeyCode.S))
        {
            S = false;
        }
        if (Input.GetKeyUp(KeyCode.A))
        {
            A = false;
        }
        if (Input.GetKeyUp(KeyCode.D))
        {
            D = false;
        }


        if (W == true)
        {
            Vector3 a = up.transform.position - transform.position;
            Math.NormVector(a);
            yoffset += a;
            yoffset.x = 0;
            yoffset.z = 0;
            Debug.Log("test");
        }
        if (S == true)
        {
            Vector3 a = down.transform.position - transform.position;
            Math.NormVector(a);
            yoffset += a;
            yoffset.x = 0;
            yoffset.z = 0;
        }
        if (A == true)
        {
            Vector3 a = left.transform.position - transform.position;
            Math.NormVector(a);
            xzoffset += a;
            xzoffset.y = 0;

        }
        if (D == true)
        {
            Vector3 a = right.transform.position - transform.position;
            xzoffset += a;
            xzoffset.y = 0;
        }
        offset = xzoffset + yoffset;
        /*These are the buttons for the footrests and the Levers
		if (vr_device.GetPress(leftFoot)) {}
		if (vr_device.GetPress(rightFoot)){}
        if (vr_device.GetPress(bikeLevers)){}
		*/



        //This calculates the lean angle of the bike bades on the angle of the trackers Y rotation.
        //Moving the bike too far down can change the Y rotation value to below 100, this accounts for
        //this and makes an adjustment

        /*
        if (trackedObject.transform.localEulerAngles.y < 100)
        {
            leanAngle = 0;
        }
        else if (trackedObject.transform.localEulerAngles.y > (initialYAngle + leanThreshhold))
        {
            leanAngle += (trackedObject.transform.localEulerAngles.y - (initialYAngle + leanThreshhold));
            Vector3 a = right.transform.position - transform.position;
            Math.NormVector(a);
            xzoffset += (a / 10)*leanAngle;
            xzoffset.y = 0;
        }
        else if (trackedObject.transform.localEulerAngles.y < (initialYAngle - leanThreshhold))
        {
            leanAngle += (trackedObject.transform.localEulerAngles.y - (initialYAngle - leanThreshhold));
            Vector3 a = left.transform.position - transform.position;
            Math.NormVector(a);
            xzoffset += (a / 10) * leanAngle;
            xzoffset.y = 0;
        }
        else leanAngle = 0;

        //This works in the same way as the lean, though it starts at an X rotation of 5, and quickly when leaning back
        //loops to 360, since leaning forward never goes above 100, if it does, than it is safe to assume they're leaning
        //back and 360 is taken away.
        if (trackedObject.transform.localEulerAngles.x > 100)
        {
            pitchAngle += ((trackedObject.transform.localEulerAngles.x - (initialXAngle - pitchThreshhold)) - 360.0f);
            Vector3 a = up.transform.position - transform.position;
            Math.NormVector(a);
            yoffset += (a / 10)*pitchAngle;
            yoffset.x = 0;
            yoffset.z = 0;
        }
        else if ((trackedObject.transform.localEulerAngles.x > (initialXAngle + pitchThreshhold) && (trackedObject.transform.localEulerAngles.x < 300)))
        {
            pitchAngle += (trackedObject.transform.localEulerAngles.x - (initialXAngle - pitchThreshhold));
            Vector3 a = down.transform.position - transform.position;
            Math.NormVector(a);
            yoffset += (a / 10) * pitchAngle;
            yoffset.x = 0;
            yoffset.z = 0;
        }
        else if (trackedObject.transform.localEulerAngles.x < (initialXAngle - pitchThreshhold))
        {
            pitchAngle += (trackedObject.transform.localEulerAngles.x - (initialXAngle - pitchThreshhold));
            Vector3 a = up.transform.position - transform.position;
            Math.NormVector(a);
            yoffset += (a / 10) * pitchAngle;
            yoffset.x = 0;
            yoffset.z = 0;
        }
        else pitchAngle = 0;
        */

        //offset = new Vector3();

        //This adds acceleration to speed and also the slowDown, and makes sure the bike doesn't go backwards after slowdown.
        if (speed < maxSpeed) speed += (acceleration * 2.0f) * Time.fixedDeltaTime;
        if (speed > 0) speed += slowDown * Time.fixedDeltaTime;
        if (speed < 0) speed = 0;
        slowDown = 0;

        //This simlply makes the engine sound like it's reving faster as the speed goes up.
        if ((audioData.pitch >= 0.5f) && (audioData.pitch <= 1.0f) && (engineOn == true))
        {
            audioData.pitch = (0.5f + speed * Time.deltaTime);
        }
        if (audioData.pitch > 1.0f)
        {
            audioData.pitch = 1.0f;
        }



        if (debugTestSpeed == true)
        {
            if (speed < 50)
                speed+=0.5f;
        }

        //THis is the code that moved the player with the values gathered.
        directionNormalzized.y = 0;
        if (maxSpeed < 0) { maxSpeed = 0; }
        if (acceleration < 0) { acceleration = 0; }
        //transform.position += (directionNormalzized * speed) * Time.fixedDeltaTime;
        /*if (speed > 0) 
        {

            transform.position += new Vector3(((leanAngle * leanAdjustment) * Time.fixedDeltaTime), 0, 0);
            transform.position += new Vector3(0, ((pitchAngle * pitchAdjustment) * Time.fixedDeltaTime), 0);         
        }
        leanAngle = 0;
        pitchAngle = 0;
        */

    }
    float GetDistance(Vector3 A,Vector3 B)
    {
        Vector3 D = (A - B);
        D.x = D.x * D.x;
        D.y = D.y * D.y;
        D.z = D.z * D.z;
        return Mathf.Sqrt(D.x + D.y + D.z);
    }
}


    