using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoundStart : MonoBehaviour {

    private AudioSource aud; // Reference to AudioSource Component
    private AudioClip buttonPressSoundEffect; // Sound Effect played when button is pressed
    private GameObject light; // Light attached to Button

	// Use this for initialization
	void Start ()
    {
        aud = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            if (Input.GetButton("Interact"))
            {
                aud.PlayOneShot(buttonPressSoundEffect); // Play Sound Effect
                StartCoroutine(Restart()); // Call Restart, loads Range Level
            }
        }
    }
    IEnumerator Restart()
    {
        // Change Light Color
        yield return new WaitForSeconds(5.0F);
        SceneManager.LoadScene("Range");
    }
}
