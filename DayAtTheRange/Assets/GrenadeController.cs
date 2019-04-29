using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeController : MonoBehaviour {

   // private Rigidbody rb;
    private float throwPower;
    bool consoleTextDisplayed;
    public GameObject grenade;
    public GameObject grenadeSpawn;

	// Use this for initialization
	void Start ()
    {
        //rb = GetComponent<Rigidbody>();
        throwPower = 0;
        consoleTextDisplayed = false; // No action completed, therefore no consoleTextDisplayed
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetButton("Fire1"))
        {
            throwPower++;
        }
        if (Input.GetButtonUp("Fire1"))
        {
            ThrowGrenade();
        }
	}
    public void ThrowGrenade()
    {
        Debug.Log("Frag out!"); // Debug statement
        GameObject grenadePrefab = Instantiate(grenade, grenadeSpawn.transform.position, grenadeSpawn.transform.rotation); // Throw Greande at the location of this spawn's position and rotation
        Rigidbody rb = grenadePrefab.GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * throwPower);
    }
}
