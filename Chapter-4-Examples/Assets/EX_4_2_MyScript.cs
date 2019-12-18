using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EX_4_2_MyScript : MonoBehaviour
{
    // Toggle of what to draw
    public bool DrawAxisFrame = false;
    public bool DrawScaledVector = false;
    public bool DrawUnitVector = false;
    public bool DrawPositionVector = false;
    public bool DrawVectorComponents = false;

    // For defining Va and Vs (ScaledVector)
    public GameObject P1 = null;   // Position P1
    public GameObject P2 = null;   // Position P2
    public float ScalingFactor = 0.8f;

    // For defining Vp (PositionVector)
    public GameObject SphereAtOrigin = null;   // The semi transparent sphere at the origin
    public float SphereRadius = 3.0f;

    // For visualizing all vectors
    private MyVector ShowVa;        // Vector Va
    private MyVector ShowVaScaled;  // Scaled Va
    private MyVector ShowNorm;      // Normalized Va
    private MyVector ShowPositionVector; // Position vector at the origin
    

    // Start is called before the first frame update
    void Start() {
        Debug.Assert(P1 != null);   // Check for proper setup in the editor
        Debug.Assert(P2 != null);
        Debug.Assert(SphereAtOrigin != null);

        // To support show position ad vector at P1
        ShowVa = new MyVector
        {
            VectorColor = Color.black
        };
        ShowNorm = new MyVector
        {
            VectorColor = new Color(0.9f, 0.9f, 0.9f)
        };
        ShowVaScaled = new MyVector
        {
            VectorColor = new Color(0.9f, 0.4f, 0.9f)
        };
        ShowPositionVector = new MyVector
        {
            VectorColor = new Color(0.4f, 0.9f, 0.9f),
            VectorAt = Vector3.zero     // Position Vector at the origin
        };
    }

    // Update is called once per frame
    void Update()
    {
        #region  Visualization on/off: show or hide to avoid clutering
        AxisFrame.ShowAxisFrame = DrawAxisFrame;    // Draw or Hide Axis Frame
        ShowVaScaled.DrawVector = DrawScaledVector; // Display or hide the vectors
        ShowNorm.DrawVector = DrawUnitVector;
        ShowVa.DrawVectorComponents = DrawVectorComponents;
        ShowVaScaled.DrawVectorComponents = DrawVectorComponents;
        ShowNorm.DrawVectorComponents = DrawVectorComponents;
        ShowPositionVector.DrawVector = DrawPositionVector;
        SphereAtOrigin.SetActive(DrawPositionVector);
        #endregion

        #region Vector Va: Compute Va and setup the drawing for Va
        Vector3 vectorVa = P2.transform.localPosition - P1.transform.localPosition;

        // Show the Va vector at P1
        ShowVa.Direction = vectorVa;
        ShowVa.Magnitude = vectorVa.magnitude;
        ShowVa.VectorAt = P1.transform.localPosition;
        #endregion

        if (DrawScaledVector)
        {
            Vector3 vectorVs = ScalingFactor * vectorVa;
            ShowVaScaled.Direction = vectorVs;
            ShowVaScaled.Magnitude = vectorVs.magnitude;
            ShowVaScaled.VectorAt = P1.transform.localPosition;
        }

        if (DrawUnitVector) {
            Vector3 unitVa = (1.0f / vectorVa.magnitude) * vectorVa;  // scale Va by its inversed size
            // Vector3 dirVa = vectorVa.normalized;  // Alternative way of computing normalized Va
            ShowNorm.Direction = unitVa;
            ShowNorm.Magnitude = unitVa.magnitude;
            ShowNorm.VectorAt = P1.transform.localPosition;
        }
                
        if (DrawPositionVector)  {
            Vector3 vectorVp = SphereRadius * vectorVa.normalized;
            ShowPositionVector.Direction = vectorVp;
            ShowPositionVector.Magnitude = vectorVp.magnitude;
            ShowPositionVector.VectorAt = SphereAtOrigin.transform.localPosition;

            // Set the radius of the sphere at the origin
            SphereAtOrigin.transform.localScale = new Vector3(2.0f*SphereRadius, 2.0f * SphereRadius, 2.0f * SphereRadius);
        }
    }
}
