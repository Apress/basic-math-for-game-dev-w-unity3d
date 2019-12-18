using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyLineSegment: MyVector 
{
    #region private functionality for drawing support
    private Vector3 LineSize = new Vector3(0.18f, 0.0f, 0.18f);

    protected override float PointerBaseHeight {  get { return 0f; } }
    protected override Vector3 AxisBaseScale {  get { return LineSize;  } }
    #endregion

    public MyLineSegment()
    {
        #region for drawing support
        DisablePointer();
        #endregion

    }         // Constructor

    public float LineLength
    {
        get { return Magnitude; }
        set { Magnitude = value; }
    }

    public float LineWidth { set { LineSize.x = LineSize.z = value; } }
}
