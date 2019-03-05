using UnityEngine;

public class SplineWalkerEnemy : MonoBehaviour {

	public BezierSpline spline;
    public GameObject Player;

	public bool lookForward; // Face forward
    public bool findPlayer; // Face player
    public bool matchPlayerSpeed;
    public bool activeDelay;
    [Range(0.0f,100.0f)]
    public float delay = 0;
	public SplineWalkerMode mode;

	private float progress;
	private bool goingForward = true;

    [Range(0.0f, 350.0f)] // Speed limits minimum and maximum - I tried 0-10 but it was too fast
    public float speed= 0.1f;
    public bool active = false;
    Vector3 offset;
    [Range(-100.0f,100.0f)]
    public float xOffset = 0;
    [Range(-100.0f, 100.0f)]
    public float yOffset = 0;
    [Range(-100.0f, 100.0f)]
    public float zOffset = 0;
    float speedModifier = 2500;


    private void Start()
    {
        Player = GameObject.Find("playerController");
        offset = new Vector3(xOffset, yOffset, zOffset);
        speed /= speedModifier;
    }
    
    private void Update () {
        if (active == true && spline != null)
        {
            if (goingForward)
            {
                progress += Time.deltaTime * speed;
                if (progress > 1f)
                {
                    if (mode == SplineWalkerMode.Once)
                    {
                        progress = 1f;
                    }
                    else if (mode == SplineWalkerMode.Loop)
                    {
                        progress -= 1f;
                    }
                    else if (mode == SplineWalkerMode.PingPong)
                    {
                        progress = 2f - progress;
                        goingForward = false;
                    }
                    else { Destroy(this.gameObject); }
                }
            }
            else
            {
                progress -= Time.deltaTime * speed;
                if (progress < 0f)
                {
                    progress = -progress;
                    goingForward = true;
                }
            }

            Vector3 position = spline.GetPoint(progress); // Find out where the player is along the spline
            position += offset;
            transform.localPosition = position; // Set position to position on spline
            if (lookForward)
            {
                transform.LookAt(position + spline.GetDirection(progress));
            }
            else if (findPlayer) // Stop program from trying to look forward and at player at the same time
            {
                // Use vectors to get player direction and set object to face player
                transform.LookAt(Player.transform.position);
            }
            if (matchPlayerSpeed)
            {
                speed = Player.GetComponent<PlayerMovement>().speed / speedModifier;
            }

            
        }
        if (activeDelay)
        {
            if (delay > 0)
            {
                delay--;
            }
            else
            {
                active = true;
                activeDelay = false;
            }
        }
    }
}