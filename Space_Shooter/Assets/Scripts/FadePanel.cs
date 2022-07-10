using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadePanel : MonoBehaviour
{
    public Image fadePanel;

    private bool fadeIn;
    private bool fadeOut;

    void Start() {
        fadeIn = false;
        fadeOut = false;
    }

    void Update() {
        if (fadeIn) {
            if (fadePanel.color.a <= 0) {
                fadeIn = false;
                return;
            }
            fadePanel.color -= new Color(0, 0, 0, 0.01f);
        }
        else if (fadeOut) {
            if (fadePanel.color.a >= 1) {
                fadeOut = false;
                return;
            }
            fadePanel.color += new Color(0, 0, 0, 0.01f);
        }
        else { ; }
    }

    public void FadeIn() {
        fadeIn = true;
        fadePanel.color += new Color(0, 0, 0, 1);
    }

    public void FadeOut() {
        fadeOut = true;
        fadePanel.color -= new Color(0, 0, 0, 1);
    }
}
