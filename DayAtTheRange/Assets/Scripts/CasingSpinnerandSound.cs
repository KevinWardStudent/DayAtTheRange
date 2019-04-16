using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CasingSpinnerandSound : MonoBehaviour {

    // Declare Variables
    private AudioSource aud; // Reference to AudioSource Componnet on Casing Game Object
    public AudioClip casingCollides; // Sound Effect played when Casing collides with another object
    private Rigidbody rb; // Reference to Rigidbody Component on Casing Game Object

	// Use this for initialization
	void Start ()
    {
        aud = GetComponent<AudioSource>(); // Grab Component
        rb = GetComponent<Rigidbody>(); // Grab Component
	}
	
	// Update is called once per frame
	void Update ()
    {
        rb.AddForce(new Vector3(100,0,0)); // Push casing to right as if being forcefully ejecting from gun
        transform.Rotate(new Vector3 (5, 15, 45) * Time.deltaTime); // Rotates Casing as it presumably falls from bolt after casing spawns
	}

    private void OnTriggerEnter(Collider other)
    {
        aud.PlayOneShot(casingCollides, 0.1F); // Play audio clip indcating casing has made contact with some object
        if (other.tag == "Ground")
        {
            rb.isKinematic = true;
        }
    }
}
