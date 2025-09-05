using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vectors : MonoBehaviour
{
    float speed = .01f;
    GameObject sphere;
    public Vector3 direction; //2, 3
    public Vector3 dirNormalised; //3
    public float dirMagnitude; //3
    public float dirMagnitude3D; //4
    public float angleFromSphere;

    System.Numerics.Vector3 v1;
    System.Numerics.Vector3 v2;

    void Start()
    {
        sphere = GameObject.Find("Sphere");
        dirNormalised = direction;
        dirMagnitude = direction.magnitude;
        direction = (sphere.transform.position - transform.position);

        float[] p1 = new float[3] { transform.position.x, transform.position.y, transform.position.z };
        float[] p2 = new float[3] { sphere.transform.position.x, sphere.transform.position.y, sphere.transform.position.z };
        dirMagnitude3D = MathsUtility.DistanceBetween(p1, p2);


        //NormaliseDirectionVector(1);
        RotateWithCrossProduct();

        Debug.Log("Where they are: " + MyMath.Dot(transform.forward, dirNormalised));
    }

    // Update is called once per frame
    void Update()
    {
        //1. dynamic vector = point2 - point1
        //transform.position += ((sphere.transform.position - transform.position) * speed);

        //2. linear animation has a vector that is cached once
        //if((transform.position - sphere.transform.position).magnitude > 0.1f)
        //transform.position += direction * speed;

        //3. take control of the speed by controlling the magnitude of the vector between 2 points
        if ((transform.position - sphere.transform.position).magnitude > 0.1f)
        {
            transform.position += dirNormalised * speed;
            
            //Debug.DrawLine(transform.position, sphere.transform.position, Color.red);

        }
    }


    public void NormaliseDirectionVector(short alternative)
    {
        switch (alternative) {
            case 1:
                    float[] norm = MathsUtility.GetNormal(direction.x, direction.y, direction.z);
                    dirNormalised.x = norm[0];
                    dirNormalised.y = norm[1];
                    dirNormalised.z = norm[2];
                break;

            case 2:
                dirNormalised.Set(dirNormalised.x / dirMagnitude, dirNormalised.y / dirMagnitude, dirNormalised.z / dirMagnitude);
                dirNormalised /= Mathf.Sqrt(Mathf.Pow(direction.x, 2) + Mathf.Pow(direction.z, 2));
                break;
            case 3:
                dirNormalised.Normalize();
                break;
            default: break;
        }
    }

    public void RotateWithCrossProduct()
    {
        //Get forward vector
        v1 = new System.Numerics.Vector3(0, 0, 1);
        //Get direction of target vector
        v2 = new System.Numerics.Vector3(dirNormalised.x, dirNormalised.y, dirNormalised.z);
        Debug.DrawRay(transform.position, dirNormalised, Color.black, 5);

        //Now calculate the angle between the two vectors
        angleFromSphere = (float)MathsUtility.RadianAngleBetweenVectors(v1, v2);// * (180 / Mathf.PI);
        Debug.Log("angleFromSphere: " + angleFromSphere);

        //Now cross the vectors and get a 3rd vector that shows in which direction your object should be turning
        System.Numerics.Vector3 cross = MathsUtility.Cross(v1, v2);
        Debug.Log("cross product: " + cross.ToString());
        //Now pass on if it's clockwise or not to the rotate function
        bool clockwise = cross.Y > 0;
        Debug.Log("clockwise: " + clockwise);

        //Now calculate the desired rotation based on my current forward, the angle to turn, and whether it's clockwise or not.
        System.Numerics.Vector3 rotation = MathsUtility.Rotate(new System.Numerics.Vector3(0, 0, 1), angleFromSphere, clockwise);
        Vector3 rot = new Vector3(rotation.X, rotation.Y, rotation.Z);
        Debug.Log("rot: " + rot.ToString());

        //NOW actually rotate the object
        transform.forward = rot;

    }

    public void RotateClean()
    {
        //Show direction of target vector
        Debug.DrawRay(transform.position, dirNormalised, Color.black, 5);

        //Now calculate the angle between the two vectors
        angleFromSphere = MyMath.GetAngle(transform.forward, dirNormalised);
        Debug.Log("angleFromSphere: " + angleFromSphere);
        /*/Now cross the vectors and get a 3rd vector that shows in which direction your object should be turning
        System.Numerics.Vector3 cross = MathsUtility.Cross(v1, v2);
        Debug.Log("cross product: " + cross.ToString());
        //Now pass on if it's clockwise or not to the rotate function
        bool clockwise = cross.Y > 0;
        Debug.Log("clockwise: " + clockwise);

        //Now calculate the desired rotation based on my current forward, the angle to turn, and whether it's clockwise or not.
        System.Numerics.Vector3 rotation = MathsUtility.Rotate(new System.Numerics.Vector3(0, 0, 1), angleFromSphere, clockwise);
        Vector3 rot = new Vector3(rotation.X, rotation.Y, rotation.Z);
        Debug.Log("rot: " + rot.ToString());

        //NOW actually rotate the object
        transform.forward = rot;*/

    }
}
