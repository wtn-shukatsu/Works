using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeapon1 : MonoBehaviour
{
    public GameObject shot;
    public Transform shotSpawn;
    public float fireRate;
    
    private float nextFire;
    private AudioSource audioSource;
    private GameController gameController;

    void Start() {
        audioSource = GetComponent<AudioSource>();
        nextFire = Time.time + fireRate;
    }

    public void Fire() {
        if (Time.time > nextFire) {
            nextFire = Time.time + fireRate;
            Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
            audioSource.Play();
        }
    }
}
