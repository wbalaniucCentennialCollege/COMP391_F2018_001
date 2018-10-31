﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByContact : MonoBehaviour {

    public GameObject explosionAsteroid;
    public GameObject explosionPlayer;

	// Use this for initialization
	void Start () {
		
	}

    // This trigger will run code when another object with a collider whose Is Trigger? boolean is set to true, 
    // and collides with this object
    void OnTriggerEnter2D(Collider2D other)
    {
        // Ignore the boundary collider
        if(other.tag == "Boundary")
        {
            Debug.Log(other.gameObject.name + " Check");
            return;
        }

        if(other.tag == "Player")
        {
            Instantiate(explosionPlayer, other.transform.position, other.transform.rotation);
            // Trigger GameOver logic
        }

        // Create the asteroid explosion
        Instantiate(explosionAsteroid, this.transform.position, this.transform.rotation);

        Destroy(other.gameObject);
        Destroy(this.gameObject);
    }
}
