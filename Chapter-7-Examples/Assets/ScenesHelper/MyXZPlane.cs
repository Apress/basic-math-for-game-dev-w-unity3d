using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyXZPlane : SingleSidedXZPlane
{
    private SingleSidedXZPlane BackPlane;
    public MyXZPlane()
    {
        BackPlane = new SingleSidedXZPlane()
        {
            PlaneNormal = -this.PlaneNormal
        };
    }
    public override Vector3 Center {
        set { base.Center = value;
            BackPlane.Center = value;
        } }

    // Transform: Size
    public override float XSize
    {
        set { MyXSize = value;
            BackPlane.XSize = value;
        }
    }
    public override float YSize
    {
        set { MyYSize = value;
            BackPlane.YSize = value;
        }
    }
    public override float ZSize
    {
        set { MyZSize = value;
            BackPlane.ZSize = value;
        }
    }

    // Transform Rotate
    public override float XRotate
    {
        set { MyXRotate = value;
            BackPlane.XRotate = 180+value;
        }
    }
    public override float YRotate
    {
        set { MyYRotate = value;
            BackPlane.YRotate = value;
        }
    }
    public override float ZRotate
    {
        set { MyZRotate = value;
            BackPlane.ZRotate = value;
        }
    }

    public override Vector3 PlaneNormal
    {
        set
        {
            ObjectToDraw.transform.localRotation = Quaternion.FromToRotation(Vector3.up, value);
            BackPlane.PlaneNormal = -value;
        }
    }

    // Drawing Support
    public override bool DrawPlane
    {
        set { DrawMyObject = value;
            BackPlane.DrawPlane = value;
        }
    } // Show/Hide the plane
    public override Color PlaneColor
    {
        set { SetMyColor(value);
            BackPlane.PlaneColor = value;
        }
    } // Set Color to draw
}
