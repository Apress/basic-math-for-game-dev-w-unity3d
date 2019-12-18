using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyScript : MonoBehaviour
{
	public GameObject LeftSphere = null;        // Sphere to the left in the init Editor View
	public GameObject CenterSphere = null;      // Sphere in the center in the init Editor View
    public GameObject RightSphere = null;       // Sphere to the right in the init Editor View

    private readonly float kSmallDelta = 0.01f; // amount to translate

    // Start is called before the first frame update
    void Start()
    {
		Debug.Assert(LeftSphere != null);		// Make sure proper editor setup
		Debug.Assert(CenterSphere != null);     // Assume these variables are properly initialized
		Debug.Assert(RightSphere != null);      // to reference to: Checker, Brick, and, Stripe
	}

    // Update is called once per frame
    void Update()
    {
        // This prints the argument string to the Console Window
        Debug.Log("Printing to Console: Convenient way of examine state.");

        // Update the sphere positions
        //	Left moves in the positive X-direction
        LeftSphere.transform.localPosition += new Vector3(kSmallDelta, 0.0f, 0.0f);

		//	Center moves in the positive Y-direction
		CenterSphere.transform.localPosition += new Vector3(0.0f, kSmallDelta, 0.0f);
		
		//	Right moves in the positive Z-direction
		RightSphere.transform.localPosition += new Vector3(0.0f, 0.0f, kSmallDelta);
	}
}
