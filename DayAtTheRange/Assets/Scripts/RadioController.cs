using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RadioController : MonoBehaviour {

    // Declare Variables
    private int indexStation; // Value used to cycle between stations
    private bool consoleTextDisplayed; // Checked to see if console text is displayed for debugging

    private AudioSource aud;
    public AudioClip[] arrayOfAudioTracks;
    public AudioClip station1;
    public AudioClip station2;
    public AudioClip station3;

    public Text canActivateRadioText;
    private bool canActivate;



	// Use this for initialization
	void Start ()
    {
        consoleTextDisplayed = false; // Radio station has not been changed so, Console has not displayed text
        aud = GetComponent<AudioSource>(); // Reference to Audio Source Component
        arrayOfAudioTracks[0] = station1; // Set index value of Radio's track list postion 0 to first track
        arrayOfAudioTracks[1] = station2; // Set index value of Radio's track list position 1 to second track
        arrayOfAudioTracks[2] = station3; // Set index value of Radio's track list position 2 to third track

        canActivate = false; // Player not in range of collider to active, so false (0, because GetAxis is a float and cannot be evaluated alongside bool)
        canActivateRadioText.text = ""; // Player not in range of collider, so nothing to display

    }
	
	// Update is called once per frame
	void Update ()
    {
        if (indexStation == 1 && consoleTextDisplayed == false)
        {
            // Console Alert: Radio Station Set 1
            Debug.Log("Now Playing Master Disorder");
            // Play Track 1 "Master Disorder" and Stop the Other Tracks
            aud.PlayOneShot(arrayOfAudioTracks[0]);
            consoleTextDisplayed = true; // Radio station has been changed so, Console has displayed text
        }

        if (indexStation == 2 && consoleTextDisplayed == false)
        {
            // Console Alert: Radio Station Set 2
            Debug.Log("Now Playing Prelude and Action");
            // Play Track 2 "Prelude and Action"
            aud.PlayOneShot(arrayOfAudioTracks[1]);
            consoleTextDisplayed = true; // Radio station has been changed so, Console has displayed text
        }

        if (indexStation == 3 && consoleTextDisplayed == false)
        {
            // Console Alert: Radio Station Set 3
            Debug.Log("Now Playing Ready Aim Fire");
            // Play Track 3 "Ready Aim Fire"
            aud.PlayOneShot(arrayOfAudioTracks[2]);
            consoleTextDisplayed = true; // Radio station has been changed so, Console has displayed text
        }

        if (indexStation > arrayOfAudioTracks.Length && consoleTextDisplayed == false)
        {
            // Console Alert: Radio Station Rset
            Debug.Log("Radio Station Reset");
            indexStation = 1; // Radio Station set back to one
            consoleTextDisplayed = false; // Radio station has been changed so, Console has displayed text
        }

        // Below If Statement checks to see if fire button is presed
        if (Input.GetButtonDown("Interact") && canActivate == true)
        {
            // Console Alert: Switch Station Button Pressed
            Debug.Log("Switch Station Change Button Pressed");
            aud.Stop(); // Stop Playing the Previous Track
            indexStation++; // Then Incriment indexStation to change station track played
            consoleTextDisplayed = false; // Radio station has been changed so, Console has displayed text

        }
	}
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            canActivate = true; // Player can activate object, now in range of collider
            canActivateRadioText.text = "Press F (X) to Activate Radio"; // Alert Player to the fact they can activate the Radio
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            canActivate = false; // Player cannot activate object, not in range of collider
            canActivateRadioText.text = ""; // Alert Player to the fact they cannot activate the Radio
        }
    }
}
