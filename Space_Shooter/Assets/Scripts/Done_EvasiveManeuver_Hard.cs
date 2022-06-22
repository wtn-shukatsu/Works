using UnityEngine;
using System.Collections;

public class Done_EvasiveManeuver_Hard : MonoBehaviour
{
	public Vector2 startWait;
    public Done_Boundary boundary;

    private Rigidbody rb;
    private float currentSpeed;
    private float rad;
    private int RightAround;

    void Start () {
        rad = 0.2f;
        rb = GetComponent<Rigidbody>();
        currentSpeed = rb.velocity.z;
        RightAround = Random.Range(0, 2);
        StartCoroutine(Evade());
	}
	
	IEnumerator Evade () {
		yield return new WaitForSeconds (Random.Range(startWait.x, startWait.y));
		while (true) {
            yield return new WaitForSeconds(Random.Range(1.0f, 1.0f));
        }
    }
	
	void FixedUpdate () {
        rb.velocity = new Vector3(rb.velocity.x, 0.0f, currentSpeed);
        rb.position = new Vector3
                      (
                          Mathf.Clamp(rb.position.x, boundary.xMin, boundary.xMax),
                          0.0f,
                          Mathf.Clamp(rb.position.z, boundary.zMin, boundary.zMax)
                      );
        if (RightAround == 0) {
            rb.position += new Vector3(rad * Mathf.Cos(Time.time * currentSpeed), 0.0f, rad * Mathf.Sin(Time.time * currentSpeed));
        }
        else {
            rb.position += new Vector3(rad * Mathf.Cos(Time.time * -currentSpeed), 0.0f, rad * Mathf.Sin(Time.time * -currentSpeed));
        }
    }
}
