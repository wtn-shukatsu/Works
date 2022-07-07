using UnityEngine;
using System.Collections;
using System;

public class Done_Asteroid : MonoBehaviour
{
    private float gravity;
    private GameObject[] EnemyShots;
    private GameObject EnemyBolt;
    private float distance;
    private Vector3 angle;

    void Update () {
        EnemyShots = GameObject.FindGameObjectsWithTag("EnemyShot");
        if (EnemyShots.Length > 0) {
            EnemyBolt = EnemyShots[0];
            distance = Vector3.Distance(EnemyBolt.transform.position, transform.position);
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
}
