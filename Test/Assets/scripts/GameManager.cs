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
    public float moveDownAccel;
    public float moveDownTopSpeed;
	public float accel;
	public float doubleClickThreshold;
	public float bumpPower;
	public int numJumps;
    public float maxCharge = 2;
    public float chargeRate = .1f;
	public GameObject winnerText;
	public GameObject winText;

	public Vector3 spawn;

	public GameObject[] p1Lives;
	private int p1CurrLife;
	public GameObject[] p2Lives;
	private int p2CurrLife;
	public bool roundOver = false;

    public AudioSource deathSound;

	void Start(){
		p1CurrLife = p1Lives.Length - 1;
		p2CurrLife = p2Lives.Length - 1;
	}

	void Update(){
		//if (!roundOver) {
		//	IsRoundOver ();
		//} else {

		//}
	}

	void IsRoundOver(){

		if (!levelBounds.rect.Contains (players [0].transform.position)) {
			if (p1CurrLife == 0) {
				//SceneManager.LoadScene (SceneManager.GetActiveScene ().name);
				Destroy (players [0]);
//				Time.timeScale = .5f;
				roundOver = true;
                StartCoroutine(DisplayWinnerText(2));
            }
            else {
                deathSound.Play();
                players[0].GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                players [0].transform.position = spawn;
			}
			p1Lives [p1CurrLife].SetActive (false);
			p1CurrLife--;

		}else if (!levelBounds.rect.Contains (players [1].transform.position)) {
			if (p2CurrLife == 0) {
				//SceneManager.LoadScene (SceneManager.GetActiveScene ().name);
				Destroy (players [1]);
				//Time.timeScale = .5f;
				roundOver = true;
				StartCoroutine(DisplayWinnerText(1));
			} else {
                deathSound.Play();
                players[1].GetComponent<Rigidbody2D>().velocity = Vector2.zero;
				players [1].transform.position = spawn;
			}
			p2Lives [p2CurrLife].SetActive (false);
			p2CurrLife--;
		}

	}

	IEnumerator DisplayWinnerText(int winner){
        winnerText.SetActive(true);
        TextMeshProUGUI winnerMesh = winnerText.GetComponent<TextMeshProUGUI> ();
		if (winner == 1) {
			winnerMesh.text = "orange";
            winnerMesh.color = p1Color;
		} else {
			winnerMesh.text = "green";
            winnerMesh.color = p2Color;
		}
        yield return new WaitForSeconds(1);

        StartCoroutine(DisplayWinText());
		
	}

    public IEnumerator DisplayWinText(){
        winText.SetActive(true);
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

    }


    private void OnTriggerExit2D ( Collider2D collision ) {
        if (collision.gameObject.name == "Player1") {
            if (p1CurrLife == 0) {
                //SceneManager.LoadScene (SceneManager.GetActiveScene ().name);
                Destroy(players[0]);
                //				Time.timeScale = .5f;
                roundOver = true;
                StartCoroutine(DisplayWinnerText(2));
            } else {
                deathSound.Play();
                players[0].GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                players[0].transform.position = spawn;
            }
            p1Lives[p1CurrLife].SetActive(false);
            p1CurrLife--;

        } else if (collision.gameObject.name == "Player2") {
            if (p2CurrLife == 0) {
                //SceneManager.LoadScene (SceneManager.GetActiveScene ().name);
                Destroy(players[1]);
                //Time.timeScale = .5f;
                roundOver = true;
                StartCoroutine(DisplayWinnerText(1));
            } else {
                deathSound.Play();
                players[1].GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                players[1].transform.position = spawn;
            }
            p2Lives[p2CurrLife].SetActive(false);
            p2CurrLife--;
        }
    }
}
