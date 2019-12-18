using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EX_6_5_MyScript : MonoBehaviour
{
    public bool ShowAxisFrame = true;

    // Plane Equation: P dot Vn = D
    public Vector3 Vn = Vector3.up;
    public float D = 2f;
    public GameObject Pn = null;  // The point where plane normal passes

    public GameObject P0 = null, P1 = null;  // The line segment
    public GameObject Pon = null;  // The intersection position
    
    #region For visualizing the vectors
    private MyVector ShowNormal;    // 
    private MyXZPlane ShowPlane; // Plane where XZ lies
    private MyLineSegment ShowLine;
    private MyLineSegment ShowRestOfLine;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        Debug.Assert(Pn != null);   // Verify proper setting in the editor
        Debug.Assert(P0 != null);
        Debug.Assert(P1 != null);
        Debug.Assert(Pon != null);

        #region For visualizing the vectors
        // To support visualizing the vectors
        ShowNormal = new MyVector {
            VectorColor = Color.white
        };
        ShowPlane = new MyXZPlane
        {
            PlaneColor = new Color(0.8f, 0.3f, 0.3f, 1.0f),
            XSize = 0.5f,
            YSize = 0.5f,
            ZSize = 0.5f
        };
        ShowLine = new MyLineSegment
        {
            VectorColor = Color.black,
            LineWidth = 0.05f
        };
        ShowRestOfLine = new MyLineSegment
        {
            VectorColor = Color.red,
            LineWidth = 0.05f
        };
        #endregion 
    }

    // Update is called once per frame
    void Update()
    {
        Vn.Normalize();
        Pn.transform.localPosition = D * Vn;

        // Compute the line segment direction 
        Vector3 v1 = P1.transform.localPosition - P0.transform.localPosition;
        if (v1.magnitude < float.Epsilon)
        {
            Debug.Log("Ill defined line (magnitude of zero). Not processed");
            return;
        }

        float denom = Vector3.Dot(Vn, v1);
        bool lineNotParallelPlane = (Mathf.Abs(denom) > float.Epsilon);  // Vn is not perpendicular with V1
        float d = 0;

        Pon.SetActive(lineNotParallelPlane);
        if (lineNotParallelPlane) 
        {
            d = (D - (Vector3.Dot(Vn, P0.transform.localPosition))) / denom;
            Pon.transform.localPosition = P0.transform.localPosition + d * v1;
            Debug.Log("Interesection pt at:" + Pon + "Distant from P0 d=" + d);
        } else
        {
            Debug.Log("Line is almost parallel to the plane, no interesection!");
        }

        #region  For visualizing the vectors

        AxisFrame.ShowAxisFrame = ShowAxisFrame;

        float offset = 1.5f;
        float size = Mathf.Abs(D) + offset;
        Vector3 from = Vector3.zero;

        if (D < 0) {
            from = D * Vn;
            size = Mathf.Abs(D) + offset;
        }
        ShowNormal.VectorAt = from;
        ShowNormal.Direction = Vn;
        ShowNormal.Magnitude = size;

        ShowLine.VectorFromTo(P0.transform.localPosition, P1.transform.localPosition);
        ShowRestOfLine.DrawVector = false;
        if (lineNotParallelPlane && ((d < 0f) || (d>1f)) )
        {
            ShowRestOfLine.DrawVector = true;
            if (d < 0f)
            {
                ShowRestOfLine.VectorFromTo(Pon.transform.localPosition, P0.transform.localPosition);
            } else
            {
                ShowRestOfLine.VectorFromTo(Pon.transform.localPosition, P1.transform.localPosition);
            }
        }

        // only update when there is a proper projection
        Vector3 von = Vector3.zero;
        float s = 2f;
        if (lineNotParallelPlane)
        {
            von = Pon.transform.localPosition - Pn.transform.localPosition;
            s = von.magnitude * 1.2f;
            if (s < 2f)
                s = 2f;
        } else
        {
            Pon.transform.localPosition = Pn.transform.localPosition;
        }
        ShowPlane.PlaneNormal = -Vn;
        ShowPlane.Center = 0.5f* (Pn.transform.localPosition + Pon.transform.localPosition);
        ShowPlane.XSize = ShowPlane.ZSize = s;

        Pon.transform.localRotation = Quaternion.FromToRotation(Vector3.up, Vn);

        #endregion

    }
}