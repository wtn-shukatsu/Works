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
            //Destroy(gameObject);
            gameObject.SetActive(false);
            Destroy(other.gameObject);
            Destroy(shotToStart);
            Debug.Log("before invoke");
            Invoke("ChangeScene", 1.5f);
            Debug.Log("after invoke");
        }
    }

    void ChangeScene() {
        Debug.Log("before load scene");
        SceneManager.LoadScene("Main");
        Debug.Log("after load scene");
    }
}