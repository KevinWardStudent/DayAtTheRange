using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPlayerMover : MonoBehaviour {

    /*
     * This script's purpose is to simply move the Player Game Object on the X, Z plane 
     */

    // Declare Variables
    private Rigidbody rb; // References to Rigidbody Component
    public float movementSpeed; // Assigned in Unity Editor, Speed at which player moves

	// Use this for initialization
	void Start ()
    {
        rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movementInput = new Vector3(moveHorizontal, 0.0F, moveVertical);
        rb.velocity = movementInput * movementSpeed;
    }
}
