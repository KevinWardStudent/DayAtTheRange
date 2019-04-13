using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByTime : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {
        Destroy(gameObject, 5.0F);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
