using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EX_2_1_MyScript : MonoBehaviour
{
    private MyIntervalBoundInY AnInterval = null;
    public float IntervalMax = 1.0f;
    public float IntervalMin = 0.0f;
        
    public GameObject TestPosition = null;   // Use sphere to represent a position
    
    // Start is called before the first frame update
    void Start()
    {
        AnInterval = new MyIntervalBoundInY();
    }

    // Update is called once per frame
    void Update()
    {
        // Updates AnInteval with values entered by the user
        AnInterval.MinValue = IntervalMin;
        AnInterval.MaxValue = IntervalMax;
        AnInterval.IntervalColor = MyDrawObject.NoCollisionColor;  // assume point is outside

        // computes inside/outside of the current TestPosition.y value
        Vector3 pos = TestPosition.transform.localPosition;
        bool isInside = (pos.y >= IntervalMin) && (pos.y <= IntervalMax);           

        if (isInside)
        {
            Debug.Log("Position In Interval! (" + IntervalMin + ", " + IntervalMax + ")" );
            AnInterval.IntervalColor = MyDrawObject.CollisionColor;

            // The inside functionality is also supported by MyYInterval
            Debug.Assert(AnInterval.ValueInInterval(pos.y));
        }

    }
}

