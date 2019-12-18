using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EX_4_1_MyScript : MonoBehaviour
{
    // For visualizing the two vectors
    public bool DrawAxisFrame = true; // Draw or Hide The AxisFrame
    public bool DrawPositionAsVector = true;
    public bool DrawVectorAsPosition = false;

    private MyVector ShowVd;        // From Origin to Pd
    private MyVector ShowVdAtP1;    // Show Vd at P1
    private MyVector ShowVe;        // From Origin to Pe
    private MyVector ShowVeAtPi;    // Show Ve from Pi to Pj

    // Support position Pd as a vector from P1 to P2
    public GameObject P1 = null;   // Position P1
    public GameObject P2 = null;   // Position P2
    public GameObject Pd = null;   // Position vector: Pd

    // Support vector defined by Pi to Pj, and show as Pe
    public GameObject Pi = null;   // Position Pi
    public GameObject Pj = null;   // Position Pj
    public GameObject Pe = null;   // Position vector: Pe


    // Start is called before the first frame update
    void Start()
    {
        Debug.Assert(P1 != null);   // Verify proper setting in the editor
        Debug.Assert(P2 != null);
        Debug.Assert(Pd != null);
        Debug.Assert(Pi != null);
        Debug.Assert(Pj != null);
        Debug.Assert(Pe != null);

        // To support visualizing the vectors
        ShowVd = new MyVector {
            VectorColor = Color.black,
            VectorAt = Vector3.zero     // Always draw Vd from the origin
        };
        ShowVdAtP1 = new MyVector
        {
            VectorColor = new Color(0.9f, 0.9f, 0.9f)
        };

        // To support show vector from Pi to Pj as position vector
        ShowVe = new MyVector
        {
            VectorColor = new Color(0.2f, 0.0f, 0.2f),
            VectorAt = Vector3.zero    // Always draw Ve from the origin
        };        
        ShowVeAtPi = new MyVector() {
            VectorColor = new Color(0.9f, 0.2f, 0.9f)
        };
    }

    // Update is called once per frame
    void Update()
    {
        #region  Visualization on/off: show or hide to avoid clutering
        AxisFrame.ShowAxisFrame = DrawAxisFrame;    // Draw or Hide Axis Frame
        P1.SetActive(DrawPositionAsVector);         // objects that suppor position as vector
        P2.SetActive(DrawPositionAsVector);
        Pd.SetActive(DrawPositionAsVector);
        Pi.SetActive(DrawVectorAsPosition);         // objects that support vector as positions 
        Pj.SetActive(DrawVectorAsPosition);
        Pe.SetActive(DrawVectorAsPosition);
        ShowVdAtP1.DrawVector = DrawPositionAsVector;    // Display or hide the vectors
        ShowVd.DrawVector = DrawPositionAsVector;
        ShowVeAtPi.DrawVector = DrawVectorAsPosition;
        ShowVe.DrawVector = DrawVectorAsPosition;
        #endregion

        #region Position Vector: Show Pd as a vector at P1
        if (DrawPositionAsVector)
        {
            // Use position of Pd as position vector
            Vector3 vectorVd = Pd.transform.localPosition;

            // Step 1: take care of visualization
            //         for Vd
            ShowVd.Direction = vectorVd;
            ShowVd.Magnitude = vectorVd.magnitude;

            //         apply Vd at P1
            ShowVdAtP1.VectorAt = P1.transform.localPosition;   // Always draw at P1
            ShowVdAtP1.Magnitude = vectorVd.magnitude;          // get from vectorVd
            ShowVdAtP1.Direction = vectorVd;

            // Step 2: demonstrate P2 is indeed Vd away from P1
            P2.transform.localPosition = P1.transform.localPosition + vectorVd;
        }
        #endregion

        #region Vector from two points: Show Ve as the position Pe
        if (DrawVectorAsPosition)
        {
            // Use from Pi to Pj as vector for Ve
            Vector3 vectorVe = Pj.transform.localPosition - Pi.transform.localPosition;

            // Step 1: Take care of visualization
            //         for Ve: from Pi to Pj
            ShowVeAtPi.VectorFromTo(Pi.transform.localPosition, Pj.transform.localPosition);
            //         Show as Ve at the origin
            ShowVe.Direction = vectorVe;
            ShowVe.Magnitude = vectorVe.magnitude;

            // Step 2: demonstrate Pe is indeed Ve away from the origin
            Pe.transform.localPosition = vectorVe;
        }
        #endregion
    }
}