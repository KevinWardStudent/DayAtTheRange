using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeExploder : MonoBehaviour {


    public float fuze = 3.0F; // Public float variable to determine the time before the grenade should explode
    private float countdown; // private float variable to decriment until the grenade needs to explode
    private bool consoleTextDisplayed; // Control Flag to check if Grenade explodes and stop our text from filling up the console

    // Particle Effects
    public GameObject explosionEffect; // public GameObject that references our particle which we want to have show when the grenade blows up
    public GameObject dirtKickUp1; // public GameObject that references our dirt particle which we want to show when the grenade blows up
    public GameObject dirtKickUp2;
    public GameObject dirtKickUp3;

    // Audio
    private AudioSource aud; // Reference to Audio Source Component
    public AudioClip soundEffectExplode; // Reference to Audio Clip for Explosion

    // Use this for initialization
    void Start()
    {
        aud = GetComponent<AudioSource>(); // Grabs reference to attached Audio Source Component
        countdown = fuze; // Grenade has not been "fired" yet so its countdown is the same as fuze
        consoleTextDisplayed = false; // No action completed, therefore no consoleTextDisplayed
    }

    // Update is called once per frame
    void Update()
    {
        countdown -= Time.deltaTime; // Decriment countdown by Time
        if (countdown <= 0 && consoleTextDisplayed == false)
        {
            GoBoom(); // Call function to explode grenade
            consoleTextDisplayed = true; // Console text has been displayed
        }   
        
    }
    public void GoBoom()
    {
        Debug.Log("I've worked up a big grunty thirst!!"); // YapYap King of Grunts orders so!
        aud.PlayOneShot(soundEffectExplode, 1.0F); // Play explosion sound effect
        Instantiate(explosionEffect, transform.position, transform.rotation); // Show our particle effect at the location of this grenade's position and rotation
        Instantiate(dirtKickUp1, transform.position, transform.rotation); // Show the dirt kick up of the grenade
        Instantiate(dirtKickUp2, transform.position, transform.rotation); // Dirt Kick Up 2
        Instantiate(dirtKickUp3, transform.position, transform.rotation); // Dirt Kick Up 3
        Destroy(gameObject); // Destory the Grenade
    }
}
