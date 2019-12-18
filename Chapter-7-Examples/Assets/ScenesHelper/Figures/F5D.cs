using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class F5D : MonoBehaviour
{
    #region For visualizing the vectors
    private MyVector ShowV1;    // V1
    private MyVector[] ShowV2;  // 0 to 5

    #endregion

    // Three positions to define two vectors: P0->P1, and P0->P2
    public GameObject P0 = null;   // Position P0
    public GameObject P1 = null;   // Position P1
    

    // Start is called before the first frame update
    void Start()
    {
        Debug.Assert(P0 != null);   // Verify proper setting in the editor
        Debug.Assert(P1 != null);   
    

        #region For visualizing the vectors
        // To support visualizing the vectors
        ShowV1 = new MyVector {
            VectorColor = Color.cyan
        };
        ShowV2 = new MyVector[5];
        for (int i =0; i<ShowV2.Length; i++)
        {
            ShowV2[i] = new MyVector
            {
                VectorColor = Color.magenta
            };
        }
        #endregion
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 v1 = P1.transform.localPosition - P0.transform.localPosition;
        
        

        #region  For visualizing the vectors
        ShowV1.VectorFromTo(P0.transform.localPosition, P1.transform.localPosition);
        float r = 0.8f * v1.magnitude;
        Vector3 p0 = P0.transform.localPosition;

        float delta = 45;
        for (int i=0; i<ShowV2.Length; i++)
        {
            float d = (i+1) * delta * Mathf.Deg2Rad;
            float x = r * Mathf.Cos(d);
            float y = r * Mathf.Sin(d);
            Vector3 t = new Vector3(x, y, 0);
            ShowV2[i].VectorFromTo(p0, t);
        }
        #endregion

    }
}
