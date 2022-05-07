using UnityEngine;
using System.Collections;

public class Done_Asteroid : MonoBehaviour
{
    private float gravity;
    private GameObject[] EnemyShots;
    private GameObject EnemyBolt;
    private float distance;
    private Vector3 angle;

    void Start ()
	{

    }

    void Update ()
	{
        EnemyShots = GameObject.FindGameObjectsWithTag("EnemyShot");
        distance = Vector3.Distance(EnemyShots[0].transform.position, transform.position);
        EnemyBolt = EnemyShots[0];
        foreach (var EnemyShot in EnemyShots) {
            var d = Vector3.Distance(EnemyShot.transform.position, transform.position);
            if (distance > d) {
                distance = d;
                EnemyBolt = EnemyShot;
            }
        }
        gravity = EnemyBolt.GetComponent<Done_Special_Bolt>().gravity;
        angle = EnemyBolt.transform.position - transform.position;
        GetComponent<Rigidbody>().AddForce(angle.normalized * (gravity / Mathf.Pow(distance, 3)));
    }
}
