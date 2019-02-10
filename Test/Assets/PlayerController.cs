using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public Animator anim;
	private Rigidbody2D rb;
	public BoxCollider2D coll;
	public float accel;
	public float topSpeed = 5;
	public float jumpPower;
	public string controlAxis;
	public KeyCode rightBumpKey;
	public KeyCode leftBumbKey;
	public KeyCode jumpKey;

	public LayerMask groundLayer;
	private bool grounded = true;
	private bool jumping = false;
	private bool bump = false;
	public float move;

	public float doubleClickThreshold;
	private float rightButtonCooler;
	private int rightButtonCount;
	private float leftButtonCooler;
	private int leftButtonCount;

	private int jumpCount = 0;


	void Start () {
		rb = GetComponent<Rigidbody2D> ();
	}

	void FixedUpdate(){
		if (move < 0 && rb.velocity.x > -topSpeed) {
			rb.AddForce (new Vector2 (move * accel, 0), ForceMode2D.Impulse);
		}
		if (move > 0 && rb.velocity.x < topSpeed){
			rb.AddForce (new Vector2 (move * accel, 0), ForceMode2D.Impulse);
		}
		if (jumping && grounded) {
			
			rb.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
		}
	}
	
	void Update () {
		jumping = Input.GetKey(jumpKey);
		grounded = IsGrounded ();
		if (Bumped (rightBumpKey)) {
			anim.SetTrigger("RightBump");
		}
		if (Bumped (leftBumbKey)) {
			anim.SetTrigger("LeftBump");
		}
		move = Input.GetAxis(controlAxis);
	}

	bool Bumped(KeyCode key){
		if (key == KeyCode.D) {
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
			return false;
		} else {
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
		}

	}

	bool IsGrounded() {
		Vector2 position = transform.position;
		Vector2 direction = Vector2.down;
		float distance = (coll.size.y / 2) + .04f;
		Debug.DrawRay(position, direction*distance, Color.green);
		RaycastHit2D hit = Physics2D.Raycast(position, direction, distance, groundLayer);
		if (hit.collider != null) {
			return true;
		}

		return false;
	}
		
}
