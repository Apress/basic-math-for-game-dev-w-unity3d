using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EX_2_2_MyScript : MonoBehaviour
{
    private MyBoxBound MyBound = null;      // For visualizing the bounding box

    public bool DrawBox = true;             // Show or hide the 3D box
    public bool DrawIntervals = true;       // Show or hide the three intervals

    public bool ControlWithMinMax = true;   // Control the box with min/max vs. center/size
    public GameObject MinPos = null;        // Min position of the box
    public GameObject MaxPos = null;        // Max position of the box
    public GameObject CenterPos = null;     // Center position of the box
    public Vector3 BoundSize = Vector3.one; // Interval size on each axis

    public GameObject TestPosition = null;  // position for performing inside/outside test
    
    // Start is called before the first frame update
    void Start()
    {
        Debug.Assert(CenterPos != null);    // Ensure proper setup in the Hierarchy Window
        Debug.Assert(MinPos != null);
        Debug.Assert(MaxPos!= null);
        Debug.Assert(TestPosition != null);

        MyBound = new MyBoxBound();         // Instantiate for visualization
    }

    // Update is called once per frame
    void Update() {
        // Step 1: update drawing options
        MyBound.DrawBoundingBox = DrawBox;
        MyBound.DrawIntervals = DrawIntervals;
           
        // Step 2: control the box
        if (ControlWithMinMax) {
            // User controls Min/Max Position, set for visualization
            MyBound.MinPosition = MinPos.transform.localPosition;
            MyBound.MaxPosition = MaxPos.transform.localPosition;

            // Show current bound center and size in the MyScript component
            BoundSize = MaxPos.transform.localPosition - MinPos.transform.localPosition;
            CenterPos.transform.localPosition = 0.5f * (MaxPos.transform.localPosition + MinPos.transform.localPosition);
        } else {
            // User control center position and the size, set for visualization 
            MyBound.Center = CenterPos.transform.localPosition;
            MyBound.Size = BoundSize;

            // Show Min/Max Position in the MyScript component
            MinPos.transform.localPosition = CenterPos.transform.localPosition - (0.5f * BoundSize);
            MaxPos.transform.localPosition = CenterPos.transform.localPosition + (0.5f * BoundSize);
        }

        // Step 3: perform inside/outside test
        Vector3 pos = TestPosition.transform.localPosition;
        Vector3 min = MinPos.transform.localPosition;
        Vector3 max = MaxPos.transform.localPosition;
        if ( (pos.x > min.x) && (pos.x < max.x) &&   // if point in x-interval   AND
             (pos.y > min.y) && (pos.y < max.y) &&   //    point in y-interval   AND
             (pos.z > min.z) && (pos.z < max.z) )    //    point in z-interval   
        {
            Debug.Log("TestPosition Inside!");
            MyBound.SetBoxColor(MyDrawObject.CollisionColor);
        } else {
            MyBound.ResetBoxColor();
        }
    }
}

