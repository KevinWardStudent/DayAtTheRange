using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    // Declare Variables
    // Points Related Variables
    private int currentPoints; // Int variable used to hold the number of points at the given moment
    public Text pointsText;
    public Text victoryText;

	// Use this for initialization
	void Start ()
    {
        currentPoints = 0; // At the start of the game, no targets are shot, thus points is 0
        UpdatePoints(); // Call points to be displayed
        victoryText.text = ""; // Victory Not achieved, no text to display 
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (currentPoints >= 20)
        {
            victoryText.text = "All Targets down. Time to leave. (You Win!)";
        }
	}

    // Used by other scripts, like the target scripts, to add points to the locally stored value of current points
    public void AddPoints(int pointsToAdd)
    {
        currentPoints += pointsToAdd; // Add points to currentPoints
        UpdatePoints(); // Update text displaying this
    
    }

    void UpdatePoints()
    {
        pointsText.text = "Points: " + currentPoints.ToString() + "/ 20";
    }
}
