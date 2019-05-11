using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RifleRayCastController : MonoBehaviour {

    // Declare Variables

    //public int gunDamage = 1; // damage inflicted upon impact by gun
    public float fireRateSingle = 0.25f; // Public float variable used to determine the firerate of the weapon, how long the play will be able to fire
    public float weaponRange = 50f; // Public float variable used to determine how far our ray will be cast into the scene, intialized to a range of 50 units
    public float hitForce = 100; // Public float variable used to determine how much force we will apply to our target upon impact, when intersecting with a rigidbody
    public Transform bulletSpawn; // Public Transform used to determine the point our ray will cast from and out of the gun

    private Camera fpsCam; // Reference to Camera which is attached to PlayerHead
    private WaitForSeconds shotDuration = new WaitForSeconds(0.07F); // Value used to determine how long we want the laser to remain visible in the game view once the player has fired
    private LineRenderer laserLine; // line renderer takes an array of two or more points in 3d space and draws a straight line between each one in the game view
    private float nextFireSingle; // holds the time at which the player will be able to fire again after firing


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
        // Display Text to Screen Indicating the

        // Semi-Automatic
        //if (Input.GetButtonDown("Fire1") && Time.time > nextFireSingle && indexFireMode == 1 && hasAmmo == true)
        if(Input.GetButtonDown("Fire1") && Time.time > nextFireSingle)
        {
            nextFireSingle = Time.time + fireRateSingle; // Incriment nextFire so that the player must wait to fire again
            StartCoroutine(ShotEffect()); // Call our Shot Effect Coroutine
            /*
             * Cameras in Unity have two render planes, a near clip plane and a far clip plane, the near clip plane which forms the closest box in the
             * scene view when the camera is selected.The value of this grid styled, near clip plane range (0,0) to (1,1), with (0.5,0.5) being the
             * center point on screen. Passing 0 for z axis means the position will be zero units away from the camera or exactly where at the player's position.
             */
            // Spawn Point for ray, always centered to center of camera, this takes a position relative to the camera and converts it to a world point
            Vector3 rayOrigin = fpsCam.ViewportToWorldPoint(new Vector3 (0.5F, 0.5F, 0.0F));
            RaycastHit hit; // Holds the information returned from our array if it hits a gameObject with a collider attached
            
            // Determine start and end positions for our raycast line when the player fires, first we need to specficy two points for lineRenderer to draw between
            /*
             * We will use the results of this raycast for a number of things. The first thing we will use this for is to determine where the end point of our laserLine will be.
             * Later we will also use this raycast to apply physics forces to the object collider it has insected and to deal damage to it, if it has a damage script.
             * Physics.Raycast will return a boolean value, meaning if it hits something, it will hit to true. Placing it within an if statement will mean certain code executes if it hits
             * something.
             * 
             * The first parameter we will pass to our Physics.Raycast statement is rayOrigin, this will be the point in our world where our ray will begin, in this case the center of our
             * camera's viewpoint, stored in Ray Origin. The direction which we will cast our ray in, will be the forward direction of our fpsCam. We will store additional information about
             * the object, like it's Rigidbody, collider and surface normal, in our RaycastHit using the keyword "out". Using the "out" keyword allows us to store additional information from
             * a function, in addition to its return type. Finally, we will pass in weaponRange which is the distance over which we want to cast our ray.
             * 
             * There are two possible positions for the end of our laser, the first being whatever we hit with our raycast and the second being what happens when we hit nothing when firing into the sky.
             * This behavior will need to be accounted for as well. In the case where we hit somehting we will set the second position our line to hit.point. In the case where our raycast has not
             * intersected with anything, and therefore our first if statement returns false, we will use an else to set the end of our line weaponRange units away from the origin point, in the
             * forward direction of our camera.
             */
            laserLine.SetPosition(0, bulletSpawn.position); // Our Start Point for our raycast to be shot out of the gun

            if (Physics.Raycast(rayOrigin, fpsCam.transform.forward, out hit, weaponRange))
            {
                laserLine.SetPosition(1, hit.point); // index position 1 of Hit array, set to the world space position of object colliding with array.
            }
            else
            {
                laserLine.SetPosition(1, rayOrigin + (fpsCam.transform.forward * weaponRange)); // index position 1 of Hit array, set to the world space position of object colliding with array
            }


        }

    }

    private IEnumerator ShotEffect()
    {
        aud.PlayOneShot(gunShoot, 0.1F); // Play sound effect for shooting
        laserLine.enabled = true; // Enable our laserLine to display on screen
        yield return shotDuration; // Make the CoRoutine wait shotDuration to turn our laserLine back off
        laserLine.enabled = false; // Disable our laserLine to display on screen
    }
}
