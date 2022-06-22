using UnityEngine;
using System.Collections;

public class Done_EvasiveManeuver_VeryHard : MonoBehaviour
{
    public float tilt;
    public Vector2 startWait;
    public Done_Boundary boundary;

    private float Xspeed;
    private Rigidbody rb;

    void Start () {
        rb = GetComponent<Rigidbody>();
        Xspeed = rb.velocity.x;
        StartCoroutine(Evade());
	}
	
	IEnumerator Evade () {
		yield return new WaitForSeconds (Random.Range(startWait.x, startWait.y));
		while (true) {
            yield return new WaitForSeconds(Random.Range(0.5f, 1.0f));
            Xspeed = 5;
        }
    }
	
	void FixedUpdate () {
        rb.velocity = new Vector3(Xspeed, 0.0f, rb.velocity.z);
        rb.position = new Vector3
                      (
                          Mathf.Clamp(rb.position.x, boundary.xMin, boundary.xMax),
                          0.0f,
                          Mathf.Clamp(rb.position.z, boundary.zMin, boundary.zMax)
                      );

        rb.rotation = Quaternion.Euler(0, 0, rb.velocity.x * -tilt);
    }
}
