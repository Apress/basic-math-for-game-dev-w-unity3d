using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyXZPlane : MyDrawObject
{
    // constructor
    public MyXZPlane() : base("SemiTransparentXZPlane") { }
    
    // Transform: Position
    public Vector3 Center
    {
        get { return MyPosition; }
        set { MyPosition = value; }
    }

    // Transform: Size
    public float XSize
    {
        get { return MyXSize; }
        set { MyXSize = value; }
    }
    public float YSize
    {
        get { return MyYSize; }
        set { MyYSize = value; }
    }
    public float ZSize
    {
        get { return MyZSize; }
        set { MyZSize = value; }
    }

    // Transform Rotate
    public float XRotate
    {
        get { return MyXRotate; }
        set { MyXRotate = value; }
    }
    public float YRotate
    {
        get { return MyYRotate; }
        set { MyYRotate = value; }
    }
    public float ZRotate
    {
        get { return MyZRotate; }
        set { MyZRotate = value; }
    }

    // Drawing Support
    public bool DrawPlane
    {
        get { return DrawMyObject; }
        set { DrawMyObject = value;  }
    } // Show/Hide the plane
    public Color PlaneColor
    {
        set { SetMyColor(value);  }
    } // Set Color to draw
}
