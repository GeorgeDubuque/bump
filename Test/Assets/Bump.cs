using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bump : MonoBehaviour {

	public Rigidbody2D rb;
	public float bumpPower = 5;
	private Animator anim;
	private bool bumpedLeft = false;
	private bool bumpedRight = false;

	void Start(){
		anim = GetComponent<Animator> ();
	}

	public void SetBumpedLeft(){
		Debug.Log ("bumpedLeft");
		bumpedLeft = !bumpedLeft;
	}

	public void SetBumpedRight(){
		bumpedRight = !bumpedRight;
	}

	void OnCollisionStay2D(Collision2D col){
		GameObject gameObj = col.gameObject;
		AnimatorStateInfo info = anim.GetCurrentAnimatorStateInfo (0);
		Debug.Log (info.IsTag("Attack"));
		Debug.Log (gameObj.name);
		if (gameObj.CompareTag("Player") && (bumpedLeft || bumpedRight)) {
			Debug.Log ("hit");
			Vector2 dir = Vector2.zero;
			if (bumpedLeft) {
				dir = Vector2.left;
			}
			if (bumpedRight) {
				dir = Vector2.right;
			}
			gameObj.GetComponent<Rigidbody2D>().AddForce(dir * bumpPower, ForceMode2D.Impulse);
		}
	}
}
