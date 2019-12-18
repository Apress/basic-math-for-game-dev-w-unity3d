using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyBoxBound : MyBoxDrawObject
{
    public MyIntervalBoundInX XInterval = new MyIntervalBoundInX();
    public MyIntervalBoundInY YInterval = new MyIntervalBoundInY();
    public MyIntervalBoundInZ ZInterval = new MyIntervalBoundInZ();
    #region private utility
    private void UpdateIntervals()
    {
        XInterval.PositionToDraw = Center;
        YInterval.PositionToDraw = Center;
        ZInterval.PositionToDraw = Center;

        Vector3 max = MaxPosition;
        Vector3 min = MinPosition;
        XInterval.MinValue = min.x;
        XInterval.MaxValue = max.x;
        YInterval.MinValue = min.y;
        YInterval.MaxValue = max.y;
        ZInterval.MinValue = min.z;
        ZInterval.MaxValue = max.z;
    }
    #endregion

    /// <summary>
    /// Position: (XInterval.MinValue, YInterval.MinValue, ZInterval.MinValue)
    /// </summary>
    public new Vector3 MinPosition
    {
        get { return base.MinPosition; }
        set { base.MinPosition = value; UpdateIntervals(); }
    }

    /// <summary>
    /// Position: (XInterval.MaxValue, YInterval.MaxValue, ZInterval.MaxValue)
    /// </summary>
    public new Vector3 MaxPosition
    {
        get { return base.MaxPosition; }
        set { base.MaxPosition = value; UpdateIntervals(); }
    }

    /// <summary>
    /// Center position = 0.5 * (MinPosition + MaxPosition)
    /// </summary>
    public new Vector3 Center
    {
        get { return base.Center; }
        set { base.Center = value; UpdateIntervals(); }
    }

    /// <summary>
    /// Size = MaxPosition - MinPosition
    /// </summary>
    public new Vector3 Size
    {
        get { return base.Size; }
        set { base.Size = value; UpdateIntervals(); }
    }

    // Drawing and Color Support
    public bool DrawIntervals {
        get { return XInterval.DrawInterval; }  // XYZ are always the same
        set {
            XInterval.DrawInterval = value;
            YInterval.DrawInterval = value;
            ZInterval.DrawInterval = value;
        }
    }        // Draw or Hide the intervals
    public bool DrawBoundingBox {
        get { return DrawBox; }
        set { DrawBox = value; }
    }      // Draw or Hide the box
    public new void ResetBoxColor()
    {
        base.ResetBoxColor();
    }   // Reset box color to default (transparent white)
    public void SetBoxColor(Color c)     // Sets the color for the box
    {
        SetPlaneColor(0, c);
        SetPlaneColor(1, c);
        SetPlaneColor(2, c);
    }  

    /// <summary>
    /// Return the status of if point is inside the box
    /// </summary>
    public bool PointInBox(Vector3 point)
    {
        return
            XInterval.ValueInInterval(point.x)   // in x interval
              &&                                 //  and 
            YInterval.ValueInInterval(point.y)   // in y interval
              &&                                 //  and 
            ZInterval.ValueInInterval(point.z);  // in z interval    
    }

    /// <summary>
    /// Return the status of two boxes intersect
    /// </summary>
    public bool BoxesIntersect(MyBoxBound otherBound)
    {
        return
            XInterval.IntervalsIntersect(otherBound.XInterval)   // x intervals intersect
              &&                                                 //  and 
            YInterval.IntervalsIntersect(otherBound.YInterval)   // y intervals intersect
              &&                                                 //  and 
            ZInterval.IntervalsIntersect(otherBound.ZInterval);  // z intervals intersect
    }
}
