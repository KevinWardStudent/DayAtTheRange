using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetPoints : MonoBehaviour {

    //Declare Variables

    private bool beenShot; // Control Flag to check if the 
    private GameController gameController; // Reference to the 

    // Use this for initialization
    void Start()
    {
        beenShot = false; // Target has not been shot
        GameObject gameControllerObject = GameObject.FindWithTag("GameController");
        if (gameControllerObject != null)
        {
            gameController = gameControllerObject.GetComponent<GameController>();
            Debug.Log("Let's score some points.");
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if ((other.tag == "Bullet" && beenShot == false) || (other.tag == "Fragment" && beenShot == false))
        {
            gameController.AddPoints(1); // Add a Point
            beenShot = true; // Target has been shot

        }

    }
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Fragment")
        {
            Debug.Log("Eureka!");
        }
    }
}
