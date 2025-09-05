using System;
using System.Numerics;
//using UnityEngine;

public class MathsUtility
{
    public static float[] GetNormal(float x, float y, float z)
    {
        //Längden av vektorn
        double magnitude = (x * x) + (y * y) + (z * z); // Mathf.Sqrt(Mathf.Pow(x, 2) + Mathf.Pow(y, 2));
        magnitude = Math.Sqrt(magnitude);
        //Normalisera ny vektor
        //return new Vector2(x/magnitude, y/magnitude);
        return new float[3] { (float)(x / magnitude), (float)(y / magnitude), (float)(z / magnitude) };
    }

    
    public static float DistanceBetween(float[] p1, float[] p2)
    {
        /*float dist = Mathf.Pow((p2.x - p1.x), 2) + Mathf.Pow((p2.y - p1.y), 2);
        dist = Mathf.Sqrt(dist);*/

        float dist =  Square(p2[0] - p1[0]) +
                      Square(p2[1] - p1[1]) +
                      Square(p2[2] - p1[2]);

        dist = (float)Math.Sqrt(dist);

        return dist;
    }

    //Hypotenuse between p1 to p2
    public static float DistanceBetween(Vector3 p1, Vector3 p2)
    {

        float dist = (Square(p2.X - p1.X) +
                      Square(p2.Y - p1.Y) +
                      Square(p2.Z - p1.Z)
                     );

        dist = (float)Math.Sqrt(dist);

        return dist;
    }

    //Where one position is in relation to another.
    public static float DotProduct(Vector3 v1, Vector3 v2)
    {
        return (v1.X * v2.X) +
               (v1.Y * v2.Y) +
               (v1.Z * v2.Z);
    }

    //public static float Angle() { return 0; }
    public static double RadianAngleBetweenVectors(Vector3 v1, Vector3 v2)
    {
        //1. Get dot product of vectors
        float product = DotProduct(v1, v2);

        //2. Get the multiplied product of both vectors' magnitudes
        float distance = DistanceBetween(Vector3.Zero, v1) * DistanceBetween(Vector3.Zero, v2);
        //double magnitude1 = Math.Sqrt( (double) (Square(v1.X) + Square(v1.Y) + Square(v1.Z)) );
        //double magnitude2 = Math.Sqrt( (double) (Square(v2.X) + Square(v2.Y) + Square(v2.Z)) );
        //double distance = magnitude1 * magnitude2;

        //3. Get the angle between the vectors with negative cosining 1./2.
        double angle = Math.Acos(product / distance);

        return angle;    //Radians. For degrees * 180/Mathf.PI;
    }

    public static float Square(float num)
    {
        return num * num;
    }

    public static Vector3 Rotate(Vector3 orgvector, float angle, bool clockwise)
    { // radians
        if (clockwise)
        {
            angle = (float)(2 * Math.PI) - angle;
        }
        float newX = (float)(orgvector.X * Math.Cos(angle) - (orgvector.Z * Math.Sin(angle)));
        float newZ = (float)(orgvector.X * Math.Sin(angle) + (orgvector.Z * Math.Cos(angle)));

        return new Vector3(newX, 0, newZ);
    }

    public static Vector3 Cross(Vector3 orgVector, Vector3 targetVector)
    {
        float x = (orgVector.Y * targetVector.Z) - (orgVector.Z * targetVector.Y);
        float y = (orgVector.Z * targetVector.X) - (orgVector.X * targetVector.Z);
        float z = (orgVector.X * targetVector.Y) - (orgVector.Y * targetVector.X);

        Vector3 newVector = new System.Numerics.Vector3(x, y, z);

        return newVector;
    }

    
    //----------- Unity Vector Converter ------------//
/*    public UnityEngine.Vector3 ToUnityVector(System.Numerics.Vector3 vector)
    {
        return new UnityEngine.Vector3(vector.X, vector.Y, vector.Z);
    }*/

}
