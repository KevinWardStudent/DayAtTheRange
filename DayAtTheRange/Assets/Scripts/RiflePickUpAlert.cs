using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RiflePickUpAlert : MonoBehaviour {

    // Declare Variables
    public Text playerCanPickUpRifle; // Text to alert player that they can pick up the Rifle


	// Use this for initialization
	void Start ()
    {
        playerCanPickUpRifle.text = ""; // Player cannot pick up the object so its blank
	}
	
	// Update is called once per frame
	void Update ()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            playerCanPickUpRifle.text = "Press F (X) to Pick Up Rifle"; // Alert Player to the fact they can activate the Radio
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            playerCanPickUpRifle.text = ""; // Alert Player to the fact they cannot activate the Radio
        }
    }
}
