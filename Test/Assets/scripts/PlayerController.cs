using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	//Movement parameters
	public float move;
    private bool moveDown;
	private float accel;
    private float moveDownAccel;
    private float moveDownTopSpeed;
	private float topSpeed = 5;
	private float jumpPower;
	private Rigidbody2D rb;

	public Animator anim;

	//Ground Detection
	public BoxCollider2D coll;
	public LayerMask groundLayer;
	private bool grounded = true;

	//Jumping
	private bool jumping = false;

	//Bump Attack
	private bool bump = false;

	//Control parameters for player
	public string controlAxis;
	public KeyCode rightBumpKey;
	public KeyCode leftBumbKey;
	public KeyCode jumpKey;
    public KeyCode downKey;



	//Double tap parameters
	public float doubleClickThreshold;
	private float rightButtonCooler;
	private int rightButtonCount;
	private float leftButtonCooler;
	private int leftButtonCount;

	private int numJumps;
	private int jumpCount = 0;

	private GameManager gm;


	void Start () {
		gm = FindObjectOfType<GameManager> ();
		rb = GetComponent<Rigidbody2D> ();
		accel = gm.accel;
		topSpeed = gm.topSpeed;
		jumpPower = gm.jumpPower;
		doubleClickThreshold = gm.doubleClickThreshold;
		numJumps = gm.numJumps;
        moveDownAccel = gm.moveDownAccel;
        moveDownTopSpeed = gm.moveDownTopSpeed;
	}

	void FixedUpdate(){
		//limits player speed to topspeed
		if (move < 0 && rb.velocity.x > -topSpeed) {
			rb.AddForce (new Vector2 (move * accel, 0), ForceMode2D.Impulse);
		}
		if (move > 0 && rb.velocity.x < topSpeed){
			rb.AddForce (new Vector2 (move * accel, 0), ForceMode2D.Impulse);
		}
        Debug.Log("moveDown == true: " + moveDown);
        Debug.Log("!grounded: " + !grounded);
        Debug.Log("rb.velocity.y: " + rb.velocity.y);
        if (moveDown && !grounded && rb.velocity.y > -moveDownTopSpeed)
        {
            Debug.Log("moving down");
            rb.AddForce(Vector2.down * (moveDownAccel), ForceMode2D.Impulse);
        }

        //does the player want to jump and have jumps left
        if (jumping && jumpCount < numJumps) {

			jumpCount++;
			rb.velocity = new Vector2 (rb.velocity.x, 0);
			rb.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
		}
	}
	
	void Update () {
		jumping = Input.GetKeyDown(jumpKey);
		grounded = IsGrounded();
        move = Input.GetAxis(controlAxis);
        moveDown = Input.GetKey(downKey);
        //Check for double click on bump keys
        if (Bumped (rightBumpKey)) {
			anim.SetTrigger("RightBump");
		}
		if (Bumped (leftBumbKey)) {
			anim.SetTrigger("LeftBump");
		}
		
	}

	/*
	 * Checks if user double clicked button
	 * Parameters:
	 * 	KeyCode key: key to check for double click
	 */
	bool Bumped(KeyCode key){
//		if (key == KeyCode.D) {
			if (Input.GetKeyDown (key)) {

				if (rightButtonCooler > 0 && rightButtonCount == 1/*Number of Taps you want Minus One*/) {
					return true;
				} else {
					rightButtonCooler = doubleClickThreshold; 
					rightButtonCount += 1;
				}
			}

			if (rightButtonCooler > 0) {
				rightButtonCooler -= 1 * Time.deltaTime;
			} else {
				rightButtonCount = 0;
			}
//		} else {
			if (Input.GetKeyDown (key)) {

				if (leftButtonCooler > 0 && leftButtonCount == 1/*Number of Taps you want Minus One*/) {
					return true;
				} else {
					leftButtonCooler = doubleClickThreshold; 
					leftButtonCount += 1;
				}
			}

			if (leftButtonCooler > 0) {
				leftButtonCooler -= 1 * Time.deltaTime;
			} else {
				leftButtonCount = 0;
			}
			return false;
//		}

	}


	/*
	 * Checks if gameobject is grounded 
	 */ 
	bool IsGrounded() {
		Vector2 position = transform.position;
		Vector2 direction = Vector2.down;
		float distance = (coll.size.y / 2) + .04f;
		Debug.DrawRay(position, direction*distance, Color.green);
		RaycastHit2D hit = Physics2D.Raycast(position, direction, distance, groundLayer);
		if (hit.collider != null) {
			jumpCount = 0;
            return true;
		}
        return false;
	}
		
}
