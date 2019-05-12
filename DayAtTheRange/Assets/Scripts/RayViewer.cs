using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayViewer : MonoBehaviour {

    /*
     * Note this is a debugging tool. RayViewer will duplicate some of the functionality of our Raycast Shooting Scripts because its purpose is similar.  Instead however of casting a ray, it
     * we will simply drawing a line to show where our ray will be going in the scene view. In start we will grab a reference to the camera attached to Player Head and then in Update we will
     * first calculate the orgin point of our line and then we will draw it using debug.drawray.
     */

    // Declare variables
    public float weaponRange = 50f; // Public float variable used to determine how far our ray will be cast into the scene, intialized to a range of 50 units
    private Camera fpsCam; // Reference to Camera which is attached to PlayerHead


	// Use this for initialization
	void Start ()
    {
        fpsCam = GetComponentInParent<Camera>(); // Grabs reference to Camera attached to Player Head, parent of this Weapon

    }

    // Update is called once per frame
    void Update ()
    {
        Vector3 lineOrigin = fpsCam.ViewportToWorldPoint(new Vector3(0.5F, 0.5F, 0.0F)); // Draws the line from the center of the fpsCam
        Debug.DrawRay(lineOrigin,fpsCam.transform.forward * weaponRange, Color.green); // Draws a ray to help us to visualize where our ray is being cast in the scene view. 
    }
}
