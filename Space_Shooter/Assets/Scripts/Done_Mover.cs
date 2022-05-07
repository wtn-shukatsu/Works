using UnityEngine;
using System.Collections;

public class Done_Mover : MonoBehaviour
{
	public float speed;

    private Rigidbody rb;

	void Start ()
	{
        rb = GetComponent<Rigidbody>();
        rb.velocity = transform.forward * speed;
	}
}
