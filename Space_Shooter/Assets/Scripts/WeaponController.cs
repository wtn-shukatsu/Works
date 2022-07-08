using UnityEngine;
using System.Collections;

public class WeaponController : MonoBehaviour
{
	public GameObject shot;
	public Transform shotSpawn;
	public float fireRate;
	public float delay;

    private AudioSource audioSource;
    private GameController gameController;

	void Start () {
        GameObject gameControllerObject = GameObject.FindGameObjectWithTag("GameController");
        if (gameControllerObject != null) {
            gameController = gameControllerObject.GetComponent<GameController>();
        }
        if (gameController == null) {
            Debug.Log("Cannot find 'GameController' script");
        }
        if (!gameController.gameOver) {
            audioSource = GetComponent<AudioSource>();
            InvokeRepeating("Fire", delay, fireRate);
        }
	}

	void Fire () {
		Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
        audioSource.Play();
	}
}
