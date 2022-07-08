using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Title : MonoBehaviour
{
    public GameObject explosion;
    public GameObject shotToStart;

    void Start() {
        if (explosion == null) {
            Debug.Log("undefined gameobject: explosion");
        }
    }

    void OnTriggerEnter(Collider other) {
        if ( other.CompareTag("Shot") ) {
            Instantiate(explosion, transform.position, transform.rotation);
            Destroy(other.gameObject);
            gameObject.SetActive(false);
            shotToStart.SetActive(false);
            Invoke("ChangeScene", 1.5f);
        }
    }

    void ChangeScene() {
        SceneManager.LoadScene("Main");
    }
}