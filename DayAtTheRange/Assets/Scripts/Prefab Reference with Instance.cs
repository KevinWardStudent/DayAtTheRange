using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabReferencewithInstance : MonoBehaviour {


    TestPlayerMover player;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        /*
      if (player == null)
      {
          return;
      }
      else
      {
          player.AmmoTextUpdate(ammoCount.ToString() + " / " + "Infinite");
      }
      */

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
