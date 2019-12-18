using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MyIntervalBound : MyDrawObject
{
    // Constructor
    public MyIntervalBound() : base("SemiTransparentCylinder")
    {
        MinValue = 0.0f;
        MaxValue = 1.0f;
    }

    #region Private Utility Functions
    protected abstract float MyIntervalCenterValue { get; set; }

    protected float MyIntervalHalfSize
    {
        get { return MyYSize; }
        set { MyYSize = value; }
    }
    protected bool IntervalTooSmall(float v) { return v < 0.01f;  }
    protected void UpdateInterval(float min, float size)
    {
        if (IntervalTooSmall(size))
            return;  // does not allow this!
        MyIntervalHalfSize = size / 2.0f; // default cyliner height is 2
        MyIntervalCenterValue = min + MyIntervalHalfSize;
    }
    #endregion 

    public float MinValue {
        get { return MyIntervalCenterValue - MyIntervalHalfSize; }
        set {
            float size = MaxValue - value;
            UpdateInterval(value, size);
        }
    }
    public float MaxValue
    {
        get { return MyIntervalCenterValue + MyIntervalHalfSize; }
        set {
            float size = value - MinValue;
            UpdateInterval(MinValue, size);
        }
    }

    // Drawing Support
    public bool DrawInterval
    {
        get { return DrawMyObject; }
        set { DrawMyObject = value; }
    }      // Draw or Hide the interval
    public Color IntervalColor
    {
        set { SetMyColor(value); }
    }    // Color to draw
    abstract public Vector3 PositionToDraw { get; set; } // Where to draw the interval
    
    /// <summary>
    /// Return the status of if the input v-value is in the interval
    /// </summary>
    public bool ValueInInterval(float v)
    {
        return ((v >= MinValue) && (v <= MaxValue));
    }

    /// <summary>
    /// Returns if there is an intersection with otherInterval
    /// </summary>
    public bool IntervalsIntersect(MyIntervalBound otherInterval)
    {
        return (MinValue <= otherInterval.MaxValue && MaxValue >= otherInterval.MinValue);
    }

}

public class MyIntervalBoundInX : MyIntervalBound
{
    // Constructor
    public MyIntervalBoundInX() : base()
    {
        MyZRotate = -90;
        SetMyColor(MyDrawObject.XAxisColor);
    }

    protected override float MyIntervalCenterValue
    {
        get { return MyPosition.x; }
        set
        {
            Vector3 currentPos = MyPosition;
            currentPos.x = value;
            MyPosition = currentPos;
        }
    }

    public override Vector3 PositionToDraw
    {
        get { return MyPosition; }
        set
        {
            Vector3 p = MyPosition;
            p.y = value.y;
            p.z = value.z;
            MyPosition = p;
        }
    }       
}

public class MyIntervalBoundInY : MyIntervalBound
{
    public MyIntervalBoundInY() : base()
    {  // Constructor
        SetMyColor(MyDrawObject.YAxisColor);
    }

    protected override float MyIntervalCenterValue
    {
        get { return MyPosition.y; }
        set
        {
            Vector3 currentPos = MyPosition;
            currentPos.y = value;
            MyPosition = currentPos;
        }
    }
    public override Vector3 PositionToDraw
    {
        get { return MyPosition; }
        set
        {
            Vector3 p = MyPosition;
            p.x = value.x;
            p.z = value.z;
            MyPosition = p;
        }
    }
}

public class MyIntervalBoundInZ : MyIntervalBound
{
    // Constructor
    public MyIntervalBoundInZ() : base()
    {
        MyXRotate = -90;
        SetMyColor(MyDrawObject.ZAxisColor);
    }

    protected override float MyIntervalCenterValue
    {
        get { return MyPosition.z; }
        set
        {
            Vector3 currentPos = MyPosition;
            currentPos.z = value;
            MyPosition = currentPos;
        }
    }
    public override Vector3 PositionToDraw
    {
        get { return MyPosition; }
        set
        {
            Vector3 p = MyPosition;
            p.x = value.x;
            p.y = value.y;
            MyPosition = p;
        }
    }
}