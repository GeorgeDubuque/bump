using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;


/*
 * Class for storing common settings for both players
 */ 
public class GameManager : MonoBehaviour {

	public GameObject[] players;
    public Color p1Color;
    public Color p2Color;
	public RectTransform levelBounds;
	public float jumpPower;
	public float topSpeed;
	public float accel;
	public float doubleClickThreshold;
	public float bumpPower;
	public int numJumps;
	public GameObject winnerText;
	public GameObject winText;

	public Vector3 spawn;

	public GameObject[] p1Lives;
	private int p1CurrLife;
	public GameObject[] p2Lives;
	private int p2CurrLife;
	public bool roundOver = false;

	void Start(){
		p1CurrLife = p1Lives.Length - 1;
		p2CurrLife = p2Lives.Length - 1;
	}

	void Update(){
		if (!roundOver) {
			IsRoundOver ();
		} else {

		}
	}

	void IsRoundOver(){

		if (!levelBounds.rect.Contains (players [0].transform.position)) {
			if (p1CurrLife == 0) {
				Debug.Log ("Blue Wins!");
				//SceneManager.LoadScene (SceneManager.GetActiveScene ().name);
				Destroy (players [0]);
//				Time.timeScale = .5f;
				roundOver = true;
				DisplayWinnerText (2);
			} else {
				players [0].transform.position = spawn;
			}
			p1Lives [p1CurrLife].SetActive (false);
			p1CurrLife--;

		}else if (!levelBounds.rect.Contains (players [1].transform.position)) {
			if (p2CurrLife == 0) {
				Debug.Log ("Orange Wins!");
				//SceneManager.LoadScene (SceneManager.GetActiveScene ().name);
				Destroy (players [1]);
				//Time.timeScale = .5f;
				roundOver = true;
				DisplayWinnerText (1);
			} else {
				players [1].transform.position = spawn;
			}
			p2Lives [p2CurrLife].SetActive (false);
			p2CurrLife--;
		}

	}

	void DisplayWinnerText(int winner){
        winnerText.SetActive(true);
        TextMeshProUGUI winnerMesh = winnerText.GetComponent<TextMeshProUGUI> ();
		if (winner == 1) {
			winnerMesh.text = "orange";
            winnerMesh.color = p1Color;
		} else {
			winnerMesh.text = "blue";
            winnerMesh.color = p2Color;
		}

		
	}

    public void DisplayWinText(){
        winText.SetActive(true);
    }
}
