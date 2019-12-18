using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class F5B : MonoBehaviour
{
    #region For visualizing the vectors
    private MyVector ShowV1;    // V1
    private MyVector ShowV2;    // V2
    private MyVector ShowNV2;
    private MyVector ShowV1_V2;
    private MyVector ShowV1_V2_Pos;


    private Color NegativeColor = Color.red;
    private Color PositiveColor = Color.green;
    #endregion

    // Three positions to define two vectors: P0->P1, and P0->P2
    public GameObject P0 = null;   // Position P0
    public GameObject P1 = null;   // Position P1
    public GameObject P2 = null;   // Position P2

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
        ShowNV2 = new MyVector
        {
            VectorColor = Color.red
        };
        ShowV1_V2 = new MyVector
        {
            VectorColor = Color.gray
        };
        ShowV1_V2_Pos = new MyVector
        {
            VectorColor = Color.gray
        };

        #endregion
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 v1 = P1.transform.localPosition - P0.transform.localPosition;
        Vector3 v2 = P2.transform.localPosition - P0.transform.localPosition;
        float dot = Vector3.Dot(v1, v2);
        float cosTheta = float.NaN;
        float theta = float.NaN;

        if (Mathf.Abs(dot) > float.Epsilon)  // make sure dot is properly defined
        {
            cosTheta = dot / (v1.magnitude * v2.magnitude);
            theta = Mathf.Acos(cosTheta) * Mathf.Rad2Deg;
        }
        Debug.Log("Dot result=" + dot + " cosTheta=" + cosTheta + " angle=" + theta);

        #region  For visualizing the vectors
        ShowV1.VectorFromTo(P0.transform.localPosition, P1.transform.localPosition);
        ShowV2.VectorFromTo(P0.transform.localPosition, P2.transform.localPosition);
        ShowNV2.VectorAt = P1.transform.localPosition;
        ShowNV2.Direction = -ShowV2.Direction;
        ShowNV2.Magnitude = ShowV2.Magnitude;

        Vector3 v1_v2 = v1 - v2;
        ShowV1_V2.Direction = v1_v2;
        ShowV1_V2.Magnitude = v1_v2.magnitude;
        ShowV1_V2.VectorAt = P2.transform.localPosition;

        ShowV1_V2_Pos.Direction = ShowV1_V2.Direction;
        ShowV1_V2_Pos.Magnitude = ShowV1_V2.Magnitude;
        ShowV1_V2_Pos.VectorAt = P0.transform.localPosition;
        #endregion

    }
}