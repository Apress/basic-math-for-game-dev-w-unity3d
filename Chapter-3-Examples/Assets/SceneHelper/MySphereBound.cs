using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Visualizes sphere bound using the Sphere GameObject
public class MySphereBound {

    #region Variables: for drawing in Unity
    public static Color CollisionColor = new Color(0.9f, 0.0f, 0.0f, 0.3f);
    public static Color NoCollisionColor = new Color(0.5f, 0.5f, 0.5f, 0.3f);
    private GameObject BoundObject = null;
    #endregion
    
    /// Constructor
    public MySphereBound()
    {
        BoundObject = MonoBehaviour.Instantiate(Resources.Load("SemiTransparentSphere")) as GameObject;
        BoundObject.transform.parent = GameObject.Find("zIgnoreThisObject").transform;
        Center = Vector3.zero;
        Radius = 2.0f;
    }

    public Vector3 Center
    {
        get { return BoundObject.transform.localPosition; }
        set { BoundObject.transform.localPosition = value; }
    }  // Center of Sphere Bound
    public float Radius
    {   // assume x/y/z values are the same
        get { return BoundObject.transform.localScale.x / 2.0f; }
        set
        {
            float v = 2.0f * value; // default sphere has a radius of 0.5f
            BoundObject.transform.localScale = new Vector3(v, v, v);
        }
    }    // Radius of the bound

    #region Drawing Support
    public bool DrawBound
    {
        get { return BoundObject.activeSelf; }
        set { BoundObject.SetActive(value); }
    } // Draw or Hide the bound
    public Color BoundColor {
        get { return BoundObject.GetComponent<Renderer>().material.color; }
        set { BoundObject.GetComponent<Renderer>().material.color = value; }
    }  // Sets the color for drawing
    #endregion

    //  Returns if the give aPoint is inside the sphere
    public bool PointInSphere(Vector3 aPoint)
    {
        Vector3 diff = this.Center - aPoint;
        return diff.magnitude <= this.Radius;
    }

    //  Returns if there is an intersection with anotherSphere
    public bool SpheresIntersects(MySphereBound anotherSphere)
    {
        Vector3 diff = this.Center - anotherSphere.Center;
        return (diff.magnitude <= (this.Radius + anotherSphere.Radius));
    }
}
