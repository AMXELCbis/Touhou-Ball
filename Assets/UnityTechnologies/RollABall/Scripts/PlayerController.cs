﻿using UnityEngine;

// Include the namespace required to use Unity UI
using UnityEngine.UI;

using System.Collections;

public class PlayerController : MonoBehaviour {

    // Create public variables for player speed, and for the Text UI game objects
    public float speed;
    public Text countText;
	public Text winText;
	public float maxSpeed;
	public bool checkRebornOnGround; //重生判断落地
	public float addForceScale = 1; //施加的移动力大小

    // Create private references to the rigidbody component on the player, and the count of pick up objects picked up so far
    private Rigidbody rb;
	private int count;
	[SerializeField]
	private LayerMask groundCheck;
	[SerializeField]
	private Transform groundCheckPoint;

	// At the start of the game..
	void Start ()
	{
		// Assign the Rigidbody component to our private rb variable
		rb = GetComponent<Rigidbody>();

		// Set the count to zero 
		count = 0;

		// Run the SetCountText function to update the UI (see below)
		SetCountText ();

		// Set the text property of our Win Text UI to an empty string, making the 'You Win' (game over message) blank
		winText.text = "";
	}

    // Each physics step..

	//Check the speed and the current direction, if not the same, increase the force
	//until current speed is equal to 0
    void Checkdirection(Vector3 movement, Vector3 currentSpeed, float AddedSpeed)
	{

		//x axis
		if ((currentSpeed.x > 0 && movement.x >=0 ) ||
            (currentSpeed.x < 0 && movement.x <= 0)) 
		{
            AddedSpeed = 1;
		}
		else
		{
           AddedSpeed = 50;
		}

        //z axis
        if ((currentSpeed.z > 0 && movement.z >= 0) ||
			(currentSpeed.z < 0 && movement.z <= 0))
        {
            AddedSpeed = 1;
        }
        else
        {
            AddedSpeed = 50;
        }

    }

    void FixedUpdate ()
	{
		// Set some local float variables equal to the value of our Horizontal and Vertical Inputs
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");
		Rigidbody rb = GetComponent<Rigidbody>();
        Vector3 currentSpeed = rb.velocity;
		//int addForce = 1;

		// Create a Vector3 variable, and assign X and Z to feature our horizontal and vertical float variables above
		Vector3 movement = moveVertical * LevelManager.instance.curForward + moveHorizontal * LevelManager.instance.curRight;
        //Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

		// check if wsad get pressed or not
        if (movement != Vector3.zero) 
			Checkdirection(movement, currentSpeed, addForceScale);


		// Add a physical force to our Player rigidbody using our 'movement' Vector3 above, 
		// multiplying it by 'speed' - our public player speed that appears in the inspector
		rb.AddForce (movement * speed * addForceScale);

		Vector2 XZvelocity = new Vector2(rb.velocity.x, rb.velocity.z);

        if (XZvelocity.magnitude > maxSpeed)
        {
            XZvelocity = XZvelocity.normalized * maxSpeed;
        }

        rb.velocity = new Vector3(XZvelocity.x, rb.velocity.y, XZvelocity.y);



        // display speed
        countText.text = "speed: " + currentSpeed.x.ToString() + "\t" + currentSpeed.z.ToString();


		CheckGroundPoint();
    }

    // When this game object intersects a collider with 'is trigger' checked, 
    // store a reference to that collider in a variable named 'other'..
    void OnTriggerEnter(Collider other) 
	{
		// ..and if the game object we intersect has the tag 'Pick Up' assigned to it..
		if (other.gameObject.CompareTag ("Pick Up"))
		{
			// Make the other game object (the pick up) inactive, to make it disappear
			other.gameObject.SetActive (false);

			// Add one to the score variable 'count'
			count = count + 1;

			// Run the 'SetCountText()' function (see below)
			SetCountText ();
		}
	}

	// Create a standalone function that can update the 'countText' UI and check if the required amount to win has been achieved
	void SetCountText()
	{
        Vector3 currentSpeed = GetComponent<Rigidbody>().velocity;

		// Check if our 'count' is equal to or exceeded 12
		if (count >= 12) 
		{
			// Set the text value of our 'winText'
			winText.text = "You Win!";
		}
	}

	public void ReBorn(Vector3 rebornPoint)
	{
		transform.position = rebornPoint;
		rb.velocity = Vector3.zero; //重置原有速度
		checkRebornOnGround = false;

    }

    public void CheckGroundPoint()
    {
		if (Physics.Raycast(transform.position, new Vector3(0, -1, 0), 1, groundCheck))
		{
			LevelManager.instance.lastDownPoint = groundCheckPoint.position;
        }
    }
    /// <summary>
    ///	根据是否要落地保持静止来控制是否需要开启地面碰撞检测
    /// </summary>
    /// <param name="collision"></param>
    //  private void OnCollisionEnter(Collision collision)
    //  {
    //      if(collision.gameObject.tag == "Ground")
    //{
    //	if(!checkRebornOnGround)
    //	{
    //              rb.velocity = Vector3.zero;
    //              checkRebornOnGround = true;
    //          }
    //}
    //  }
}