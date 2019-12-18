using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EX_5_2_MyScript : MonoBehaviour
{
    public enum ProjectionChoice
    {
        V1OntoV2,
        V2OntoV1,
        ProjectionOff
    };

    // Three positions to define two vectors: P0->P1, and P0->P2
    public GameObject P0 = null;   // Position P0
    public GameObject P1 = null;   // Position P1
    public GameObject P2 = null;   // Position P2

    public ProjectionChoice ProjChoice = ProjectionChoice.V1OntoV2;

    #region For visualizing the vectors
    private MyVector ShowV1;    // V1
    private MyVector ShowV2;    // V2
    private MyLineSegment ShowProjected;
    private MyLineSegment ShowProjectedAlone;
    private Color PositiveColor = new Color(0.2f, 0.2f, 0.2f, 0.6f);
    private Color NegativeColor = new Color(0.8f, 0.2f, 0.2f, 0.6f);
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        Debug.Assert(P0 != null);   // Verify proper setting in the editor
        Debug.Assert(P1 != null);   
        Debug.Assert(P2 != null);

        #region For visualizing the vectors
        // To support visualizing the vectors
        ShowV1 = new MyVector {
            VectorColor = Color.cyan
        };
        ShowV2 = new MyVector
        {
            VectorColor = Color.magenta
        };
        ShowProjected = new MyLineSegment
        {
            VectorColor = PositiveColor
        };
        ShowProjectedAlone = new MyLineSegment
        {
            VectorColor = PositiveColor,
            VectorAt = Vector3.zero,
            Direction = Vector3.up
        };
        #endregion 
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 v1 = P1.transform.localPosition - P0.transform.localPosition;
        Vector3 v2 = P2.transform.localPosition - P0.transform.localPosition;

        if ((v1.magnitude > float.Epsilon)  &&
            (v2.magnitude > float.Epsilon))
            // make sure v1 and v2 are not zero vectors
        {
            switch (ProjChoice)
            {
                case ProjectionChoice.V1OntoV2:
                    float v1LengthonV2 = Vector3.Dot(v1, v2.normalized);
                    Debug.Log("Projection Result: Length of V1 along V2 = " + v1LengthonV2);
                    break;
                case ProjectionChoice.V2OntoV1:
                    float v2LengthonV1 = Vector3.Dot(v1.normalized, v2);
                    Debug.Log("Projection Result: Length of V2 along V1 = " + v2LengthonV1);
                    break;
                default:
                    Debug.Log("Projection Result: no projection, dot=" + Vector3.Dot(v1, v2));
                    break;
            }
        }

        #region  For visualizing the vectors
        ShowV1.VectorFromTo(P0.transform.localPosition, P1.transform.localPosition);
        ShowV2.VectorFromTo(P0.transform.localPosition, P2.transform.localPosition);
        ShowProjected.DrawVector = (v1.magnitude > float.Epsilon) && (v2.magnitude > float.Epsilon) && (ProjChoice != ProjectionChoice.ProjectionOff);
        ShowProjectedAlone.DrawVector = ShowProjected.DrawVector;
        ShowProjected.VectorColor = PositiveColor;
        ShowProjectedAlone.VectorColor = PositiveColor;
        float length = 0f;
        if (ShowProjected.DrawVector)
        {
            if (ProjChoice == ProjectionChoice.V1OntoV2)
            {
                Vector3 nv = v2.normalized;
                length = Vector3.Dot(v1, nv);
                Vector3 pt = P0.transform.localPosition + length * nv;
                ShowProjected.VectorFromTo(P0.transform.localPosition, pt);
                Debug.DrawLine(P1.transform.localPosition, pt, Color.black);
            }
            else
            {
                Vector3 nv = v1.normalized;
                length = Vector3.Dot(v2, nv);
                Vector3 pt = P0.transform.localPosition + length * nv;
                ShowProjected.VectorFromTo(P0.transform.localPosition, pt);
                Debug.DrawLine(P2.transform.localPosition, pt, Color.black);
            }
            ShowProjectedAlone.VectorAt = P0.transform.localPosition - Vector3.right;
            ShowProjectedAlone.Magnitude = ShowProjected.Magnitude;
            if (length > 0)
            {
                ShowProjectedAlone.Direction = Vector3.up;
                ShowProjectedAlone.VectorColor = Color.black;
                ShowProjected.VectorColor = PositiveColor;
            } else
            {
                ShowProjectedAlone.Direction = -Vector3.up;
                ShowProjectedAlone.VectorColor = Color.red;
                ShowProjected.VectorColor = NegativeColor;
            }
        }
        #endregion

    }
}