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
    public EnemyWeapon1 enemyWeapon1;

    private float positionDeltaX;
    private float positionDeltaZ;
    private float currentSpeedX;
    private float currentSpeedZ;
    private Rigidbody rb;
    private GameObject player;
    private Transform target;
    private bool aiming;

    void Start () {
        rb = GetComponent<Rigidbody>();
        currentSpeedX = rb.velocity.x;
        currentSpeedZ = rb.velocity.z;
        aiming = false;
        player = GameObject.FindGameObjectWithTag("Player");
        if (player != null) {
            target = player.transform;
            StartCoroutine(Aim());
        }
    }
	
	IEnumerator Aim() {
		yield return new WaitForSeconds (Random.Range(startWait.x, startWait.y));

        float aimingTime = Random.Range(maneuverTime.x, maneuverTime.y);
        while (target != null) {
            if (positionDeltaZ > 0) {
                currentSpeedX = Mathf.Clamp(positionDeltaX / aimingTime, maxSpeed.xMin, maxSpeed.xMax);
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

    void Update() {
        if (target != null) {
            positionDeltaX = target.position.x - rb.position.x;
            positionDeltaZ = rb.position.z - target.position.z;
            if (positionDeltaZ > 0 && Mathf.Abs(positionDeltaX) <= 0.4f) {
                enemyWeapon1.Fire();
            }
        }
    }

    void FixedUpdate () {
        rb.velocity = new Vector3(currentSpeedX, 0, currentSpeedZ);
        rb.position = new Vector3 (
                            Mathf.Clamp(rb.position.x, boundary.xMin, boundary.xMax),
                            0.0f,
                            Mathf.Clamp(rb.position.z, boundary.zMin, boundary.zMax)
                          );
        rb.rotation = Quaternion.Euler(0, 0, rb.velocity.x * (-tilt));
    }
}
