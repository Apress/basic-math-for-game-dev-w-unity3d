using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EX_3_2_MyScript : MonoBehaviour
{
    public GameObject APoint = null;            // The CheckerSphere position

    private MySphereBound SphereBound = null;   // To visualize the car sphere bound
    public GameObject TheCar = null;            // Reference to the car
    public float CarBoundRadius = 2.0f;         // Size (radius) of the sphere bound 
    public bool DrawCarBound = true;            // To draw or hide the bound

    public float DistanceBetween = 0.0f;        // Distance between the car and APoint

    // Start is called before the first frame update
    void Start()
    {
        Debug.Assert(APoint != null);	    // Make sure proper editor setup
        Debug.Assert(TheCar != null);

        SphereBound = new MySphereBound();  // To visualize the sphere bound
    }

    // Update is called once per frame
    void Update()
    {
        // Step 1: Assume no collision
        SphereBound.BoundColor = MySphereBound.NoCollisionColor;

        // Step 2: Update the sphere bound
        SphereBound.Center = TheCar.transform.localPosition;
        SphereBound.Radius = CarBoundRadius;    // Set the radius
        SphereBound.DrawBound = DrawCarBound;   // Show or Hide the bound

        // Step 3: Compute the distance between APoint and Center of Sphere Bound
        Vector3 diff = TheCar.transform.localPosition - APoint.transform.localPosition;
        DistanceBetween = diff.magnitude;

        // Step 4: Testing and showing collision status
        bool isInside = (DistanceBetween <= CarBoundRadius);
        // TheCar.SetActive(!isInside);  // what does this do?
        if (isInside)
        {
            Debug.Log("Inside!! Distance:" + DistanceBetween);
            SphereBound.BoundColor = MySphereBound.CollisionColor;

            // The Inside/Outside functionality is supported by the MySphereCollider class as well
            Debug.Assert(SphereBound.PointInSphere(APoint.transform.localPosition));
        }
    }
}

