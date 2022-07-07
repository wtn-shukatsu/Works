using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShotToStart : MonoBehaviour
{
    public float speed = 1.0f;

    Text text;
    float time;
    AudioSource audioSource;

    void Start() {
        text = GetComponent<Text>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update() {
        text.color = GetAlphaColor(text.color);
    }

    // AlphaílÇçXêV
    Color GetAlphaColor(Color _color) {
        time += Time.deltaTime * 5.0f * speed;

        // color.a ÇÕ 0(0%) Å` 1(100%) 
        _color.a = Mathf.Sin(time);

        return _color;
    }
}
