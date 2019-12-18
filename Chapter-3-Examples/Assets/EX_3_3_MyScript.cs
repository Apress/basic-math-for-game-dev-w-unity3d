using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EX_3_3_MyScript : MonoBehaviour
{
    private MySphereBound TaxiBound = null;
    private MySphereBound CarBound = null;

    public GameObject TheTaxi = null;
    public float TaxiBoundRadius = 2.0f;
    public bool DrawTaxiBound = true;

    public GameObject TheCar = null;
    public float CarBoundRadius = 2.0f;
    public bool DrawCarBound = true;

    public float DistanceBetween = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Assert(TheTaxi != null);	    // Make sure proper editor setup
        Debug.Assert(TheCar != null);

        TaxiBound = new MySphereBound();
        CarBound = new MySphereBound();
    }

    // Update is called once per frame
    void Update()
    {
        // Step 1: Assume no intersection
        TaxiBound.BoundColor = MySphereBound.NoCollisionColor;
        CarBound.BoundColor = MySphereBound.NoCollisionColor;

        // Step 2: Update the Taxi sphere bound
        TaxiBound.Center = TheTaxi.transform.localPosition;
        TaxiBound.Radius = TaxiBoundRadius;
        TaxiBound.DrawBound = DrawTaxiBound;

        // Step 3: Update the Car sphere bound
        CarBound.Center = TheCar.transform.localPosition;
        CarBound.Radius = CarBoundRadius;
        CarBound.DrawBound = DrawCarBound;

        // Step 4: Compute the distance between the sphere bounds as magnitude of a Vector3 
        Vector3 diff = TaxiBound.Center - CarBound.Center;
        DistanceBetween = diff.magnitude;

        // Step 5: Testing and showing intersection status
        bool hasIntersection = DistanceBetween <= (TaxiBound.Radius + CarBound.Radius);
        if (hasIntersection)
        {
            Debug.Log("Intersect!! Distance:" + DistanceBetween);
            TaxiBound.BoundColor = MySphereBound.CollisionColor;
            CarBound.BoundColor = MySphereBound.CollisionColor;

            // The collision functionality is supported by the MySphereBound class as well
            Debug.Assert(TaxiBound.SpheresIntersects(CarBound)); 
        }
    }
}