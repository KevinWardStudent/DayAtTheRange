using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetPoints : MonoBehaviour {

    //Declare Variables

    private bool beenShot; // Control Flag to check if the 
    private GameController gameController; // Reference to the game controller component

    private int health = 1; // How many shots does it take for this target to fall down? Just one.



    /*
     * Note to keep TargetAnimation.cs disabled or off the target game object to test this
     */
    // This One line is from TargetAnimation.cs and is here to animate the target.
    private Animator anim; // Reference variable for Animator component

    // Use this for initialization
    void Start()
    {
        // These two lines are from TargetAnimation.cs and are here to animate the target.
        anim = GetComponent<Animator>();
        anim.SetBool("RotateTargetUp", true);
        beenShot = false; // Target has not been shot
        GameObject gameControllerObject = GameObject.FindWithTag("GameController");
        if (gameControllerObject != null)
        {
            gameController = gameControllerObject.GetComponent<GameController>();
            Debug.Log("Let's score some points.");
        }
    }

    // Update is called once per frame
    void Update()
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

    public void healthCalculator (int damageIn)
    {
        health -= damageIn; // When called by RayCast weapons, we decriment this value to then execute following code
        if (health == 0 && beenShot == false)
        {
            // These four lines are from TargetAnimation.cs and are used here to animate the target.
            Debug.Log("Target HIT!");
            anim.SetBool("RotateTargetUp", false);
            anim.SetBool("RotateTargetDown", true);
            anim.SetBool("RotateTargetDown", false);
            gameController.AddPoints(1); // Add a Point
            beenShot = true; // Target has been shot
        }
    }
}
