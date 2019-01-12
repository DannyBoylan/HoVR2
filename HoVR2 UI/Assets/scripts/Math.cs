using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Math : MonoBehaviour {
    //Basic math functions for Vector3 to help save time


    /////////////////////////////////
    ////         DIVISION        ////
    /////////////////////////////////

    public static Vector3 DivideVector(Vector3 A, Vector3 B)
    {
        Vector3 rv;
        rv.x = A.x / B.x;
        rv.y = A.y / B.y;
        rv.z = A.z / B.z;
        return rv;
    }
    public static Vector3 DivideVector(Vector3 A, float divisor)
    {
        Vector3 rv;
        rv.x = A.x / divisor;
        rv.y = A.y / divisor;
        rv.z = A.z / divisor;
        return rv;
    }
    /////////////////////////////////
    ////       MULTIPLICATION    ////
    /////////////////////////////////

    public static Vector3 MultiplyVector(Vector3 A, Vector3 B)
    {
        Vector3 rv;
        rv.x = A.x * B.x;
        rv.y = A.y * B.y;
        rv.z = A.z * B.z;
        return rv;
    }
    public static Vector3 MultiplyVector(Vector3 A, float scalar)
    {
        Vector3 rv;
        rv.x = A.x * scalar;
        rv.y = A.y * scalar;
        rv.z = A.z * scalar;
        return rv;
    }
    ///////////////////////////////////////
    ////          Cross Product        ////
    ///////////////////////////////////////
    public static Vector3 VectorCrossProduct(Vector3 A, Vector3 B)
    {
        Vector3 C = new Vector3(((A.y * B.z) - (A.z * B.y)), ((A.z * B.x) - (A.x * B.z)), ((A.x * B.y) - (A.y * B.x)));
        return C;
    }

    /////////////////////////////////
    ////        Dot Product      ////
    /////////////////////////////////
    public static float DotProduct(Vector3 A, Vector3 B, bool normalize = true)
    {
        if (normalize == true)
        {
            A = NormVector(A);
            B = NormVector(B);
        }

        float rv = (A.x * B.x) + (A.y * B.y) + (A.z * B.z);
        return rv;
    }
    /////////////////////////////////
    ////          length         ////
    /////////////////////////////////

    public static float VectorLength(Vector3 A)
    {
        float rv = Mathf.Sqrt((A.x * A.x) + (A.y * A.y) + (A.z * A.z));
        return rv;
    }
    /////////////////////////////////
    ////         Normalize       ////
    /////////////////////////////////
    public static Vector3 NormVector(Vector3 A)
    {
        Vector3 rv = DivideVector(A, VectorLength(A));
        return rv;
    }
    /////////////////////////////////
    ////          Distance         ////
    /////////////////////////////////

    public static float Distance(Vector3 A, Vector3 B)
    {
        Vector3 C = MultiplyVector((A - B), (A - B));
        float rv = Mathf.Sqrt(C.x + C.y + C.z);
        return rv;
    }
}
