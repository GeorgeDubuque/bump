using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bump : MonoBehaviour {

	private GameManager gm;
	public Rigidbody2D rb;
	private float bumpPower = 5;
	private Animator anim;
	private bool bumpedLeft = false;
	private bool bumpedRight = false;
	private AudioSource audio;
	private float audioCool;
	public float audioCoolTime = 1;
	public PlayerController pc;
    public float charge;

	void Start(){
		gm = FindObjectOfType<GameManager> ();
		bumpPower = gm.bumpPower;
		anim = GetComponent<Animator> ();
		audio = GetComponent<AudioSource> ();
		audioCool = Time.time;
	}

	public void SetBumpedLeft(){
		bumpedLeft = !bumpedLeft;
	}

	public void SetBumpedRight(){
		bumpedRight = !bumpedRight;
	}

	void OnCollisionEnter2D(Collision2D col){
		GameObject gameObj = col.gameObject;
		AnimatorStateInfo info = anim.GetCurrentAnimatorStateInfo (0);
		if (gameObj.CompareTag("Player")) {
			if (pc.move != 0 && rb.velocity.magnitude > 2 && Time.time - audioCool > audioCoolTime) {
				audio.Play ();
				audioCool = Time.time;

			}
			if((bumpedLeft || bumpedRight)){
				Vector2 dir = Vector2.zero;
				if (bumpedLeft) {
					dir = Vector2.left;
				}
				if (bumpedRight) {
					dir = Vector2.right;
				}
                Debug.Log(bumpPower * pc.charge);
				gameObj.GetComponent<Rigidbody2D>().AddForce(dir * (bumpPower * pc.charge), ForceMode2D.Impulse);
                pc.charge = 1;
			}

		}
	}
}
