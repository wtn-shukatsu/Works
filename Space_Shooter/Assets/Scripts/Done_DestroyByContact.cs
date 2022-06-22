using UnityEngine;
using System.Collections;

public class Done_DestroyByContact : MonoBehaviour
{
	public GameObject explosion;
	public GameObject playerExplosion;
	public int scoreValue;
    public int health;

    private Done_GameController gameController;

    void Start () {
        GameObject gameControllerObject = GameObject.FindGameObjectWithTag ("GameController");
		if (gameControllerObject != null) {
			gameController = gameControllerObject.GetComponent <Done_GameController>();
		}
		if (gameController == null) {
			Debug.Log ("Cannot find 'GameController' script");
		}
	}

	void OnTriggerEnter (Collider other) {
        if (other.CompareTag("Boundary") || other.CompareTag("Enemy")) {
            return;
        }

        if (other.CompareTag("Shot")) {
            Destroy(other.gameObject);
            health--;
        }

        if (other.CompareTag("EnemyShot") && transform.tag.Equals("Asteroid")) {
            Instantiate(explosion, transform.position, transform.rotation);
            Destroy(gameObject);
            return;
        }

        if (explosion != null && health <= 0) {
            Instantiate(explosion, transform.position, transform.rotation);
            Destroy(gameObject);
            Destroy(other.gameObject);
            gameController.AddScore(scoreValue);
        }

        if (other.CompareTag("Player")) {
			Instantiate(playerExplosion, other.transform.position, other.transform.rotation);
            Destroy(other.gameObject);
            Destroy(gameObject);
            gameController.GameOver();
		}
	}
}