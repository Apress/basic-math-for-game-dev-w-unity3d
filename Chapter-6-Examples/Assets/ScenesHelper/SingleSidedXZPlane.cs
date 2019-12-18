using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleSidedXZPlane : MyDrawObject
{
    // constructor
    public SingleSidedXZPlane() : base("SemiTransparentXZPlane")
    {
    }

    // Transform: Position
    public virtual Vector3 Center
    {
        get { return MyPosition; }
        set { MyPosition = value; }
    }

    // Transform: Size
    public virtual float XSize
    {
        get { return MyXSize; }
        set { MyXSize = value; }
    }
    public virtual float YSize
    {
        get { return MyYSize; }
        set { MyYSize = value; }
    }
    public virtual float ZSize
    {
        get { return MyZSize; }
        set { MyZSize = value; }
    }

    // Transform Rotate
    public virtual float XRotate
    {
        get { return MyXRotate; }
        set { MyXRotate = value; }
    }
    public virtual float YRotate
    {
        get { return MyYRotate; }
        set { MyYRotate = value; }
    }
    public virtual float ZRotate
    {
        get { return MyZRotate; }
        set { MyZRotate = value; }
    }

    public virtual Vector3 PlaneNormal
    {
        get
        {
            Matrix4x4 m = Matrix4x4.Rotate(ObjectToDraw.transform.localRotation);
            return m.GetColumn(1); // Y is the front
        }
        set
        {
            Vector3 zDir = -Vector3.forward;
            float dot = Mathf.Abs(Vector3.Dot(value.normalized, zDir.normalized));
            if ((1f - dot) < float.Epsilon)
            { // alomst parallel 
                zDir = -Vector3.right;
            }
            Vector3 tmp = Vector3.Cross(value, zDir);
            zDir = Vector3.Cross(tmp, value);
            ObjectToDraw.transform.localRotation = Quaternion.LookRotation(zDir, value);
        }
    }

    // Drawing Support
    public virtual bool DrawPlane
    {
        get { return DrawMyObject; }
        set { DrawMyObject = value; }
    } // Show/Hide the plane
    public virtual Color PlaneColor
    {
        set { SetMyColor(value); }
    } // Set Color to draw
}
