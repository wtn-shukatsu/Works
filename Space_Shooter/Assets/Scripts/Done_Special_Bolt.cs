using UnityEngine;
using System.Collections;

public class Done_Special_Bolt : MonoBehaviour
{
    public float gravity;

    private GameObject player;
    private float distance;
    private Vector3 angle;

    void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void FixedUpdate () {
        transform.RotateAround(transform.position, Vector3.up, 100.0f);

        if (player) {
            distance = Vector3.Distance(transform.position, player.transform.position);
            angle = transform.position - player.transform.position;
            player.GetComponent<Rigidbody>().AddForce(angle.normalized * (gravity / Mathf.Pow(distance, 2)));
        }
    }
}
