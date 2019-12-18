using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EX_2_4_MyScript : MonoBehaviour
{
    private MyBoxBound CarBound = null;     // For visualizing the three bounding boxes
    private MyBoxBound TaxiBound = null;
    private MyBoxBound OverlapBox = null;

    public bool DrawBox = true;             // Controls what to show/hide
    public bool DrawIntervals = false;

    public Vector3 CarCenterOffset = Vector3.zero;  // Offset between the geometry and box centers
    // The y-center of the car is at ground level

    public GameObject TheTaxi = null;       // Reference to the Taxi game object
    public Vector3 TaxiBoundSize = Vector3.one; // User sets desirable taxi bounding box size
    
    public GameObject TheCar = null;        // Reference to the Car game object
    public Vector3 CarBoundSize = Vector3.one;  // User sets the desirable car bounding box size

    public Vector3 OverlapBoxMin = Vector3.zero;    // Min position of the overlapping bounding box
    public Vector3 OverlapBoxMax = Vector3.zero;    // Max position of the overlapping bounding box

    // Start is called before the first frame update
    void Start()
    {
        Debug.Assert(TheTaxi != null);      // Ensure that proper reference setup in Inspector Window
        Debug.Assert(TheCar != null);

        TaxiBound = new MyBoxBound();       // Instantiate the visualization variables
        CarBound = new MyBoxBound();
        OverlapBox = new MyBoxBound();
        OverlapBox.SetBoxColor(new Color(0.4f, 0.9f, 0.9f, 0.6f));

        OverlapBox.DrawBoundingBox = false; // hide the overlap box initially
        OverlapBox.DrawIntervals = false; // not showing this in this example
    }

    // Update is called once per frame
    void Update()
    {
        // Step 1: Set the user specify drawing state
        TaxiBound.DrawBoundingBox = DrawBox;
        TaxiBound.DrawIntervals = DrawIntervals;
        CarBound.DrawBoundingBox = DrawBox;
        CarBound.DrawIntervals = DrawIntervals;

        // Step 2: Update the bounds (Taxi first, then Car)
        TaxiBound.Center = TheTaxi.transform.localPosition + CarCenterOffset;
        TaxiBound.Size = TaxiBoundSize;
        CarBound.Center = TheCar.transform.localPosition + CarCenterOffset; 
        CarBound.Size = CarBoundSize;
        
        // Step 3: test for intersection ... 
        // Two box bounds overlap when all three intervals overlap ...
        if (((TaxiBound.MinPosition.x <= CarBound.MaxPosition.x) &&   //     X-interval overlap
             (TaxiBound.MaxPosition.x >= CarBound.MinPosition.x))
            &&                                                        // AND
            ((TaxiBound.MinPosition.y <= CarBound.MaxPosition.y) &&   //     Y-interval overlap
             (TaxiBound.MaxPosition.y >= CarBound.MinPosition.y))
            &&                                                        // AND
            ((TaxiBound.MinPosition.z <= CarBound.MaxPosition.z) &&   //     Z-interval overlap
             (TaxiBound.MaxPosition.z >= CarBound.MinPosition.z)))
        {
            // Min/Max of the overlap box bound
            Vector3 min = new Vector3(
                    Mathf.Max(TaxiBound.MinPosition.x, CarBound.MinPosition.x),   // min x position
                    Mathf.Max(TaxiBound.MinPosition.y, CarBound.MinPosition.y),   // min y position
                    Mathf.Max(TaxiBound.MinPosition.z, CarBound.MinPosition.z));  // min z position
            Vector3 max = new Vector3(
                    Mathf.Min(TaxiBound.MaxPosition.x, CarBound.MaxPosition.x),   // max x position
                    Mathf.Min(TaxiBound.MaxPosition.y, CarBound.MaxPosition.y),   // max y position
                    Mathf.Min(TaxiBound.MaxPosition.z, CarBound.MaxPosition.z));  // max z position
            OverlapBox.DrawBox = TaxiBound.DrawBox;
            OverlapBox.DrawIntervals = TaxiBound.DrawIntervals;
            OverlapBox.MinPosition = min;
            OverlapBox.MaxPosition = max;

            // Update to show the overlap bound's min and max
            OverlapBoxMax = max;
            OverlapBoxMin = min;

            // The same functionality is implemented in the BoxBound
            Debug.Assert(TaxiBound.BoxesIntersect(CarBound));
        } else
        {
            OverlapBox.DrawBox = false;
            OverlapBox.DrawIntervals = false;
        }
    }
}