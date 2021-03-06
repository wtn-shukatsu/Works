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

    // Alpha値を更新
    Color GetAlphaColor(Color _color) {
        time += Time.deltaTime * 5.0f * speed;

        // color.a は 0(0%) 〜 1(100%) 
        _color.a = Mathf.Sin(time);

        return _color;
    }
}
