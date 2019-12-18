using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Base class for all manipulating a game object to be drawn (e.g., a collider)
public class MyDrawObject {
    // Define Color for all to share
    public static Color CollisionColor = new Color(0.9f, 0.0f, 0.0f, 0.3f);
    public static Color NoCollisionColor = new Color(0.5f, 0.5f, 0.5f, 0.3f);
    public static Color XAxisColor = new Color(0.9f, 0.2f, 0.2f, 0.6f);
    public static Color YAxisColor = new Color(0.2f, 0.9f, 0.2f, 0.6f);
    public static Color ZAxisColor = new Color(0.2f, 0.2f, 0.9f, 0.6f);
    #region private variables
    private GameObject ObjectToDraw = null;
    #endregion

    /// Constructor
    protected MyDrawObject(string prefabName)
    {
        ObjectToDraw = MonoBehaviour.Instantiate(Resources.Load(prefabName)) as GameObject;
        ObjectToDraw.transform.parent = GameObject.Find("zIgnoreThisObject").transform;
    }

    #region Set/Get Positions
    /// Center of the Sphere collider: Assign and Reference
    protected Vector3 MyPosition
    {
        get { return ObjectToDraw.transform.localPosition; }
        set { ObjectToDraw.transform.localPosition = value; }
    }
    #endregion

    #region Set/Get Sizes
    /// Size of the collider: Assign and Reference
    protected float MySize
    {   // assume x/y/z values are the same
        get { return ObjectToDraw.transform.localScale.x; }
        set { ObjectToDraw.transform.localScale = new Vector3(value, value, value); }
    }

    protected float MyXSize
    {
        get { return ObjectToDraw.transform.localScale.x; }
        set
        {
            Vector3 s = ObjectToDraw.transform.localScale;
            s.x = value;
            ObjectToDraw.transform.localScale = s;
        }
    }

    protected float MyYSize
    {
        get { return ObjectToDraw.transform.localScale.y; }
        set
        {
            Vector3 s = ObjectToDraw.transform.localScale;
            s.y = value;
            ObjectToDraw.transform.localScale = s;
        }
    }

    protected float MyZSize
    {
        get { return ObjectToDraw.transform.localScale.z; }
        set
        {
            Vector3 s = ObjectToDraw.transform.localScale;
            s.z = value;
            ObjectToDraw.transform.localScale = s;
        }
    }
    #endregion

    #region Set/Get Rotation
    protected float MyXRotate
    {
        get { return ObjectToDraw.transform.localEulerAngles.x; }
        set { ObjectToDraw.transform.localRotation = Quaternion.AngleAxis(value, Vector3.right); }
    }

    protected float MyYRotate
    {
        get { return ObjectToDraw.transform.localEulerAngles.y; }
        set { ObjectToDraw.transform.localRotation = Quaternion.AngleAxis(value, Vector3.up); }
    }

    protected float MyZRotate
    {
        get { return ObjectToDraw.transform.localEulerAngles.z; }
        set { ObjectToDraw.transform.localRotation = Quaternion.AngleAxis(value, Vector3.forward); }
    }
    #endregion

    // Drawing Support
    protected bool DrawMyObject
    {
        get { return ObjectToDraw.activeSelf; }
        set { ObjectToDraw.SetActive(value); }
    }  // To show or hide the object

    protected void SetMyColor(Color c)
    {
        ObjectToDraw.GetComponent<Renderer>().material.color = c;
    } // What color to draw
}
