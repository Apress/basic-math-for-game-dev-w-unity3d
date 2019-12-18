using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EX_5_1_MyScript : MonoBehaviour
{
    // Three positions to define two vectors: P0->P1, and P0->P2
    public GameObject P0 = null;   // Position P0
    public GameObject P1 = null;   // Position P1
    public GameObject P2 = null;   // Position P2

    public bool DrawThePlane = true;

    #region For visualizing the vectors
    private MyVector ShowV1;    // V1
    private MyVector ShowV2;    // V2
    private MyLineSegment ShowDot;
    private MyXZPlane ShowPlane; // Plane where XZ lies

    private Color NegativeColor = Color.red;
    private Color PositiveColor = Color.black;
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
        ShowDot = new MyLineSegment
        {
            VectorAt = Vector3.zero,
            LineWidth = 0.07f
        };
        ShowPlane = new MyXZPlane
        {
            PlaneColor = new Color(0.3f, 0.8f, 0.3f, 0.5f),
            XSize = 0.5f,
            YSize = 0.5f,
            ZSize = 0.5f
        };
        #endregion 
    }

    // Update is called once per frame
    void Update()
    {
        float cosTheta = float.NaN;
        float theta = float.NaN;
        Vector3 v1 = P1.transform.localPosition - P0.transform.localPosition;
        Vector3 v2 = P2.transform.localPosition - P0.transform.localPosition;
        float dot = Vector3.Dot(v1, v2);
        if ((v1.magnitude > float.Epsilon) && (v2.magnitude > float.Epsilon))
        {
            cosTheta = dot / (v1.magnitude * v2.magnitude);
            // Alternatively,
            //   costTheta = Vector3.Dot(v1.normalize, v2.normalize)
            theta = Mathf.Acos(cosTheta) * Mathf.Rad2Deg;
        }
        Debug.Log("Dot result=" + dot + " cosTheta=" + cosTheta + " angle=" + theta);

        #region  For visualizing the vectors
        ShowPlane.DrawPlane = DrawThePlane;

        ShowV1.VectorFromTo(P0.transform.localPosition, P1.transform.localPosition);
        ShowV2.VectorFromTo(P0.transform.localPosition, P2.transform.localPosition);
        Vector3 dotP = Vector3.zero;

        ShowPlane.DrawPlane &= cosTheta != float.NaN;
        ShowDot.DrawVector = cosTheta != float.NaN;
        if (cosTheta != float.NaN)
        {
            float r = 3f * Mathf.Abs(cosTheta);
            dotP.x = r * cosTheta;
            dotP.y = r * Mathf.Sin(theta * Mathf.Deg2Rad);

            ShowDot.VectorFromTo(Vector3.zero, dotP);

            if (Mathf.Abs(cosTheta) < (1f - float.Epsilon))
            {
                // The plane
                Vector3 n = Vector3.Cross(v1, v2);
                if (Vector3.Dot(n, Vector3.forward) > 0)
                    n = -n;

                float s = Mathf.Max(v1.magnitude, v2.magnitude);
                ShowPlane.XSize = ShowPlane.ZSize = s * 0.7f;
                ShowPlane.PlaneNormal = n;
                ShowPlane.Center = (P0.transform.localPosition + P1.transform.localPosition + P2.transform.localPosition) / 3f;
            } else
            {
                ShowPlane.DrawPlane = false;
            }
        }

        if (dot > 0f)
            ShowDot.VectorColor = PositiveColor;
        else
            ShowDot.VectorColor = NegativeColor;
        #endregion

    }
}