using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EX_5_3_MyScript : MonoBehaviour
{
    // Positions: to deine the interval, the test, and projected 
    public GameObject P0 = null;   // Position P0
    public GameObject P1 = null;   // Position P1
    public GameObject Pt = null;   // Position Pt: test position
    public GameObject Pon = null;  // Position Pon: position on the interval-line

    #region For visualizing the vectors
    private MyVector ShowV1;    // V1
    private MyLineSegment ShowLine; // Defined by P0P1
    private MyLineSegment ShowPv, ShowPa;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        Debug.Assert(P0 != null);   // Verify proper setting in the editor
        Debug.Assert(P1 != null);
        Debug.Assert(Pt != null);
        Debug.Assert(Pon != null);

        #region For visualizing the vectors
        // To support visualizing the vectors
        ShowV1 = new MyVector {
            VectorColor = Color.green
        };
        ShowLine = new MyLineSegment
        {
            VectorColor = MyDrawObject.NoCollisionColor,
            LineWidth = 0.6f
        };
        ShowPv = new MyLineSegment
        {
            VectorColor = Color.black,
            LineWidth = 0.02f
        };
        ShowPa = new MyLineSegment
        {
            VectorColor = Color.black,
            LineWidth = 0.02f
        };
        Pt.GetComponent<Renderer>().material.color = Color.black;
        Pon.GetComponent<Renderer>().material.color = Color.black;
        #endregion

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 v1 = P1.transform.localPosition - P0.transform.localPosition;

        if (v1.magnitude > float.Epsilon)
        {
            Vector3 vt = Pt.transform.localPosition - P0.transform.localPosition;
            Vector3 v1n = v1.normalized;
            float d = Vector3.Dot(vt, v1n);
            Pon.transform.localPosition = P0.transform.localPosition + d * v1.normalized;

            if ((d >= 0) && (d <= v1.magnitude))
                Debug.Log("V1.mag=" + v1.magnitude + "  Projected Length=" + d + "  ==> Inside!");
            else
                Debug.Log("V1.mag=" + v1.magnitude + "  Projected Length=" + d + "  ==> Outside!");
        }

        #region  For visualizing the vectors

        ShowLine.VectorFromTo(P0.transform.localPosition, P1.transform.localPosition);
        bool visible = v1.magnitude > float.Epsilon;
        ShowV1.DrawVector = visible;
        ShowPv.DrawVector = visible;
        ShowPa.DrawVector = visible;

        if (visible)
        {
            Vector3 v1n = v1.normalized;
            Vector3 vt = Pt.transform.localPosition - P0.transform.localPosition;
            float d = Vector3.Dot(vt, v1n);
            if (d >= 0 && d <= v1.magnitude)
                ShowLine.VectorColor = MyDrawObject.CollisionColor;
            else
                ShowLine.VectorColor = MyDrawObject.NoCollisionColor;

            ShowPv.VectorFromTo(P0.transform.localPosition, Pt.transform.localPosition);
            ShowPa.VectorFromTo(Pt.transform.localPosition, Pon.transform.localPosition);

            float after = 0.45f;
            float before = 0.15f;
            Vector3 pv0 = P0.transform.localPosition - before * v1; ;
            Vector3 pv1 = P1.transform.localPosition + after * v1;

            if (d > (((1f + after) * v1.magnitude) - 1f))
                pv1 = Pon.transform.localPosition + v1n;

            if (d < ((-before * v1.magnitude) + 1f))
                pv0 = Pon.transform.localPosition - v1n;

            ShowV1.VectorFromTo(pv0, pv1);
        }
        #endregion

    }
}
