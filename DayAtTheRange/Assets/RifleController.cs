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
    private int indexFireMode;

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
        if (Input.GetButton("Fire1") && Time.time > nextFireSingle)
        {
            nextFireSingle = Time.time + fireRateSingle;
            //Instantiate(bullet, bulletShotSpawn.transform.position, bulletShotSpawn.transform.rotation); // Instantiate bullet at position and rotation of bulletShotSpawn
            //Instantiate(bullet, bulletShotSpawnGameObject.transform.position, bulletShotSpawnGameObject.transform.rotation); // Instantiate bullet at position and rotation of bulletShotSpawnGameObject
            Instantiate(bullet, bulletSpawn.position, bulletSpawn.rotation); // -- Test code for shooting
            Debug.Log("Single Shot Fired");
        }
        // Burst
        // Automatic 
    }
}
