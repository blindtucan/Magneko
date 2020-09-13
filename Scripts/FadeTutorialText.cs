using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeTutorialText : MonoBehaviour
{
    // Set in inspector
    public Text[] texts;
    public Image[] images;
    public float fadeTimer = 8.5f;
    private bool fade = false;
    private float fadeTime = 0.0f;

    // Update is called once per frame
    void Update()
    {
        fadeTime+=Time.deltaTime;
        if(fadeTime > fadeTimer && fade==false) {
            //fade the things
            Debug.Log("fading all instructions");
            // Fade text
            for (int i=0; i<texts.Length; i++) {
                StartCoroutine(FadeTextToZeroAlpha(1.0f, texts[i]));
            }

            // Fade images
            for (int i=0; i<images.Length; i++) {
                StartCoroutine(FadeImageToZeroAlpha(1.0f, images[i]));
            }
        }
    }

    public IEnumerator FadeImageToZeroAlpha(float t, Image img) {
        img.color = new Color(img.color.r, img.color.g, img.color.b, 1);
        while (img.color.a > 0.0f) {
            img.color = new Color(img.color.r, img.color.g, img.color.b, img.color.a - (Time.deltaTime / t));
            yield return null;
        }
    }

    public IEnumerator FadeTextToZeroAlpha(float t, Text i) {
        i.color = new Color(i.color.r, i.color.g, i.color.b, 1);
        //yield return new WaitForSeconds(0.2f);
        while (i.color.a > 0.0f)
        {
            i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a - (Time.deltaTime / t));
            yield return null;
        }
        fade = true;
    }
}
