using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMyVector : MonoBehaviour
{
    public MyVector aVector = null;
    public MyVector sVector = null;
    public MyVector cutVector = null;
    public GameObject Src, Dst = null;
    public GameObject aPos = null;
    public float size = 1.0f;

    public bool ShowS = true;
    public bool ShowA = true;
    public bool ShowC = true;

    // Start is called before the first frame update
    void Start()
    {
        aVector = new MyVector();
        aVector.VectorColor = Color.red;
        sVector = new MyVector();
        sVector.VectorColor = Color.green;
        cutVector = new MyVector();
    }

    // Update is called once per frame
    void Update()
    {
        aVector.DrawVector = ShowA;
        sVector.DrawVector = ShowS;
        cutVector.DrawVector = ShowC;

        Vector3 d = Dst.transform.localPosition - Src.transform.localPosition;
        aVector.Direction = d;
        aVector.VectorAt = aPos.transform.localPosition;
        aVector.Magnitude = size;

        sVector.VectorAtDirLength(aVector.VectorAt + Vector3.right, d, aVector.Magnitude);
        cutVector.VectorFromTo(Src.transform.localPosition, Dst.transform.localPosition);
    }
}