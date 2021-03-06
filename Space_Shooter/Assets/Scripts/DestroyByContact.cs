using UnityEngine;
using System.Collections;

public class DestroyByContact : MonoBehaviour
{
	public GameObject explosion;
	public GameObject playerExplosion;
	public int scoreValue;
    public int health;

    private GameController gameController;

    void Start () {
        GameObject gameControllerObject = GameObject.FindGameObjectWithTag ("GameController");
		if (gameControllerObject != null) {
			gameController = gameControllerObject.GetComponent <GameController>();
		}
		if (gameController == null) {
			Debug.Log ("Cannot find 'GameController' script");
		}
	}

	void OnTriggerEnter (Collider other) {
        if (other.CompareTag("Shot")) {
            Destroy(other.gameObject);
            health--;
        }
        else if (other.CompareTag("EnemyShotGravity") && transform.tag.Equals("Asteroid")) {
            Instantiate(explosion, transform.position, transform.rotation);
            Destroy(gameObject);
            return;
        }
        else if (other.CompareTag("Player")) {
            Instantiate(playerExplosion, other.transform.position, other.transform.rotation);
            Destroy(other.gameObject);
            Destroy(gameObject);
            gameController.GameOver();
        }
        else {
            return;
        }

        if (explosion != null && health <= 0) {
            Instantiate(explosion, transform.position, transform.rotation);
            Destroy(gameObject);
            gameController.AddScore(scoreValue);
        }
	}
}