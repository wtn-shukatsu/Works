﻿using UnityEngine;
using System.Collections;

[System.Serializable]
public class Done_Boundary 
{
	public float xMin, xMax, zMin, zMax;
}

public class Done_PlayerController : MonoBehaviour
{
	public float speed;
	public float tilt;
	public Done_Boundary boundary;

	public GameObject shot;
	public Transform[] shotSpawns;
	public float fireRate;
	 
	private float nextFire;
    private Rigidbody rb;
    private AudioSource audioSource;
	
    void Start() {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

	void Update () {
		if (Input.GetButton("Fire1") && Time.time > nextFire) {
			nextFire = Time.time + fireRate;
            foreach (var shotSpawn in shotSpawns) {
                Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
            }
            audioSource.Play ();
		}
	}

	void FixedUpdate () {
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");

		Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);
		rb.velocity = movement * speed;
		
		rb.position = new Vector3
					  (
						  Mathf.Clamp (rb.position.x, boundary.xMin, boundary.xMax), 
						  0.0f, 
						  Mathf.Clamp (rb.position.z, boundary.zMin, boundary.zMax)
					  );
		
		rb.rotation = Quaternion.Euler (0.0f, 0.0f, rb.velocity.x * -tilt);
	}
}
