using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EX_5_5_MyScript_CompleteSolution: MonoBehaviour
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

        Vector3 vb = P1.transform.localPosition - Pa.transform.localPosition;
        Vector3 v1n = v1.normalized;
        Vector3 van = va.normalized;
        float d = Vector3.Dot(v1n, van);

        bool almostParallel = (1f - Mathf.Abs(d) < float.Epsilon);

        float d1 = 0f, da = 0f;
        bool d0Out = true;
        bool daOut = true;

        if (!almostParallel)  // two lines are not parallel
        {
            float dotAB = Vector3.Dot(van, vb);
            float dot1B = Vector3.Dot(v1n, vb);

            d1 = (-dot1B + d * dotAB) / (1 - (d * d));
            da = (dotAB - d * dot1B) / (1 - (d * d));

            d1 /= v1.magnitude;
            da /= va.magnitude;

            Pd_1.transform.localPosition = P1.transform.localPosition + d1 * v1;
            Pd_a.transform.localPosition = Pa.transform.localPosition + da * va;
            float dist = (Pd_1.transform.localPosition - Pd_a.transform.localPosition).magnitude;
            Debug.Log("d1=" + d1 + " da=" + da + " Distance=" + dist);

            d0Out = (d1 < 0f) || (d1 > 1.0f);
            daOut = (da < 0f) || (da > 1.0f);
        } 

        if (almostParallel || d0Out || daOut)
        {
            // Debug.Log("Line segments are parallel, special case not handled");
            // Compute end pt to line
            Vector3 pon1a, pon1b, pona1, pona2;
            bool inside = false;

            // distance from Line-P1P2 to points Pa, and Pb
            d1 = -1f;
            float d1a = LineToPoint(P1.transform.localPosition, P2.transform.localPosition, Pa.transform.localPosition, out pon1a, out inside);
            if (inside)
            {
                Pd_1.transform.localPosition = pon1a;
                Pd_a.transform.localPosition = Pa.transform.localPosition;
                d1 = d1a/v1.magnitude;
                da = 0f;
            }
            else
            {
                float d1b = LineToPoint(P1.transform.localPosition, P2.transform.localPosition, Pb.transform.localPosition, out pon1b, out inside);
                if (inside)
                {
                    Pd_1.transform.localPosition = pon1b;
                    Pd_a.transform.localPosition = Pb.transform.localPosition;
                    d1 = d1b/v1.magnitude;
                    da = 1f;
                }
                else
                {
                    if (d1a < d1b)
                    {
                        Pd_1.transform.localPosition = pon1a;
                        d1 = d1a/v1.magnitude;
                    }
                    else
                    {
                        Pd_1.transform.localPosition = pon1b;
                        d1 = d1b/v1.magnitude;
                    }

                    // distance from Line-PaPb to points P1 and P2
                    float da1 = LineToPoint(Pa.transform.localPosition, Pb.transform.localPosition, P1.transform.localPosition, out pona1, out inside);
                    if (inside)
                    {
                        Pd_a.transform.localPosition = pona1;
                        Pd_1.transform.localPosition = P1.transform.localPosition;
                        da = da1/va.magnitude;
                        d1 = 0f;
                    }
                    else
                    {
                        float da2 = LineToPoint(Pa.transform.localPosition, Pb.transform.localPosition, P2.transform.localPosition, out pona2, out inside);
                        if (inside)
                        {
                            Pd_a.transform.localPosition = pona2;
                            Pd_1.transform.localPosition = P2.transform.localPosition;
                            da = da2 / va.magnitude;
                            d1 = 1f;
                        }
                        else
                        {
                            if (da1 < da2)
                            {
                                da = da1 / va.magnitude;
                                Pd_a.transform.localPosition = pona1;
                            }
                            else
                            {
                                da = da2 / va.magnitude;
                                Pd_a.transform.localPosition = pona2;
                            }
                        }
                    }
                }
            }
            d0Out = (d1 < 0f) || (d1 > 1.0f);
            daOut = (da < 0f) || (da > 1.0f);
        }

        #region  For visualizing the line
        ShowV1.VectorFromTo(P1.transform.localPosition, P2.transform.localPosition);
        ShowVa.VectorFromTo(Pa.transform.localPosition, Pb.transform.localPosition);
        // ShowVp.DrawVector = (!almostParallel);
        // Pd_1.SetActive(!almostParallel);
        // Pd_a.SetActive(!almostParallel);


       
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
        #endregion
    }

    private float LineToPoint(Vector3 p0, Vector3 p1, Vector3 pt, out Vector3 pon, out bool inside)
    {
        float distant = -1; // closest distant
        Vector3 v1 = p1 - p0;
        float v1Len = v1.magnitude;
        pon = p0;
        inside = false;

        if (v1Len > float.Epsilon)
        {
            Vector3 vt = pt - p0;
            Vector3 v1n = (1f / v1Len) * v1; // <<-- what am I doing here?
            float d = Vector3.Dot(vt, v1n);
            if (d < 0)
            {
                pon = p0;
                distant = vt.magnitude;
            }
            else if (d > v1Len)
            {
                pon = p1;
                distant = (pt - p1).magnitude;
            }
            else
            {
                pon = p0 + d * v1n;
                Vector3 vc = pon - pt;
                distant = vc.magnitude;
                inside = true;
            }
        }
        return distant;
    }
}
