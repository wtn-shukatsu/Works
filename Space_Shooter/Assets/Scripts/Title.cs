using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Title : MonoBehaviour
{
    public GameObject explosion;
    public GameObject shotToStart;
    public FadePanel fadePanel;
    public Text titleText;
    public BoxCollider titleCollider;

    void Start() {
        if (explosion == null) {
            Debug.Log("undefined gameobject: explosion");
        }
    }

    void OnTriggerEnter(Collider other) {
        if ( other.CompareTag("Shot") ) {
            Instantiate(explosion, transform.position, transform.rotation);
            Destroy(other.gameObject);
            titleText.enabled = false;
            titleCollider.enabled = false;
            shotToStart.SetActive(false);

            StartCoroutine(ChangeSceneToMain());
        }
    }

    IEnumerator ChangeSceneToMain() {
        yield return new WaitForSeconds(1.5f);

        fadePanel.FadeOut();

        yield return new WaitForSeconds(1.5f);

        SceneManager.LoadScene("Main");
    }
}