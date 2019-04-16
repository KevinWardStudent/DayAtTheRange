using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    private GameObject casingShotSpawn; // Test example of script reference
    private GameObject magEmptyShotSpawn; // Test example of script reference

    // index for fire mode
    private int indexFireMode; // Value used to cycle between FireModes
    private bool consoleTextDisplayed; // Checked to see if console text is displayed for debugging

    // Bullet & Casing Reference
    public GameObject bullet; // Bullet that is shot
    public Transform bulletSpawn; // -- Test Code for Shooting, will try raycasting
    public GameObject casing; // Casing that is ejected
    public Transform casingSpawn; // Test example of script reference, really just an assignment in unity editor

    // Magazine Full & Empty Reference
    public GameObject magFull; // Full Mag that is ejected
    public Transform magFullSpawn; // Test example of script reference, really just an assign in unity editor
    public GameObject magEmpty; // Empty Mag that is ejected
    public Transform magEmptySpawn; // Test example of script reference, really just an assignment in unity editor

    // Ammo Counter
    int ammoCount; // Ammo currently in magazine
    //public Text ammoCountText; // Displays text to screen
    private bool hasAmmo; // Checks if there is ammo in the magazine

    // fireRates and nextFires of Single
    public float fireRateSingle;
    private float nextFireSingle;

    // fireRates and nextFires of Burst
    public float fireRateBurst;
    private float nextFireBurst;

    // fireRates and nextFires of Automatic
    public float fireRateAutomatic;
    private float nextFireAutomatic;

    // Sounds Effects
    private AudioSource aud; // Reference to AudioSource Component attached to Rifle
    public AudioClip gunShoot; // Audio Clip Assigned in Unity Editor, the rifle shot sound effect

    TestPlayerMover player;


    // Use this for initialization
    void Start()
    {

        //player = TestPlayerMover.instance;



        consoleTextDisplayed = false; // FireMode has not been changed so, Console has not displayed text
        indexFireMode = 1; // Fire Mode by default is single shot, there indexFireMode is 1
        ammoCount = 30; // ammo when gun is picked up
        //ammoCountText.text = ammoCount.ToString() + " / " + "Infinite"; // Display text of ammoCount
        hasAmmo = true; // There are bullets in the magazaine so true
        aud = GetComponent<AudioSource>(); // Grabs reference to AudioSourceComponent attached to Rifle


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
    void Update()
    {
        if (player == null)
        {
            return;
        }
        else
        {
            player.AmmoTextUpdate(ammoCount.ToString() + " / " + "Infinite");
        }
        //ammoCountText.text = ammoCount.ToString() + " / " + "Infinite"; // Display text of ammoCount
        // Semi-Automatic
        if (Input.GetButtonDown("Fire1") && Time.time > nextFireSingle && indexFireMode == 1 && hasAmmo == true)
        {
            nextFireSingle = Time.time + fireRateSingle;
            //Instantiate(bullet, bulletShotSpawn.transform.position, bulletShotSpawn.transform.rotation); // Instantiate bullet at position and rotation of bulletShotSpawn
            //Instantiate(bullet, bulletShotSpawnGameObject.transform.position, bulletShotSpawnGameObject.transform.rotation); // Instantiate bullet at position and rotation of bulletShotSpawnGameObject
            
            Instantiate(bullet, bulletSpawn.position, bulletSpawn.rotation); // -- Test code for shooting -- This is the only one that works because of Game Object Reference not working
            ammoCount--; // Iterate ammoCount by one down each shot
            aud.PlayOneShot(gunShoot, 0.1F); // Play sound effect for shooting
            Instantiate(casing, casingSpawn.position, casingSpawn.rotation); // -- Test code for ejecting casings -- This is the only one that works because of Game Object Reference not working
            Debug.Log("Single Shot Fired");
        }

        // Burst
        if (Input.GetButtonDown("Fire1") && Time.time > nextFireBurst && indexFireMode == 2 && hasAmmo == true)
        {
            nextFireBurst = Time.time + fireRateBurst;
            Instantiate(bullet, bulletSpawn.position, bulletSpawn.rotation); // -- Test code for shooting -- This is the only one that works because of Game Object Reference not working
            aud.PlayOneShot(gunShoot, 0.1F); // Play sound effect for shooting
            Instantiate(casing, casingSpawn.position, casingSpawn.rotation); // -- Test code for ejecting casings -- This is the only one that works because of Game Object Reference not working
            Instantiate(bullet, bulletSpawn.position, bulletSpawn.rotation); // -- Test code for shooting -- This is the only one that works because of Game Object Reference not working
            aud.PlayOneShot(gunShoot, 0.1F); // Play sound effect for shooting
            Instantiate(casing, casingSpawn.position, casingSpawn.rotation); // -- Test code for ejecting casings -- This is the only one that works because of Game Object Reference not working
            Instantiate(bullet, bulletSpawn.position, bulletSpawn.rotation); // -- Test code for shooting -- This is the only one that works because of Game Object Reference not working
            aud.PlayOneShot(gunShoot, 0.1F); // Play sound effect for shooting
            Instantiate(casing, casingSpawn.position, casingSpawn.rotation); // -- Test code for ejecting casings -- This is the only one that works because of Game Object Reference not working
            ammoCount -= 3; // Iterate ammoCount by three because you burst fired
            Debug.Log("Burst Shot Fired");
        }

        // Automatic
        if (Input.GetButton("Fire1") && Time.time > nextFireAutomatic && indexFireMode == 3 && hasAmmo == true)
        {
            nextFireAutomatic = Time.time + fireRateAutomatic;
            Instantiate(bullet, bulletSpawn.position, bulletSpawn.rotation); // -- Test code for shooting -- This is the only one that works because of Game Object Reference not working
            ammoCount--; // Iterate ammoCount by one down each shot
            aud.PlayOneShot(gunShoot, 0.1F); // Play sound effect for shooting
            Instantiate(casing, casingSpawn.position, casingSpawn.rotation); // -- Test code for ejecting casings -- This is the only one that works because of Game Object Reference not working
            Debug.Log("Automatic Shot Fired");
        }

        // Fire Modes
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
            // Console Alert: FireMode set to automatic
            Debug.Log("Automatic");
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

        // Ammo Management and Reload
        if (Input.GetButtonDown("Reload"))
        {
            ammoCount = 30;
            hasAmmo = true;
            if (ammoCount <= 0)
            {
                Instantiate(magEmpty, magEmptySpawn.position, magEmptySpawn.rotation); // Spawn empty mag if ammoCount is 0
            }
            if (ammoCount > 0)
            {
                ammoCount = 30 + 1;
                Instantiate(magFull, magFullSpawn.position, magFullSpawn.rotation); // Spawn full mag if ammoCount is greater than 0 
            }
        }
        if (ammoCount <= 0)
        {
            hasAmmo = false;
        }
    }

    // Below Coroutine used to simulate burst fire
    IEnumerator BurstFire()
    {
        return null;
    }

    public void PickedUp()
    {
        player = GetComponentInParent<TestPlayerMover>();

    }
    public void PutDown()
    {
        player = null;
    }

}
