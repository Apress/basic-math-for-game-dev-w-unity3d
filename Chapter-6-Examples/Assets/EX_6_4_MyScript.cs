using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EX_6_4_MyScript : MonoBehaviour
{
    public bool ShowAxisFrame = true;
    public bool ShowProjections = true;

    // Plane Equation: P dot Vn = D
    public Vector3 Vn = Vector3.up;
    public float D = 2f;

    public GameObject Pn = null;
    public GameObject Pt = null;  // The point to be projected onto the plane
    public GameObject Pl = null;  // Projection of Pt on Vn
    public GameObject Pon = null; // Projeciton of Pt on the plane
        
    #region For visualizing the vectors
    private MyVector ShowNormal, ShowPt;    // 
    private MyXZPlane ShowPlane; // Plane where XZ lies
    private MyLineSegment ShowPtOnPlane, ShowPtOnN;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        Debug.Assert(Pn != null);   // Verify proper setting in the editor
        Debug.Assert(Pt != null);
        Debug.Assert(Pl != null);
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
        ShowPtOnPlane = new MyLineSegment
        {
            VectorColor = Color.black,
            LineWidth = 0.05f
        };
        ShowPt = new MyVector
        {
            VectorColor = Color.red
        };
        ShowPtOnN = new MyLineSegment
        {
            VectorColor = Color.green,
            LineWidth = 0.05f
        };
        #endregion 
    }

    // Update is called once per frame
    void Update()
    {
        Vn.Normalize();
        Pn.transform.localPosition = D * this.Vn;
        bool inFront = (Vector3.Dot(Pt.transform.localPosition, Vn) > D); // Pt infront of the plane

        Pon.SetActive(inFront);
        Pl.SetActive(inFront);
        float d = 0f;
        if (inFront)
        {
            d = Vector3.Dot(Pt.transform.localPosition, Vn);
            Pl.transform.localPosition = d * Vn;
            Pon.transform.localPosition = Pt.transform.localPosition - (d - D) * Vn;
        }

        #region  For visualizing the vectors

        AxisFrame.ShowAxisFrame = ShowAxisFrame;
        ShowPtOnPlane.DrawVector = ShowProjections;
        ShowPtOnN.DrawVector = ShowProjections;
        ShowPt.DrawVector = ShowProjections;

        ShowNormal.VectorAt = Vector3.zero;
        ShowNormal.Direction = Vn;

        if (!inFront)
        {
            d = Vector3.Dot(Pt.transform.localPosition, Vn);
            Pon.transform.localPosition = Pt.transform.localPosition - (d - D) * Vn;
            Pl.transform.localPosition = d * Vn;
        }

        float offset = 1.5f;
        float Dsize = Mathf.Abs(D) + offset;
        Vector3 from = Vector3.zero;

        if (D < 0)
        {
            from = D * Vn;
            Dsize = Mathf.Abs(D) + offset;
        }

        float useD = Mathf.Max(d, Vector3.Dot(Vn, (Pt.transform.localPosition - Pn.transform.localPosition)));
        float useSize = Dsize;
        // now consider d
        if ((useD + offset) > Dsize)
        {
            useSize = useD + offset;
        }
        else if (useD < 0)
        {
            Vector3 toFrom = Pl.transform.localPosition - from;
            if (Vector3.Dot(toFrom, Vn) < 0f)
            {
                from = Pl.transform.localPosition;
                useSize = Dsize + Pl.transform.localPosition.magnitude;
            }
        }

        ShowNormal.VectorAt = from;
        ShowNormal.Magnitude = useSize;

        float s = 2f;
        Vector3 ptTon = d * Vn - Pt.transform.localPosition;
        ShowPtOnN.Direction = ptTon;
        ShowPtOnN.Magnitude = ptTon.magnitude;
        ShowPtOnN.VectorAt = Pt.transform.localPosition;

        ShowPt.VectorFromTo(Vector3.zero, Pt.transform.localPosition);
        Vector3 von = Pon.transform.localPosition - Pn.transform.localPosition;
        s = von.magnitude * 1.2f;
        if (s < 2f)
            s = 2f;

        ShowPtOnPlane.VectorFromTo(Pon.transform.localPosition, Pt.transform.localPosition);
        Pon.transform.localRotation = Quaternion.FromToRotation(Vector3.up, Vn);

        ShowPlane.PlaneNormal= -Vn;

        ShowPlane.Center = Pn.transform.localPosition;
        ShowPlane.XSize = ShowPlane.ZSize = s;
        #endregion

    }
}