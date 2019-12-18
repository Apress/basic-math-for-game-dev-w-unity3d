using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EX_5_4_MyScript : MonoBehaviour
{
    // Positions: to deine the interval, the test, and projected
    public GameObject P0 = null;  // Position P0
    public GameObject P1 = null;  // Position P1
    public GameObject Pt = null;  // Position for distance computation
    public GameObject Pon = null; // closest point on line

    #region For visualizing the line
    private MyVector ShowV1;
    private MyLineSegment ShowLine, ShowVc;
    private float kScaleFactor = 0.5f;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        Debug.Assert(P0 != null);   // Verify proper setting in the editor
        Debug.Assert(P1 != null);   
        Debug.Assert(Pt != null);
        Debug.Assert(Pon != null);

        #region For visualizing the lines
        // To support visualizing the lines
        ShowLine = new MyLineSegment
        {
            VectorColor = MyDrawObject.NoCollisionColor,
            LineWidth = 0.6f
        };
        ShowVc = new MyLineSegment
        {
            VectorColor = Color.black,
            LineWidth = 0.05f
        };
        ShowV1 = new MyVector
        {
            VectorColor = Color.green
        };
        #endregion 
    }

    // Update is called once per frame
    void Update()
    {
        float distance = 0; // closest distance
        Vector3 v1 = P1.transform.localPosition - P0.transform.localPosition;
        float v1Len = v1.magnitude;

        if (v1Len > float.Epsilon)
        {
            Vector3 vt = Pt.transform.localPosition - P0.transform.localPosition;
            Vector3 v1n = (1f / v1Len) * v1; // <<-- what is going on here?
            float d = Vector3.Dot(vt, v1n);
            if (d < 0)
            {
                Pon.transform.localPosition = P0.transform.localPosition;
                distance = vt.magnitude;
            }
            else if (d > v1Len)
            {
                Pon.transform.localPosition = P1.transform.localPosition;
                distance = (Pt.transform.localPosition - P1.transform.localPosition).magnitude;
            }
            else
            {
                Pon.transform.localPosition = P0.transform.localPosition + d * v1n;
                Vector3 von = Pon.transform.localPosition - Pt.transform.localPosition;
                distance = von.magnitude;
            }
            float s = distance * kScaleFactor;
            Pon.transform.localScale = new Vector3(s, s, s);
            Debug.Log("v1Len=" + v1Len + " d=" + d + " Distance=" + distance);
        }

        #region  For visualizing the lines
        bool visible = v1Len > float.Epsilon;
        ShowVc.DrawVector = visible;
        ShowLine.DrawVector = visible;
        ShowV1.DrawVector = visible;
        if (v1Len > float.Epsilon)
        {
            Vector3 vt = Pt.transform.localPosition - P0.transform.localPosition;
            Vector3 v1n = (1f / v1Len) * v1; // <<-- what am I doing here?
            float d = Vector3.Dot(v1n, vt);

            ShowLine.VectorFromTo(P0.transform.localPosition, P1.transform.localPosition);
            ShowVc.VectorFromTo(Pt.transform.localPosition, Pon.transform.localPosition);
            float after = 0.45f;
            float before = 0.15f;
            Vector3 pv0 = P0.transform.localPosition - before * v1; ;
            Vector3 pv1 = P1.transform.localPosition + after * v1;

            ShowV1.VectorFromTo(pv0, pv1);
        }
        #endregion

    }
}
