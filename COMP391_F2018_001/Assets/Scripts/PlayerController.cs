using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Boundary
{
    public float xMin, xMax, yMin, yMax;
}

public class PlayerController : MonoBehaviour {
    
    // Variable Declarations
    [Header("Movement Settings")]
    public float speed = 5.0f;
    // public float xMin, xMax, yMin, yMax;
    public Boundary boundary;

    [Header("Attack Settings")]
    public GameObject laser;        // Laser prefab
    public GameObject laserSpawn;   // Laser spawn location
    public float fireRate = 0.5f;   // Firing speed

    private Rigidbody2D rBody;
    private float myTime = 0.0f;    // fireRate counter

	// Use this for initialization
	void Start () {
        rBody = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
        // Debug.Log(Time.deltaTime);
        myTime += Time.deltaTime; // DeltaTime represents the amount of time elapsed since the last Update() call.
                                  // Debug.Log(myTime);

        if (Input.GetButton("Fire1") && myTime > fireRate)
        {
            Instantiate(laser, laserSpawn.transform.position, laserSpawn.transform.rotation);
            myTime = 0.0f;
        }
	}

    // Fixed framerate update. Used mainly for physics. 
    void FixedUpdate()
    {
        // Get input
        float horiz = Input.GetAxis("Horizontal");
        float vert = Input.GetAxis("Vertical");
        // Debug.Log("x: " + horiz + ", y: " + vert);

        Vector2 movement = new Vector2(horiz, vert);

        // Player movement code
        // Rigidbody2D rBody = GetComponent<Rigidbody2D>();
        rBody.velocity = movement * speed;

        // Restrict our player movement
        rBody.position = new Vector2(
            Mathf.Clamp(rBody.position.x, boundary.xMin, boundary.xMax), // Restricts X to xMin and xMax
            Mathf.Clamp(rBody.position.y, boundary.yMin, boundary.yMax)); // Restrict Y to yMin and yMax
    }
}
