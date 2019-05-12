using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RifleRayCastAim : MonoBehaviour {

    private Animator anim; // Reference to Animator component

    // Use this for initialization
    void Start ()
    {
        anim = GetComponent<Animator>(); // Grabs reference to Player's Animator component
        anim.SetBool("AimDownSightRifleRayCast", false);
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetAxis("Fire2") > 0)
        {
            anim.SetBool("AimDownSightRifleRayCast", true);
        }
        else
        {
            anim.SetBool("AimDownSightRifleRayCast", false);
        }
    }
}
