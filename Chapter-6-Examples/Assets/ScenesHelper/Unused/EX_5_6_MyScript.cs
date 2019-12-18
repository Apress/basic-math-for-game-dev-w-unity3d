using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EX_5_6_MyScript : MonoBehaviour
{
    public GameObject P0, P1;     // defines the line
    public GameObject TheSphere;  // here is the sphere
    public float Diameter=0.8f;   // Diameter of the sphere
    public GameObject Pa, Pb;     // The two intersection positions


    #region For visualizing the line
    private MyLineSegment ShowTheLine;
    #endregion


    private void Start()
    {
        Debug.Assert(P0 != null);   // Verify proper setting in the editor
        Debug.Assert(P1 != null);
        Debug.Assert(TheSphere != null);
        Debug.Assert(Pa != null);
        Debug.Assert(Pb != null);

        #region For visualizing the line
        // To support visualizing the line
        ShowTheLine = new MyLineSegment
        {
            VectorColor = Color.blue,
            LineWidth = 0.08f
        };
        #endregion 
    }

    private void Update()
    {
        string details = "nothing for now";
        bool hit = false;

        TheSphere.transform.localScale = new Vector3(Diameter, Diameter, Diameter);

        Vector3 v1 = P1.transform.localPosition - P0.transform.localPosition;
        float len = v1.magnitude;
        Vector3 v1n = v1 * 1f/len;
        Vector3 vS = TheSphere.transform.localPosition - P0.transform.localPosition;
        float h = Vector3.Dot(vS, v1n);

        float r = Diameter/2f; // User specified radius

        Vector3 ph;
        float d, a;
        if (h >= len) // case A
        {
            if ((P1.transform.localPosition - TheSphere.transform.localPosition).magnitude < r)
            {
                hit = true;
                details = "Case A1";

                ph = P0.transform.localPosition + h * v1n;
                d = (TheSphere.transform.localPosition - ph).magnitude;
                a = Mathf.Sqrt(r * r - d * d);
                Pa.transform.localPosition = P0.transform.localPosition + (h - a) * v1n;
            }
            else
            {
                details = "Case A2";
                Pa.transform.localPosition = TheSphere.transform.localPosition;
            }
            Pb.transform.localPosition = Pa.transform.localPosition;
        }
        else if (h <= 0) // case B
        {
            if ((P0.transform.localPosition - TheSphere.transform.localPosition).magnitude < r)
            {
                hit = true;
                details = "Case B1";

                ph = P0.transform.localPosition + h * v1n;
                d = (TheSphere.transform.localPosition - ph).magnitude;
                a = Mathf.Sqrt(r * r - d * d) + h;
                Pa.transform.localPosition = P0.transform.localPosition + a * v1n;

            }
            else
            {
                details = "Case B2";
                Pa.transform.localPosition = TheSphere.transform.localPosition;
            }
            Pb.transform.localPosition = Pa.transform.localPosition;

        }
        else
        {
            d = Mathf.Sqrt(vS.sqrMagnitude - (h * h));
            if (d < r)
            {
                hit = true;
                details = "Case C1";

                a = Mathf.Sqrt(r * r - d * d);
                Pa.transform.localPosition = P0.transform.localPosition + (h - a) * v1n;
                Pb.transform.localPosition = P0.transform.localPosition + (h + a) * v1n;
            }
            else
            {
                details = "Case C2";
                Pa.transform.localPosition = TheSphere.transform.localPosition;
                Pb.transform.localPosition = Pa.transform.localPosition;
            }
        }
        Debug.Log(hit + ":" + details);

        #region  For visualizing the line
        ShowTheLine.VectorFromTo(P0.transform.localPosition, P1.transform.localPosition);
        #endregion
    }
}
