using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeanAnimation : MonoBehaviour {

    /*
     * The purpose of this script is to allow for leaning on the player game object without ceasing movement on the parent.
     */

    private Animator anim;
	// Use this for initialization
	void Start ()
    {
        anim = GetComponent<Animator>();
	}

    // Update is called once per frame
    void Update()
    {

        if (Input.GetAxis("Lean") > 0 && !(Input.GetAxis("Lean") < 0))
        {
            Debug.Log("Do the lean! 'Right'");
            anim.SetBool("LeanRight", true);
        }
        else
        {
            anim.SetBool("LeanRight", false);
        }
        if (Input.GetAxis("Lean") < 0 && !(Input.GetAxis("Lean") > 0))
        {
            Debug.Log("Do the lean! 'Left'");
            anim.SetBool("LeanLeft", true);
        }
        else
        {
            anim.SetBool("LeanLeft", false);
        }


    }
}
