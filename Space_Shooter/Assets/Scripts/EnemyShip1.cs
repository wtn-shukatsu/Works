using UnityEngine;
using System.Collections;

public class EnemyShip1 : MonoBehaviour
{
	public float tilt;
	public float smoothing;
	public Vector2 startWait;
	public Vector2 maneuverTime;
	public Vector2 maneuverWait;
    public Boundary maxSpeed;
    public Boundary boundary;

    private float currentSpeedX;
    private float currentSpeedZ;
    private Rigidbody rb;
    private GameObject player;
    private Transform target;
    private bool aiming;

    void Start () {
        rb = GetComponent<Rigidbody>();
        currentSpeedZ = rb.velocity.z;
        currentSpeedX = rb.velocity.x;
        aiming = false;
        player = GameObject.FindGameObjectWithTag("Player");
        if (player != null) {
            target = player.transform;
            StartCoroutine(Evade());
        }
    }
	
	IEnumerator Evade () {
		yield return new WaitForSeconds (Random.Range (startWait.x, startWait.y));

        float aimingTime = Random.Range(maneuverTime.x, maneuverTime.y);
        while (true) {
            if (target != null && rb.position.z > target.position.z) {
                currentSpeedX = (target.position.x - transform.position.x) / aimingTime;
                currentSpeedX = Mathf.Clamp(currentSpeedX, maxSpeed.xMin, maxSpeed.xMax);
                currentSpeedZ = -currentSpeedZ / 4;
                aiming = true;
            }

            yield return new WaitForSeconds(aimingTime);

            if (aiming) {
                currentSpeedX = 0;
                currentSpeedZ = -currentSpeedZ * 4;
                aiming = false;
            }

            yield return new WaitForSeconds(Random.Range(maneuverWait.x, maneuverWait.y));
        }
    }
	
	void FixedUpdate () {
        rb.velocity = new Vector3(currentSpeedX, 0, currentSpeedZ);
        rb.position = new Vector3
                         (
                            Mathf.Clamp(rb.position.x, boundary.xMin, boundary.xMax),
                            0.0f,
                            Mathf.Clamp(rb.position.z, boundary.zMin, boundary.zMax)
                         );
        rb.rotation = Quaternion.Euler(0, 0, rb.velocity.x * (-tilt));
    }
}
