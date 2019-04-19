using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PistolPickUpAlert : MonoBehaviour {

    // Declare Variables
    public Text playerCanPickUpPistol; // Text to alert player that they can pick up the Pistol


    // Use this for initialization
    void Start()
    {
        playerCanPickUpPistol.text = ""; // Player cannot pick up the object so its blank 
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            playerCanPickUpPistol.text = "Press F (X) to Pick Up Pistol"; // Alert Player to the fact they can activate the Pistol
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            playerCanPickUpPistol.text = ""; // Alert Player to the fact they cannot activate the Pistol
        }
    }
}
