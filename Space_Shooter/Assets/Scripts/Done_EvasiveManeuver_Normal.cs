using UnityEngine;
using System.Collections;

public class Done_EvasiveManeuver_Normal : MonoBehaviour
{
	public float tilt;
	public float smoothing;
	public Vector2 startWait;
	public Vector2 maneuverTime;
	public Vector2 maneuverWait;
    public Done_Boundary boundary;

    private float currentSpeed;
	private float targetManeuver;
    private Rigidbody rb;
    private Transform target;
    private bool evade = false;

    void Start ()
	{
        rb = GetComponent<Rigidbody>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
        currentSpeed = rb.velocity.z;
		StartCoroutine(Evade());
	}
	
	IEnumerator Evade ()
	{
		yield return new WaitForSeconds (Random.Range (startWait.x, startWait.y));
		while (true)
		{
			targetManeuver = target.position.x;
            if (rb.position.z > target.position.z) {
                currentSpeed = -currentSpeed / 4;
                evade = true;
            }
            yield return new WaitForSeconds (Random.Range (maneuverTime.x, maneuverTime.y));
			targetManeuver = 0;
            if (evade) {
                currentSpeed = -currentSpeed * 4;
                evade = false;
            }
            yield return new WaitForSeconds (Random.Range (maneuverWait.x, maneuverWait.y));
		}
	}
	
	void FixedUpdate ()
	{
		float newManeuver = Mathf.MoveTowards (targetManeuver, rb.position.x, smoothing * Time.deltaTime);
		rb.velocity = new Vector3 (newManeuver, 0.0f, currentSpeed);
		rb.position = new Vector3
		(
			Mathf.Clamp(rb.position.x, boundary.xMin, boundary.xMax), 
			0.0f, 
			Mathf.Clamp(rb.position.z, boundary.zMin, boundary.zMax)
		);
		
		rb.rotation = Quaternion.Euler (0, 0, rb.velocity.x * -tilt);
	}
}
