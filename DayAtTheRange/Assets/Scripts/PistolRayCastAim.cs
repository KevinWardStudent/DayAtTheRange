using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistolRayCastAim : MonoBehaviour {

    private Animator anim; // Reference to Animator component

    // Use this for initialization
    void Start ()
    {
        anim = GetComponent<Animator>(); // Grabs reference to Player's Animator component
        anim.SetBool("PistolRayCastAimDownSight", false);
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetAxis("Fire2") > 0)
        {
            anim.SetBool("PistolRayCastAimDownSight", true);
        }
        else
        {
            anim.SetBool("PistolRayCastAimDownSight", false);
        }
    }
}
