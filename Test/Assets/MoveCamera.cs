using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour {
	public Transform[] players;
	private float origZ;

	void Start(){
		origZ = transform.position.z;
	}

	private void Update() {
		SetCameraPos();
	}

	void SetCameraPos() {
		Vector3 middle = Vector3.zero;
		int numPlayers = 0;

		for(int i=0; i<players.Length; ++i) {
			if(players[i] ==null){
				continue; //skip, since player is deleted
			}
			middle += players[i].position;
			numPlayers++;
		}//end for every player

		//take average:
		middle /= numPlayers;
		Camera cam = GetComponent<Camera>();
		cam.transform.position = new Vector3(middle.x, cam.transform.position.y, origZ);
	}


}
