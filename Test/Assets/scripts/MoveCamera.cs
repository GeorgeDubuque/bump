using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour {

	private GameManager gm;

	public GameObject[] players;
	private float origZ;
	private Camera cam;
	private bool player1Vis = true;
	private bool player2Vis = true;
	public RectTransform viewRect;
	private float startSize;

	void Start(){
		gm = FindObjectOfType<GameManager> ();
		cam = GetComponent<Camera> ();
		//SetRect ();
		startSize = cam.orthographicSize;
		origZ = transform.position.z;
	}

	private void Update() {
		//SetRect ();
		SetCameraPos();
	}

	void SetCameraPos() {
		Vector3 middle = Vector3.zero;
		int numPlayers = 0;
		for (int i = 0; i < players.Length; ++i) {
			if (players [i] == null) {
				continue; //skip, since player is deleted
			}

			middle += players [i].transform.position;
			numPlayers++;
		}//end for every player
			
		if (!gm.roundOver) {
			float playerDistance = Vector2.Distance(players[0].transform.position, players[1].transform.position);
			if (playerDistance > 20) {
				float targetSize = (playerDistance + 5) * ((float)Screen.height / (float)Screen.width) * 0.5f;
//				cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, targetSize, .1f);
				cam.orthographicSize = targetSize;
            } else {
                cam.orthographicSize = startSize;
            }
		}

		//take average:
		middle /= numPlayers;
		cam.transform.position = Vector3.Lerp(cam.transform.position, new Vector3(middle.x, middle.y, origZ), .1f);
	}
}
