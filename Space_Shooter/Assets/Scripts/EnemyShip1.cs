using UnityEngine;
using System.Collections;

public class EnemyShip1 : MonoBehaviour
{
    [SerializeField] float tilt;
    [SerializeField] float smoothing;
    [SerializeField] Vector2 startWait;
    [SerializeField] Vector2 maneuverTime;
    [SerializeField] Vector2 maneuverWait;
    [SerializeField] Boundary maxSpeed;
    [SerializeField] Boundary boundary;
    [SerializeField] EnemyWeapon enemyWeapon;

    float positionDeltaX;
    float positionDeltaZ;
    float currentSpeedX;
    float currentSpeedZ;
    Rigidbody rb;
    GameObject player;
    Transform target;
    bool aiming;

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
                enemyWeapon.Fire();
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
