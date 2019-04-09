using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetAnimation : MonoBehaviour {

    /*
     * This script rotates the target on when spawned. Then when a bullet collides with the target, it rotates back down to its start position.
     */


    // Declare Variables
    private Animator anim; // Reference variable for Animator component

    // Use this for initialization
	void Start ()
    {
        anim = GetComponent<Animator>();
        anim.SetBool("RotateTargetUp",true);
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Bullet")
        {
            Debug.Log("Target HIT!");
            anim.SetBool("RotateTargetUp", false);
            anim.SetBool("RotateTargetDown", true);
            anim.SetBool("RotateTargetDown",false);
        }
    }
}
