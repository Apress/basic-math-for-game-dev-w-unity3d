using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMyLineSegment : MonoBehaviour
{
    private MyVector aVector = null;
    private MyLineSegment aLine = null;
    public GameObject Src, Dst = null;
    public float aLineWidth = 0.1f;

    private MyLineSegment LineDir = null;
    public float LineDirWidth = 0.18f;
    public GameObject aPos = null;
    public GameObject bPos = null;
    public float length = 1.0f;
    public float alpha = 0.1f;
    public float ratio = 1.2f;

    private Color lc;

    // Start is called before the first frame update
    void Start()
    {
        aLine = new MyLineSegment();
        aLine.VectorColor = Color.red;

        lc = new Color(0.8f, 0f, 0.0f, alpha);
        LineDir = new MyLineSegment();
        LineDir.VectorColor = lc;
        aVector = new MyVector();
        aVector.VectorColor = Color.black;
    }

    // Update is called once per frame
    void Update()
    {
        aLine.VectorFromTo(Src.transform.localPosition, Dst.transform.localPosition);
        aLine.LineWidth = aLineWidth;

        LineDir.VectorFromTo(aPos.transform.localPosition, bPos.transform.localPosition);
        LineDir.LineLength = length;
        LineDir.LineWidth = LineDirWidth;
        lc.a = alpha;
        LineDir.VectorColor = lc;

        aVector.VectorAt = aPos.transform.localPosition;
        aVector.Direction = LineDir.Direction;
        aVector.Magnitude = length * ratio;


    }
}