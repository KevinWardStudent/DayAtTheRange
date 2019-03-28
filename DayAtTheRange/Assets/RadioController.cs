using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadioController : MonoBehaviour {

    // Declare Variables
    private int indexStation; // Value used to cycle between stations
    private bool consoleTextDisplayed; // Checked to see if console text is displayed for debugging
    private int indexStationLimit;

    //public GameObject station1;
    //public GameObject station2;
    //public GameObject station3;

    private AudioSource aud;
    //public ArrayList audioTracks;
    public AudioClip[] arrayOfAudioTracks;
    public AudioClip station1;
    public AudioClip station2;
    public AudioClip station3;


	// Use this for initialization
	void Start ()
    {
        aud = GetComponent<AudioSource>();
        //arrayOfAudioTracks.Length += station1;
            //.Add(station1);

    }
	
	// Update is called once per frame
	void Update ()
    {
        if (indexStation == 1)
        {
            // Track 1
        }

        if (indexStation == 2)
        {
            // Track 2
        }

        if (indexStation == 3)
        {
            // Track 3
        }

        if (indexStation > indexStationLimit)
        {
            indexStation = 1;
        }
	}
}
