using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class F5G : MonoBehaviour
{
    #region For visualizing the vectors
    private MyVector ShowV1;    // V1
    private MyLineSegment ShowLine; // Defined by P0P1
    #endregion

    // Three positions to define two vectors: P0->P1, and P0->P2
    public GameObject P0 = null;   // Position P0
    public GameObject P1 = null;   // Position P1
    public GameObject Pa = null;   // Position Pa
    public GameObject Pb = null;   // Position Pb


    // Start is called before the first frame update
    void Start()
    {
        Debug.Assert(P0 != null);   // Verify proper setting in the editor
        Debug.Assert(P1 != null);   
    

        #region For visualizing the vectors
        // To support visualizing the vectors
        ShowV1 = new MyVector {
            VectorColor = Color.green
        };
        Pa.GetComponent<Renderer>().material.color = Color.black;
        Pb.GetComponent<Renderer>().material.color = Color.black;
        ShowLine = new MyLineSegment
        {
            VectorColor = MyDrawObject.NoCollisionColor,
            LineWidth = 0.6f
        };
        #endregion
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 v1 = P1.transform.localPosition - P0.transform.localPosition;

        #region  For visualizing the vectors
        Vector3 pv0 = P0.transform.localPosition - 0.5f * v1;
        Vector3 pv1 = P1.transform.localPosition + 0.5f * v1;
        ShowV1.VectorFromTo(pv0, pv1);

        Pa.transform.localPosition = P0.transform.localPosition - 0.18f * v1;
        Pb.transform.localPosition = P0.transform.localPosition + 1.2f * v1;

        ShowLine.VectorFromTo(P0.transform.localPosition, P1.transform.localPosition);
        #endregion

    }
}
