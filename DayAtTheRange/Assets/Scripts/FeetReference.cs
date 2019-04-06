using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeetReference : MonoBehaviour {

    private Rigidbody rb;
    private float speed;
    // Use this for initialization
    void Start ()
    {
        rb = GetComponent<Rigidbody>();
        speed = 2;
	}
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        rb.AddForce(movement * speed);
    }
}
