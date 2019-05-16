using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeController : MonoBehaviour {

   // private Rigidbody rb;
    private float throwPower;
    bool consoleTextDisplayed;
    public GameObject grenade;
    public GameObject grenadeSpawn;

    // FireMode
    private int indexFireMode; //  int to index through and to determine which fireMode the grenade is on


    // Audio
    private AudioSource aud; // Reference to AudioSource Component
    public AudioClip pinPull; // Reference to Pin Pull Sound Effect When Button is held down
    public AudioClip handleRelease; // Reference to Handle Sound Effect When Button is released

    float nextFire;
    float fireRate = 1.2F;
	// Use this for initialization
	void Start ()
    {
        //rb = GetComponent<Rigidbody>();
        //throwPower = 0.0F;
        consoleTextDisplayed = false; // No action completed, therefore no consoleTextDisplayed
        aud = GetComponent<AudioSource>();
        indexFireMode = 1; // Default fire mode is toss, thus set to 1
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetButton("Fire1"))
        {
            Debug.Log("Pin is pulled.");
            //throwPower++;
        }
        if (Input.GetButtonDown("Fire1") && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            aud.PlayOneShot(pinPull, 1.0F); // Play Pin Pull Sounnd Effect
        }
        if (Input.GetButtonUp("Fire1") && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            aud.PlayOneShot(handleRelease, 1.0F); // Play Handle Release Sound Effect
            ThrowGrenade();
        }
        if (indexFireMode == 1 && consoleTextDisplayed == false)
        {
            // Console Alert: FireMode set to single
            Debug.Log("Toss");
            throwPower = 10.0F; // Set throwPower to Toss Value
            // Play FireMode Switch Sound Effect
            //aud.PlayOneShot(FireModeSoundEffect);
            consoleTextDisplayed = true; // FireMode has been changed so, Console has displayed text
        }

        if (indexFireMode == 2 && consoleTextDisplayed == false)
        {
            // Console Alert: FireMode set to burst
            Debug.Log("Throw Close");
            throwPower = 15.0F; // Set throwPower to Throw Close Value
            // Play FireMode Switch Sound Effect
            //aud.PlayOneShot(FireModeSoundEffect);
            consoleTextDisplayed = true; // FireMode has been changed so, Console has displayed text
        }

        if (indexFireMode == 3 && consoleTextDisplayed == false)
        {
            // Console Alert: FireMode set to automatic
            Debug.Log("Throw Far");
            throwPower = 25.0F; // Set throwPower to Throw Far Value
            // Play FireMode Switch Sound Effect
            //aud.PlayOneShot(FireModeSoundEffect);
            consoleTextDisplayed = true; // FireMode has been changed so, Console has displayed text
        }

        if (indexFireMode > 3 && consoleTextDisplayed == false)
        {
            // Console Alert: FireMode Reset
            Debug.Log("Switch FireMode Reset");
            indexFireMode = 1; // FireMode set back to one
            consoleTextDisplayed = false; // FireMode has been changed so, Console has displayed text
        }

        // Below If Statement checks to see if FireMode is presed
        if (Input.GetButtonDown("FireMode"))
        {
            // Console Alert: Switch Station Button Pressed
            Debug.Log("Switch FireMode Button Pressed");
            //aud.Stop(); // Stop Playing the Previous Track
            indexFireMode++; // Then Incriment indexStation to change station track played
            consoleTextDisplayed = false; // Radio station has been changed so, Console has displayed text

        }
    }
    public void ThrowGrenade()
    {
        Debug.Log("Frag out!"); // Debug statement
        GameObject grenadePrefab = Instantiate(grenade, grenadeSpawn.transform.position, grenadeSpawn.transform.rotation); // Throw Greande at the location of this spawn's position and rotation
        Rigidbody rb = grenadePrefab.GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * throwPower, ForceMode.VelocityChange); // Send Grenade Forward with throwPower of fire mode selected, ignore mass of object
    }
}
