using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EX_6_3_MyScript : MonoBehaviour
{
    #region Identical to EX_6_2
    // Defines two vectors: V1 = P1 - P0, V2 = P2 - P0
    public GameObject P0 = null;   // The three positions
    public GameObject P1 = null;   // 
    public GameObject P2 = null;   // 

    // Plane equation:   P dot vn = D
    public GameObject Ds;         // To show the D-value 
    public GameObject Pn;          // Where Vn crosses the plane

    public bool ShowPointOnPlane = true;  // Hide or Show Pt
    public GameObject Pt;           // Point to adjust
    public GameObject Pon;          // Point in the Plane, in the Pt direction
    #endregion
    public GameObject P2p;  // The perpendicular version of P2

    #region For visualizing the vectors
    private MyVector ShowV1, ShowV2, ShowV3;
    private MyVector ShowVn;
    private MyVector ShowNormal;      // Vn
    private MyXZPlane ShowPlane;      // Plane where XZ lies
    private MyLineSegment ShowPtLine; // Line to Pt
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        #region Identical to EX_6_2
        Debug.Assert(P0 != null);   // Verify proper setting in the editor
        Debug.Assert(P1 != null);   
        Debug.Assert(P2 != null);
        Debug.Assert(Ds != null);
        Debug.Assert(Pn != null);
        Debug.Assert(Pt != null);
        Debug.Assert(Pon != null);
        #endregion 

        Debug.Assert(P2p != null);

        #region For visualizing the vectors
        // To support visualizing the vectors
        ShowV1 = new MyVector
        {
            VectorColor = Color.cyan
        };
        ShowV2 = new MyVector
        {
            VectorColor = Color.magenta
        };
        ShowV3 = new MyVector
        {
            VectorColor = Color.green
        };
        ShowNormal = new MyVector {
            VectorColor = Color.white
        };
        ShowVn = new MyVector
        {
            VectorColor = Color.black
        };
        ShowPlane = new MyXZPlane
        {
            PlaneColor = new Color(0.8f, 0.3f, 0.3f, 1.0f),
            XSize = 0.5f,
            YSize = 0.5f,
            ZSize = 0.5f
        };
        ShowPtLine = new MyLineSegment
        {
            VectorColor = Color.black,
            LineWidth = 0.05f
        };
        #endregion 
    }

    // Update is called once per frame
    void Update()
    {
        #region Identical to EX_6_2
        // Computes V1 and V2
        Vector3 v1 = P1.transform.localPosition - P0.transform.localPosition;
        Vector3 v2 = P2.transform.localPosition - P0.transform.localPosition;
        if ((v1.magnitude < float.Epsilon) || (v2.magnitude < float.Epsilon))
            return;

        // Plane equation parameters
        Vector3 vn = Vector3.Cross(v1, v2);
        vn.Normalize();  // keep this vector normalized
        float D = Vector3.Dot(vn, P0.transform.localPosition);
        
        // Showing the plane equation is consistent
        Pn.transform.localPosition = D * vn;
        Ds.transform.localScale = new Vector3(D * 2f, D * 2f, D * 2f); // sphere expects diameter

        // Set up for displaying Pt and Pon
        Pt.SetActive(ShowPointOnPlane);
        Pon.SetActive(ShowPointOnPlane);
        float t = 0;
        bool almostParallel = false;
        if (ShowPointOnPlane)
        {
            float d = Vector3.Dot(vn, Pt.transform.localPosition);  // distance
            almostParallel = (Mathf.Abs(d) < float.Epsilon);
            Pon.SetActive(!almostParallel);
            if (!almostParallel)
            {
                t = D / d;
                Pon.transform.localPosition = t * Pt.transform.localPosition;
            } 
        }
        #endregion

        float l1 = v1.magnitude;
        float l2 = v2.magnitude;
        Vector3 v2p = l2 * Vector3.Cross(vn, v1).normalized;
        P2p.transform.localPosition = P0.transform.localPosition + v2p;

        bool inside = false;
        if (!almostParallel)
        {
            Vector3 von = Pon.transform.localPosition - P0.transform.localPosition;
            float d1 = Vector3.Dot(von, v1.normalized);
            float d2 = Vector3.Dot(von, v2p.normalized);

            inside = ((d1 >= 0) && (d1 <= l1)) && ((d2 >= 0) && (d2 <= l2));
            if (inside)
                Debug.Log("Inside: Pon is inside of the region defined by V1 and V2");
            else
                Debug.Log("Outside: Pon is outside of the region defined by V1 and V2");
        }
        #region  For visualizing the vectors

        ShowV1.VectorFromTo(P0.transform.localPosition, P1.transform.localPosition);
        ShowV2.VectorFromTo(P0.transform.localPosition, P2.transform.localPosition);
        ShowV3.VectorFromTo(P0.transform.localPosition, P2p.transform.localPosition);

        ShowVn.VectorAt = P0.transform.localPosition;
        ShowVn.Direction = vn;
        ShowVn.Magnitude = 2f;

        ShowNormal.VectorAt = Vector3.zero;
        ShowNormal.Magnitude = Mathf.Abs(D)+2f;
        ShowNormal.Direction = vn;

        ShowPlane.PlaneNormal = -vn;
        Vector3 at = P0.transform.localPosition + P1.transform.localPosition + P2.transform.localPosition + Pn.transform.localPosition;
        int c = 4;

        float scale = 1.0f;
        ShowPtLine.DrawVector = ShowPointOnPlane;
        float da = v1.magnitude * scale;
        float db = v2.magnitude * scale;
        float du = Mathf.Max(da, db);

        if (ShowPointOnPlane && (!almostParallel))
        {
            Pon.GetComponent<Renderer>().material.color = Color.white;
            float don = (Pon.transform.localPosition - P0.transform.localPosition).magnitude * scale;
            at += Pon.transform.localPosition;
            c++;
            du = Mathf.Max(du, don);

            // Now the line
            ShowPtLine.VectorColor = Color.black;
            if (Vector3.Dot(Pon.transform.localPosition, Pt.transform.localPosition) < 0)
            {
                ShowPtLine.VectorColor = Color.red;
                ShowPtLine.VectorFromTo(Pt.transform.localPosition, Pon.transform.localPosition);
            }
            else
            {
                if (Pon.transform.localPosition.magnitude > Pt.transform.localPosition.magnitude)
                    ShowPtLine.VectorFromTo(Vector3.zero, Pon.transform.localPosition);
                else
                    ShowPtLine.VectorFromTo(Vector3.zero, Pt.transform.localPosition);
            }

            if (!inside)
                Pon.GetComponent<Renderer>().material.color = Color.red;
        }

        if (du < 0.5f)
            du = 0.5f;

        ShowPlane.XSize = ShowPlane.ZSize = du;
        ShowPlane.Center = at / c;
        #endregion
    }
}