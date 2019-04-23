using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GrenadePickUpAlert : MonoBehaviour 
{
    // Declare Variables
    public Text playerCanPickUpGrenade; // Text to alert player that they can pick up the Grenade


    // Use this for initialization
    void Start()
    {
        playerCanPickUpGrenade.text = ""; // Player cannot pick up the object so its blank 
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            playerCanPickUpGrenade.text = "Press F (X) to Pick Up Grenade"; // Alert Player to the fact they can activate the Grenade
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            playerCanPickUpGrenade.text = ""; // Alert Player to the fact they cannot activate the Grenade
        }
    }
}
