using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    // Variable Declarations
    public float speed = 5.0f;
    public float xMin, xMax, yMin, yMax;

    private Rigidbody2D rBody;

	// Use this for initialization
	void Start () {
        rBody = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per framep
	void Update () {
		
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
            Mathf.Clamp(rBody.position.x, xMin, xMax), // Restricts X to xMin and xMax
            Mathf.Clamp(rBody.position.y, yMin, yMax)); // Restrict Y to yMin and yMax
    }
}
