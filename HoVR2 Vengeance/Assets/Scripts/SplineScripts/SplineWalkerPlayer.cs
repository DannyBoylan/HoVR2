using UnityEngine;

public class SplineWalkerPlayer : MonoBehaviour
{

    public BezierSpline spline;

    public float duration;

    public bool lookForward; // Make sure player is looking forward on the spline

    public SplineWalkerMode mode;

    private float progress;
    private bool goingForward = true;

    public float distance;
    Vector3 position;
    Vector3 ZPosition;

    public float xDistance;
    public float yDistance;

    private float speedModifier = 2500;
    [Range(0.0f, 5.0f)] // Speed limits minimum and maximum - I tried 0-10 but it was too fast
    public float speedMultiplier;


    private void Update()
    {

        Vector3 D = V3Mul((transform.localPosition - spline.GetPoint(progress)), transform.localPosition - spline.GetPoint(progress));
        distance = Mathf.Sqrt(D.x + D.y + D.z);


        float speed = this.GetComponent<PlayerMovement>().speed / speedModifier;
        if (goingForward)
        {
            progress += Time.deltaTime * speed;
            // Debug.Log(progress);
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
                else
                {
                    progress = 2f - progress;
                    goingForward = false;
                }
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

        position = spline.GetPoint(progress); // Find out where the player is along the spline
        position += GetComponent<PlayerMovement>().offset;
        transform.position = position; // Set position to position on spline
        if (lookForward)
        {
            transform.LookAt(position + spline.GetDirection(progress));
            transform.Rotate(Vector3.up, -90);
        }
    }
    Vector3 V3Mul(Vector3 a, Vector3 b)
    {
        Vector3 rv;
        rv.x = a.x * b.x;
        rv.y = a.y * b.y;
        rv.z = a.z * b.z;
        return rv;
    }
    public Vector3 moveToCentre(Vector3 a)
    {
        Vector3 rv;
        rv = spline.GetPoint(progress) - transform.position;
        float length = Mathf.Sqrt((rv.x * rv.x) + (rv.y * rv.y) + (rv.z * rv.z));
        rv = rv / length; // Normalise

        return a += rv/5;

    }
}