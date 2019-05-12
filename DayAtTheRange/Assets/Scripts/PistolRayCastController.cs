using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PistolRayCastController : MonoBehaviour {

    // Declare Variables

    public int gunDamage = 1; // damage inflicted upon impact by gun
    public float weaponRange = 50f; // Public float variable used to determine how far our ray will be cast into the scene, intialized to a range of 50 units

    public float hitForce = 100; // Public float variable used to determine how much force we will apply to our target upon impact, when intersecting with a rigidbody
    public Transform bulletSpawn; // Public Transform used to determine the point our ray will cast from and out of the gun

    private Camera fpsCam; // Reference to Camera which is attached to PlayerHead
    private WaitForSeconds shotDuration = new WaitForSeconds(0.07F); // Value used to determine how long we want the laser to remain visible in the game view once the player has fired
    private LineRenderer laserLine; // line renderer takes an array of two or more points in 3d space and draws a straight line between each one in the game view

    // Casing, Magazine Full & Empty Reference
    public GameObject casing; // Casing that is ejected
    public Transform casingSpawn; // Test example of script reference, really just an assignment in unity editor
    public GameObject magFull; // Full Mag that is ejected
    public Transform magFullSpawn; // Test example of script reference, really just an assign in unity editor
    public GameObject magEmpty; // Empty Mag that is ejected
    public Transform magEmptySpawn; // Test example of script reference, really just an assignment in unity editor

    // fireRates and nextFires of Semi-Automatic
    public float fireRateSingle; // Public float variable used to determine the firerate of the weapon, how long the play will be able to fire
    private float nextFireSingle; // holds the time at which the player will be able to fire again after firing

    // fireRates and nextFires of Automatic
    public float fireRateAutomatic;
    private float nextFireAutomatic;


    // index for fire mode
    private int indexFireMode; // Value used to cycle between FireModes
    private bool consoleTextDisplayed; // Checked to see if console text is displayed for debugging

    // Ammo Counter
    int ammoCount; // Ammo currently in magazine
    public Text ammoCountText; // Displays text to screen
    private bool hasAmmo; // Checks if there is ammo in the magazine

    // Sounds Effects
    private AudioSource aud; // Reference to AudioSource Component attached to Rifle
    public AudioClip gunShoot; // Audio Clip Assigned in Unity Editor, the rifle shot sound effect
    public AudioClip gunEquipReload; // Audio Clip Assigned in Unity Editor, the rifle equip sound effect


    // Use this for initialization
    void Start ()
    {
        laserLine = GetComponent<LineRenderer>(); // Grabs Reference to LineRendererComponent attached to this Weapon 
        aud = GetComponent<AudioSource>(); // Grabs reference to AudioSourceComponent attached to Weapon
        fpsCam = GetComponentInParent<Camera>(); // Grabs reference to Camera attached to Player Head, parent of this Weapon



        consoleTextDisplayed = false; // FireMode has not been changed so, Console has not displayed text
        indexFireMode = 1; // Fire Mode by default is single shot, there indexFireMode is 1
        ammoCount = 30; // ammo when gun is picked up
        ammoCountText.text = ammoCount.ToString() + " / " + "Infinite"; // Display text of ammoCount
        hasAmmo = true; // There are bullets in the magazaine so true
    }
	
	// Update is called once per frame
	void Update ()
    {
        ammoCountText.text = ammoCount.ToString() + " / " + "Infinite"; // Display text of ammoCount

        // Semi-Automatic
        if (Input.GetButtonDown("Fire1") && Time.time > nextFireSingle && indexFireMode == 1 && hasAmmo == true)
        {
            nextFireSingle = Time.time + fireRateSingle; // Incriment nextFire so that the player must wait to fire again
            StartCoroutine(ShotEffect()); // Call our Shot Effect Coroutine

            // Spawn Point for ray, always centered to center of camera, this takes a position relative to the camera and converts it to a world point
            Vector3 rayOrigin = fpsCam.ViewportToWorldPoint(new Vector3(0.5F, 0.5F, 0.0F));
            RaycastHit hit; // Holds the information returned from our array if it hits a gameObject with a collider attached

            // Determine start and end positions for our raycast line when the player fires, first we need to specficy two points for lineRenderer to draw between
            laserLine.SetPosition(0, bulletSpawn.position); // Our Start Point for our raycast to be shot out of the gun

            if (Physics.Raycast(rayOrigin, fpsCam.transform.forward, out hit, weaponRange))
            {
                laserLine.SetPosition(1, hit.point); // index position 1 of Hit array, set to the world space position of object colliding with array.

                TargetPoints targetHealth = hit.collider.GetComponent<TargetPoints>(); // Using hit, store scripting reference of TargetPoints in targetHealth

                if (targetHealth != null) // If the target hit did have TargetPoints attached then...
                {
                    // Call healthCalculator function of TargetPoints and pass in this gun's Damage as a parameter
                    targetHealth.healthCalculator(gunDamage);
                }

                if (hit.rigidbody != null) // If the target hit did have a rigidbody attached then...
                {
                    // AddForce to target in the direction negative to its normals and with hitForce as a multiplier
                    hit.rigidbody.AddForce(-hit.normal * hitForce);
                }

            }
            else
            {
                laserLine.SetPosition(1, rayOrigin + (fpsCam.transform.forward * weaponRange)); // index position 1 of Hit array, set to the world space position of object colliding with array
            }
            ammoCount--; // Iterate ammoCount by one down each shot
            Instantiate(casing, casingSpawn.position, casingSpawn.rotation); // -- Test code for ejecting casings -- This is the only one that works because of Game Object Reference not working
            Debug.Log("Single Shot Fired");
        }

        // Automatic
        if (Input.GetButton("Fire1") && Time.time > nextFireAutomatic && indexFireMode == 2 && hasAmmo == true)
        {
            nextFireAutomatic = Time.time + fireRateAutomatic;
            StartCoroutine(ShotEffect()); // Call our Shot Effect Coroutine

            // Spawn Point for ray, always centered to center of camera, this takes a position relative to the camera and converts it to a world point
            Vector3 rayOrigin = fpsCam.ViewportToWorldPoint(new Vector3(0.5F, 0.5F, 0.0F));
            RaycastHit hit; // Holds the information returned from our array if it hits a gameObject with a collider attached

            // Determine start and end positions for our raycast line when the player fires, first we need to specficy two points for lineRenderer to draw between
            laserLine.SetPosition(0, bulletSpawn.position); // Our Start Point for our raycast to be shot out of the gun

            if (Physics.Raycast(rayOrigin, fpsCam.transform.forward, out hit, weaponRange))
            {
                laserLine.SetPosition(1, hit.point); // index position 1 of Hit array, set to the world space position of object colliding with array.

                TargetPoints targetHealth = hit.collider.GetComponent<TargetPoints>(); // Using hit, store scripting reference of TargetPoints in targetHealth

                if (targetHealth != null) // If the target hit did have TargetPoints attached then...
                {
                    // Call healthCalculator function of TargetPoints and pass in this gun's Damage as a parameter
                    targetHealth.healthCalculator(gunDamage);
                }

                if (hit.rigidbody != null) // If the target hit did have a rigidbody attached then...
                {
                    // AddForce to target in the direction negative to its normals and with hitForce as a multiplier
                    hit.rigidbody.AddForce(-hit.normal * hitForce);
                }

            }
            else
            {
                laserLine.SetPosition(1, rayOrigin + (fpsCam.transform.forward * weaponRange)); // index position 1 of Hit array, set to the world space position of object colliding with array
            }

            ammoCount--; // Iterate ammoCount by one down each shot
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
        if(indexFireMode == 2 && consoleTextDisplayed == false)
        {
            // Console Alert: FireMode set to automatic
            Debug.Log("Automatic");
            // Play FireMode Switch Sound Effect
            //aud.PlayOneShot(FireModeSoundEffect);
            consoleTextDisplayed = true; // FireMode has been changed so, Console has displayed text
        }

        if (indexFireMode > 2 && consoleTextDisplayed == false)
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
            aud.PlayOneShot(gunEquipReload, 1.0F); // Play Reload Sound Effect
            ammoCount = 15;
            hasAmmo = true;
            if (ammoCount <= 0)
            {
                Instantiate(magEmpty, magEmptySpawn.position, magEmptySpawn.rotation); // Spawn empty mag if ammoCount is 0
            }
            if (ammoCount > 0)
            {
                ammoCount = 15 + 1;
                Instantiate(magFull, magFullSpawn.position, magFullSpawn.rotation); // Spawn full mag if ammoCount is greater than 0 
            }
        }
        if (ammoCount <= 0)
        {
            hasAmmo = false;
        }
    }
    private IEnumerator ShotEffect()
    {
        // Comment out laserline if you want to make laser invisible
        aud.PlayOneShot(gunShoot, 0.1F); // Play sound effect for shooting
        laserLine.enabled = true; // Enable our laserLine to display on screen
        yield return shotDuration; // Make the CoRoutine wait shotDuration to turn our laserLine back off
        laserLine.enabled = false; // Disable our laserLine to display on screen
    }
}
