using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyVector 
{
    #region private functionality for drawing support
    private GameObject TheAxis = null;
    private GameObject ThePointer = null;
    private GameObject TheVector = null;
    private GameObject NegativeNode = null;
    private float VectorLen = 1.0f;
    private static readonly float kVectorSizeRef = 1.0f;   // Any larger, DO NOT scale Pointer
    private static readonly float kPointerBaseHeight = 0.5f;
    private static readonly Vector3 kPointerBaseScale = new Vector3(0.16f, 0.16f, 0.6f);  // XY circle, Z-cone height
    private static readonly float kAxisHeightScale = 0.5f;
    private static readonly Vector3 kAxisBaseScale = new Vector3(0.12f, 0f, 0.12f);
    private static readonly Vector3 kNegate = new Vector3(1f, -1f, 1f);

    private void UpdateForDraw()
    {
        float scaleFactor = 1.0f;
        float mag = Mathf.Abs(Magnitude);
        if (mag < kVectorSizeRef)
        {
            scaleFactor = mag;
        }
        float pointerHeight = kPointerBaseHeight * scaleFactor;

        // Axis
        TheAxis.transform.localScale = ((mag - pointerHeight) * kAxisHeightScale * Vector3.up) + (scaleFactor * kAxisBaseScale);
        TheAxis.transform.localPosition = Vector3.up * TheAxis.transform.localScale.y;

        // Pointer
        ThePointer.transform.localPosition = Vector3.up * mag;
        ThePointer.transform.localScale = kPointerBaseScale * scaleFactor;

        // sign
        if (Magnitude >= 0)
            NegativeNode.transform.localScale = Vector3.one;
        else
            NegativeNode.transform.localScale = kNegate;

        if (DrawVector && DrawVectorComponents)
        {
            Vector3 d = Direction * Magnitude;
            Vector3 dx = new Vector3(d.x, 0, 0);
            Vector3 dy = new Vector3(0, d.y, 0);
            Vector3 dz = new Vector3(0, 0, d.z);
            Vector3 at = VectorAt;
            Debug.DrawLine(at, at + dx, Color.red);
            Debug.DrawLine(at + dx, at + dx + dy, Color.green);
            Debug.DrawLine(at + dx + dy, at + dx + dy + dz, Color.blue);
        }
    }
    #endregion

    public MyVector()
    {
        #region for drawing support
        ThePointer = MonoBehaviour.Instantiate(Resources.Load("WhitePointer")) as GameObject;
        TheAxis = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        TheAxis.GetComponent<Renderer>().material.color = Color.white;
        TheVector = new GameObject();
        TheVector.gameObject.name = "The Vector";
        TheVector.transform.parent = GameObject.Find("zIgnoreThisObject").transform;
        NegativeNode = new GameObject();  // easiest, not eligent.
        NegativeNode.gameObject.name = "Navigate Node";
        NegativeNode.transform.parent = TheVector.transform;
        ThePointer.transform.parent = NegativeNode.transform;
        TheAxis.transform.parent = NegativeNode.transform;
        VectorAt = Vector3.zero;
        DrawVectorComponents = true;
        UpdateForDraw();
        #endregion
    }         // Constructor

    public float Magnitude
    {
        get { return VectorLen; }
        set { VectorLen = value; UpdateForDraw(); }
    }    // Size of the vector
    public Vector3 Direction
    {
        get { return TheVector.transform.localRotation * Vector3.up; }
        set { TheVector.transform.localRotation = Quaternion.FromToRotation(Vector3.up, value); UpdateForDraw(); }
    }  // Direction of the vector
    public Vector3 VectorAt
    {
        get { return TheVector.transform.localPosition; }
        set { TheVector.transform.localPosition = value; }
    }   // The location to draw the vector

    // Drawing Support
    public bool DrawVector
    {
        get { return TheVector.activeSelf; }
        set { TheVector.SetActive(value); }
    }      // Draw or Hide the interval
    public bool DrawVectorComponents {
        get; set;
    }
    public Color VectorColor
    {
        set {
            TheAxis.GetComponent<Renderer>().material.color = value;
            ThePointer.GetComponent<Renderer>().material.color = value;
        }
    }    // Color to draw

    // A vector from src to dst
    public void VectorFromTo(Vector3 src, Vector3 dst)
    {
        Vector3 d = dst - src;
        Direction = d;
        Magnitude = d.magnitude;
        VectorAt = src;
        UpdateForDraw();
    }

    // A vector at src, with direction: dir and magnitude: len
    public void VectorAtDirLength(Vector3 pos, Vector3 dir, float len)
    {
        Direction = dir.normalized;
        Magnitude = len;
        VectorAt = pos;
    }
}
