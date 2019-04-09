using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RifleController : MonoBehaviour {

    /*
     * The point of this script is shoot the gun and probably and will allow for pick up.
     * This also controls fire more for the game. 
     */

    // Declare Variables

    // References for Spawn Locations
    //private RPBulletSpawnReference bulletShotSpawn;
    //private RPCasingSpawnReference casingShotSpawn;
    //private RPMagEmptySpawnReference magEmptyShotSpawn;

    //GameObject bulletShotSpawnGameObject = GameObject.Find("RiflePrototypeBulletSpawn");

    private GameObject bulletShotSpawn; // Comment this out if you want to try the reference

    // index for fire mode
    private int indexFireMode; // Value used to cycle between FireModes
    private bool consoleTextDisplayed; // Checked to see if console text is displayed for debugging

    // Bullet Reference
    public GameObject bullet;
    public Transform bulletSpawn; // -- Test Code for Shooting
    
    // fireRates and nextFires of Single
    public float fireRateSingle;
    private float nextFireSingle;

    // fireRates and nextFires of Burst
    public float fireRateBurst;
    private float nextFireBurst;

    // fireRates and nextFires of Automatic
    public float fireRateAutomatic;
    private float nextFireAutomatic;
    
    // Use this for initialization
    void Start ()
    {
        consoleTextDisplayed = false; // FireMode has not been changed so, Console has not displayed text
        indexFireMode = 1;
        // References to Spawns Locations, storing Game Objects of reference scripts in them
        /*
        GameObject bulletShotSpawnGameObject = GameObject.Find("RiflePrototypeBulletSpawn");
        if (bulletShotSpawnGameObject != null)
        {
            Debug.Log("Gun knows where bullet should spawn.");
            bulletShotSpawn = bulletShotSpawnGameObject.GetComponent<RPBulletSpawnReference>();
        }
        */

    }
	
	// Update is called once per frame
	void Update ()
    {
        // Semi-Automatic
        if (Input.GetButtonDown("Fire1") && Time.time > nextFireSingle && indexFireMode == 1)
        {
            nextFireSingle = Time.time + fireRateSingle;
            //Instantiate(bullet, bulletShotSpawn.transform.position, bulletShotSpawn.transform.rotation); // Instantiate bullet at position and rotation of bulletShotSpawn
            //Instantiate(bullet, bulletShotSpawnGameObject.transform.position, bulletShotSpawnGameObject.transform.rotation); // Instantiate bullet at position and rotation of bulletShotSpawnGameObject
            Instantiate(bullet, bulletSpawn.position, bulletSpawn.rotation); // -- Test code for shooting -- This is the only one that works because of Game Object Reference not working
            Debug.Log("Single Shot Fired");
        }

        // Burst
        if (Input.GetButtonDown("Fire1") && Time.time > nextFireBurst && indexFireMode == 2)
        {
            nextFireBurst = Time.time + fireRateBurst;
            Instantiate(bullet, bulletSpawn.position, bulletSpawn.rotation); // -- Test code for shooting -- This is the only one that works because of Game Object Reference not working
            Instantiate(bullet, bulletSpawn.position, bulletSpawn.rotation); // -- Test code for shooting -- This is the only one that works because of Game Object Reference not working
            Instantiate(bullet, bulletSpawn.position, bulletSpawn.rotation); // -- Test code for shooting -- This is the only one that works because of Game Object Reference not working
            Debug.Log("Single Shot Fired");
        }

        // Automatic
        if (Input.GetButton("Fire1") && Time.time > nextFireAutomatic && indexFireMode == 3)
        {
            nextFireAutomatic = Time.time + fireRateAutomatic;
            Instantiate(bullet, bulletSpawn.position, bulletSpawn.rotation); // -- Test code for shooting -- This is the only one that works because of Game Object Reference not working
            Debug.Log("Automatic Shot Fired");
        }

        if (indexFireMode == 0 && consoleTextDisplayed == false)
        {
            // Console Alert: FireMode set to safe
            Debug.Log("Safe");
            // Play FireMode Switch Sound Effect
            //aud.PlayOneShot(FireModeSoundEffect);
            consoleTextDisplayed = true; // FireMode has been changed so, Console has displayed text
        }
        if (indexFireMode == 1 && consoleTextDisplayed == false)
        {
            // Console Alert: FireMode set to single
            Debug.Log("Single");
            // Play FireMode Switch Sound Effect
            //aud.PlayOneShot(FireModeSoundEffect);
            consoleTextDisplayed = true; // FireMode has been changed so, Console has displayed text
        }

        if (indexFireMode == 2 && consoleTextDisplayed == false)
        {
            // Console Alert: FireMode set to burst
            Debug.Log("Burst");
            // Play FireMode Switch Sound Effect
            //aud.PlayOneShot(FireModeSoundEffect);
            consoleTextDisplayed = true; // FireMode has been changed so, Console has displayed text
        }

        if (indexFireMode == 3 && consoleTextDisplayed == false)
        {
            // Console Alert: Radio Station Set 3
            Debug.Log("Automatic");
            // Play FireMode Switch Sound Effect
            //aud.PlayOneShot(FireModeSoundEffect);
            consoleTextDisplayed = true; // FireMode has been changed so, Console has displayed text
        }

        if (indexFireMode > 3 && consoleTextDisplayed == false)
        {
            // Console Alert: Radio Station Rset
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
}
