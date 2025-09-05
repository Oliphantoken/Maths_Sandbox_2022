using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyMath
{
    public static float GetMagnitude(Vector3 vec)
    {
        return GetMagnitude(vec.x, vec.y, vec.z);
    }

    public static float GetMagnitude(float x, float y, float z)
    {
        float magnitude = Square(x) + Square(y) + Square(z);
        magnitude = Mathf.Sqrt(magnitude);
        return magnitude;
    }

    public static Vector3 GetNormal(Vector3 vec)
    {
        return GetNormal(vec.x, vec.y, vec.z);
    }
    public static Vector3 GetNormal(float x, float y, float z)
    {
        float magnitude = GetMagnitude(x, y, z);

        x /= magnitude;
        y /= magnitude;
        z /= magnitude;

        return new Vector3(x, y, z);
    }

    public static float Dot(Vector3 dir1, Vector3 dir2)
    {
        return  dir1.x * dir2.x +
                dir1.y * dir2.y +
                dir1.z * dir2.z;
    }


    /// <param name="from"></param>
    /// <param name="to"></param>
    /// <returns>Angle in Radians</returns>
    public static float GetAngle(Vector3 from, Vector3 to)
    {
        float magnitude1 = GetMagnitude(from);
        float magnitude2 = GetMagnitude(to);

        float angle = Mathf.Acos(magnitude1 / magnitude2);
        return angle;
    }


    ///<Summary>Rotates in the xz-space, around the Y-axis.
    ///Give the angle in Radian. </Summary>
    public static Vector3 RotateVector(Vector3 from, float angle, bool clockwise = false)
    {
        if (clockwise)
        {
            angle = (float)(2 * Math.PI) - angle;
        }
        float newX = (float)(from.x * Math.Cos(angle) - (from.x * Math.Sin(angle)));
        float newZ = (float)(from.z * Math.Sin(angle) + (from.z * Math.Cos(angle)));

        return new Vector3(newX, 0, newZ);
    }

    public static float Distance(Vector3 from, Vector3 to)
    {
        float dist = Square(to.x - from.x) +
                     Square(to.y - from.y) +
                     Square(to.z - from.z);
        dist = Mathf.Sqrt(dist);

        return dist;
    }

    public static float Square(float num)
    {
        return num * num;
    }

}
