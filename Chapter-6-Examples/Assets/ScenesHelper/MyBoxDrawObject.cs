using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyBoxDrawObject
{
    #region Drawing Support
    private MyXZPlane[,] Faces = new MyXZPlane[3,2] {
            {new MyXZPlane(),  new MyXZPlane()},
            {new MyXZPlane(),  new MyXZPlane()},
            {new MyXZPlane(),  new MyXZPlane()}
        };
                // The 6 faces of the box: 0 is +, 1 is -
                    // 0: + and - Y/Z ==> Move in X                        
                    // 1: + and - X/Z ==> Move in Y
                    // 2: + and - X/Y ==> Move in Z

    private Vector3 BoxCenter = Vector3.zero;
    private Vector3 BoxSize = Vector3.zero;
    #endregion

    public Vector3 Center {
        get { return BoxCenter; }
        set
        {
            BoxCenter = value;
            UpdateBoxDraw();
        }
    }
    public Vector3 Size
    {
        get { return BoxSize; } 
        set {
            BoxSize = value;
            UpdateBoxDraw();
        }
    }
    public float SizeX
    {
        get { return Size.x; }
        set
        {
            Vector3 s = Size;
            s.x = value;
            Size = s;
        }
    }
    public float SizeY
    {
        get { return Size.y; }
        set
        {
            Vector3 s = Size;
            s.y = value;
            Size = s;
        }
    }
    public float SizeZ
    {
        get { return Size.z; }
        set
        {
            Vector3 s = Size;
            s.z = value;
            Size = s;
        }
    }
    public Vector3 MinPosition
    {
        get { return Center - (Size / 2.0f); }
        set
        {
            Vector3 max = Center + (Size / 2.0f);  // current max
            Size = max - value;
            Center = 0.5f * (max + value);
        }
    }
    public Vector3 MaxPosition
    {
        get { return Center + (Size / 2.0f); }
        set
        {
            Vector3 min = Center - (Size / 2.0f); // current min
            Size = value - min;
            Center = 0.5f * (min + value);
        }
    }

    // index: 0=>X, 1=>Y, 2=>Z
    protected void SetPlaneColor(int index, Color c)
    {
        Faces[index, 0].PlaneColor = c;
        Faces[index, 1].PlaneColor = c;
    }

    protected void ResetBoxColor()
    {
        SetPlaneColor(0, MyDrawObject.NoCollisionColor);
        SetPlaneColor(1, MyDrawObject.NoCollisionColor);
        SetPlaneColor(2, MyDrawObject.NoCollisionColor);
    }

    public MyBoxDrawObject()
    {
        Faces[0,0].ZRotate = -90.0f;   // Y/Z plane ==> Move in X
        Faces[0,1].ZRotate = 90.0f;
        
        // Faces[1,0] has the correct orientation   X/Z ==> Move in Y
        Faces[1,1].XRotate = 180.0f;  // 
        
        Faces[2,0].XRotate = 90.0f;  // X/Y plane ==> Move in Z
        Faces[2,1].XRotate = -90.0f;
        ResetBoxColor();

        Center = Vector3.zero;
        Size = Vector3.one;

        UpdateBoxDraw();
    }

    private void UpdateBoxDraw()
    {
        Vector3 halfSize = Size / 2.0f;
        Vector3 useSize = Size / 10f; // default plane size is 10

        // Faces[0] on Y/Z plane
        //      Position in X <- Size.X
        //         Scale in X <- Size.Y 
        //         Scale in Z <- Size.Z 
        Faces[0, 0].Center = Center + new Vector3(halfSize.x, 0, 0);
        Faces[0, 1].Center = Center - new Vector3(halfSize.x, 0, 0);
        Faces[0, 0].XSize = useSize.y;
        Faces[0, 0].ZSize = useSize.z;
        Faces[0, 1].XSize = Faces[0,0].XSize;
        Faces[0, 1].ZSize = Faces[0,0].ZSize;

        // Faces[1] on X/Z plane
        //         Scale in X <-- Size.X 
        //      Position in Y <-- Size.Y
        //         Scale in Z <-- Size.Z  
        Faces[1, 0].Center = Center + new Vector3(0, halfSize.y, 0);
        Faces[1, 1].Center = Center - new Vector3(0, halfSize.y, 0);
        Faces[1, 0].XSize = useSize.x;
        Faces[1, 0].ZSize = useSize.z;
        Faces[1, 1].XSize = Faces[1, 0].XSize;
        Faces[1, 1].ZSize = Faces[1, 0].ZSize;

        // ZFaces on X/Y plane
        //         Scale in X <-- Size.X
        //         Scale in Z <-- Size.Y
        //      Position in Z <-- Size.Z
        Faces[2, 0].Center = Center + new Vector3(0, 0, halfSize.z);
        Faces[2, 1].Center = Center - new Vector3(0, 0, halfSize.z);
        Faces[2, 0].XSize = useSize.x;
        Faces[2, 0].ZSize = useSize.y;
        Faces[2, 1].XSize = Faces[2, 0].XSize;
        Faces[2, 1].ZSize = Faces[2, 0].ZSize;
    }

    public bool DrawBox { 
        get { return Faces[0, 0].DrawPlane; }  // all 6 faces are the same!
        set
        {
            foreach (MyXZPlane d in Faces)
                d.DrawPlane = value;
        }
    }
}