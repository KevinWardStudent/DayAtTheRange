using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistolAnim : MonoBehaviour {
    private Animator anim; // Reference to Animator component
    // Use this for initialization
    void Start()
    {
        anim = GetComponent<Animator>(); // Grabs reference to Player's Animator component
        anim.SetBool("AimDownSight", false);
        anim.SetBool("GunShot", false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxisRaw("Fire1") > 0)
        {
            Debug.Log("Are you alive?");
        }
        else
        {
            //anim.SetBool("GunShot", false);
        }
        if (Input.GetAxis("Fire2") > 0)
        {
            anim.SetBool("AimDownSight", true);
        }
        else
        {
            anim.SetBool("AimDownSight", false);
        }
    }
}
