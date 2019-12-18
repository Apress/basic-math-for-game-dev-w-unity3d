using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EX_6_1_MyScript : MonoBehaviour
{
    // Three positions to define two vectors: P0->P1, and P0->P2
    public GameObject P0 = null;   // Position P0
    public GameObject P1 = null;   // Position P1
    public GameObject P2 = null;   // Position P2

    public bool DrawThePlane = true;
    public bool DrawV1xV2 = true;
    public bool DrawV2xV1 = true;
    public float Factor = 0.4f;

    #region For visualizing the vectors
    private MyVector ShowV1;    // V1
    private MyVector ShowV2;    // V2
    private MyVector ShowV1xV2, ShowV2xV1;
    private MyXZPlane ShowPlane; // Plane where XZ lies

    private Color V1xV2Color = Color.black;
    private Color V2xV1Color = Color.red;
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
        ShowV1xV2 = new MyVector
        {
            VectorColor = V1xV2Color
        };
        ShowV2xV1 = new MyVector
        {
            VectorColor = V2xV1Color
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
        Vector3 v1 = P1.transform.localPosition - P0.transform.localPosition;
        Vector3 v2 = P2.transform.localPosition - P0.transform.localPosition;
        Vector3 v1xv2 = Vector3.Cross(v1, v2);
        Vector3 v2xv1 = Vector3.Cross(v2, v1);

        float d = Vector3.Dot(v1.normalized, v2.normalized);
        bool notParallel = (Mathf.Abs(d) < (1.0f - float.Epsilon));

        if (notParallel)
        {
            float theta = Mathf.Acos(d) * Mathf.Rad2Deg;
            float cd = Vector3.Dot(v1xv2.normalized, v2xv1.normalized);
            float dv1 = Vector3.Dot(v1xv2, v1);
            float dv2 = Vector3.Dot(v1xv2, v2);
            Debug.Log(" theta=" + theta + "  v1xv2=" + v1xv2 + "  v2xv1=" + v2xv1 + "  v1xv2-dot-v2xv1=" + cd + " Dot with v1/v2=" + dv1 + " " + dv2);
        } else
        {
            Debug.Log("Two vectors are parallel, cross product is a zero vector");
        }

        #region  For visualizing the vectors
        ShowPlane.DrawPlane = DrawThePlane;
        ShowV1xV2.DrawVector = DrawV1xV2;
        ShowV2xV1.DrawVector = DrawV2xV1;

        ShowV1.VectorFromTo(P0.transform.localPosition, P1.transform.localPosition);
        ShowV2.VectorFromTo(P0.transform.localPosition, P2.transform.localPosition);
        

        ShowPlane.DrawPlane &= notParallel;
        ShowV1xV2.DrawVector &= notParallel;
        ShowV2xV1.DrawVector &= notParallel;

        if (notParallel)
        {
            ShowV1xV2.VectorAt = P0.transform.localPosition;
            ShowV1xV2.Magnitude = v1xv2.magnitude * Factor;
            ShowV1xV2.Direction = v1xv2;

            ShowV2xV1.VectorAt = P0.transform.localPosition;
            ShowV2xV1.Magnitude = v2xv1.magnitude * Factor;
            ShowV2xV1.Direction = v2xv1;

            // The plane
            Vector3 n = Vector3.Cross(v1, v2);
            if (Vector3.Dot(n, Vector3.forward) > 0)
                n = -n;

            float s = Mathf.Max(v1.magnitude, v2.magnitude);
            ShowPlane.XSize = ShowPlane.ZSize = s;
            ShowPlane.PlaneNormal = n;
            ShowPlane.Center = (P0.transform.localPosition + P1.transform.localPosition + P2.transform.localPosition) / 3f;
        }

        #endregion

    }
}