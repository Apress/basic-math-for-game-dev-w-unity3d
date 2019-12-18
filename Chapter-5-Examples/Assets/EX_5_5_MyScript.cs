using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EX_5_5_MyScript : MonoBehaviour
{
    // Positions: to deine the two lines.
    public GameObject P1, P2;  // define the line V1
    public GameObject Pa, Pb;  // define the line Va
    public GameObject Pd_1;    // point on V1 closest to Va
    public GameObject Pd_a;    // point on va closest to V1

    #region For visualizing the line
    private MyLineSegment ShowV1, ShowVa, ShowVp;
    private const float kScaleFactor = 0.4f;
    #endregion

    void Start()
    {
        Debug.Assert(P1 != null);   // Verify proper setting in the editor
        Debug.Assert(P2 != null);
        Debug.Assert(Pd_1 != null);
        Debug.Assert(Pa != null);
        Debug.Assert(Pb != null);
        Debug.Assert(Pd_a != null);

        #region For visualizing the line
        // To support visualizing the line
        ShowV1 = new MyLineSegment
        {
            VectorColor = Color.red,
            LineWidth = 0.1f
        };
        ShowVa = new MyLineSegment
        {
            VectorColor = Color.blue,
            LineWidth = 0.1f
        };
        ShowVp = new MyLineSegment
        {
            VectorColor = Color.black,
            LineWidth = 0.05f
        };
        #endregion 
    }

    void Update()
    {
        Vector3 v1 = (P2.transform.localPosition - P1.transform.localPosition);
        Vector3 va = (Pb.transform.localPosition - Pa.transform.localPosition);

        if ((v1.magnitude < float.Epsilon) || (va.magnitude < float.Epsilon))
            return;  // will only work with well defined line segments

        Vector3 va1 = P1.transform.localPosition - Pa.transform.localPosition;
        Vector3 v1n = v1.normalized;
        Vector3 van = va.normalized;
        float d = Vector3.Dot(v1n, van);

        bool almostParallel = (1f - Mathf.Abs(d) < float.Epsilon);

        float d1 = 0f, da = 0f;

        if (!almostParallel)  // two lines are not parallel
        {
            float dot1A1 = Vector3.Dot(v1n, va1);
            float dotAA1 = Vector3.Dot(van, va1);
            
            d1 = (-dot1A1 + d * dotAA1) / (1 - (d * d));
            da = (dotAA1 - d * dot1A1) / (1 - (d * d));

            d1 /= v1.magnitude;
            da /= va.magnitude;

            Pd_1.transform.localPosition = P1.transform.localPosition + d1 * v1;
            Pd_a.transform.localPosition = Pa.transform.localPosition + da * va;
            float dist = (Pd_1.transform.localPosition - Pd_a.transform.localPosition).magnitude;
            Debug.Log("d1=" + d1 + " da=" + da + " Distance=" +  dist);
        } else
        {
            Debug.Log("Line segments are parallel, special case not handled");
        }

        #region  For visualizing the line
        ShowV1.VectorFromTo(P1.transform.localPosition, P2.transform.localPosition);
        ShowVa.VectorFromTo(Pa.transform.localPosition, Pb.transform.localPosition);
        ShowVp.DrawVector = (!almostParallel);

        Pd_1.SetActive(!almostParallel);
        Pd_a.SetActive(!almostParallel);

        if (!almostParallel)  // two lines are not parallel
        {
            bool d0Out = (d1 < 0f) || (d1 > 1.0f);
            bool daOut = (da < 0f) || (da > 1.0f);
            Color c = Pd_1.GetComponent<Renderer>().material.color;
            
            c.a = d0Out ? 1.0f : 0.25f;
            Pd_1.GetComponent<Renderer>().material.color = c;
            c.a = daOut ? 1.0f : 0.25f;
            Pd_a.GetComponent<Renderer>().material.color = c;
            
            // ShowVp.VectorColor = c;
            ShowVp.VectorFromTo(Pd_1.transform.localPosition, Pd_a.transform.localPosition);
            
            float s = ShowVp.Magnitude * kScaleFactor;
            Pd_1.transform.localScale = new Vector3(s, s, s);
            Pd_a.transform.localScale = new Vector3(s, s, s);
        }
        #endregion
    }
}
