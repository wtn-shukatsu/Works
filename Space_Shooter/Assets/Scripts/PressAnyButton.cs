using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PressAnyButton : MonoBehaviour
{
    [SerializeField] float speed = 1.0f;

    Text text;
    float time;
    AudioSource audioSource;

    void Start() {
        text = GetComponent<Text>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update() {
        if (Input.anyKeyDown) {
            speed = 8;
            audioSource.Play();
            Invoke("ChangeScene", 1.5f);
        }
        text.color = GetAlphaColor(text.color);
    }

    // AlphaílÇçXêV
    Color GetAlphaColor(Color _color) {
        time += Time.deltaTime * 5.0f * speed;

        // color.a ÇÕ 0(0%) Å` 1(100%) 
        _color.a = Mathf.Sin(time);

        return _color;
    }

    void ChangeScene() {
        SceneManager.LoadScene("Main");
    }
}
