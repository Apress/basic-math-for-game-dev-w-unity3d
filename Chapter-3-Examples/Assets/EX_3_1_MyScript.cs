using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EX_3_1_MyScript : MonoBehaviour
{
    #region Public Variables
    public GameObject Checker = null;         // The spheres to work with
    public GameObject Stripe = null;

    public Vector3 CheckerPosition = Vector3.zero;
    public Vector3 StripePosition = Vector3.zero;
    
    public float DistanceBetween = 0.0f;
    public float MagnitudeOfVector = 0.0f;
    #endregion 

    // Start is called before the first frame update
    void Start()
    {
		Debug.Assert(Checker!= null);	// Make sure proper editor setup
        Debug.Assert(Stripe != null);   // Make sure proper editor setup
    }

    // Update is called once per frame
    void Update()
    {
        // Update the sphere positions
        Checker.transform.localPosition = CheckerPosition;
        Stripe.transform.localPosition = StripePosition;

        // Apply Pythagorean Theorem to compute distance
        float dx = StripePosition.x - CheckerPosition.x;
        float dy = StripePosition.y - CheckerPosition.y;
        float dz = StripePosition.z - CheckerPosition.z;
        DistanceBetween = Mathf.Sqrt(dx*dx + dy*dy + dz*dz);

        // Compute the magnitude of a Vector3 
        Vector3 diff = StripePosition - CheckerPosition;
        MagnitudeOfVector = diff.magnitude;

        #region Display the dx, dy, and dz 
        Vector3 posB = CheckerPosition + new Vector3(dx, 0f, 0f);  // Position B of Figure 3.1
        Vector3 posC = posB + new Vector3(0f, dy, 0f);             // Position C of Figure 3.1
        Vector3 posD = posC + new Vector3(0f, 0f, dz);             // Position D of Figure 3.1
        Debug.DrawLine(CheckerPosition, posB, Color.red);          // Connect the positions with 
        Debug.DrawLine(posB, posC, Color.green);                   // Color lines
        Debug.DrawLine(posC, posD, Color.blue);
        Debug.DrawLine(CheckerPosition, posD, Color.black);
        #endregion
    }
}
