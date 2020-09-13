using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadCutsceneOnCollision : MonoBehaviour
{
    //vars for fade out
    public Image fade;
    private Animator faderAnimator;

    public void Start() {
        faderAnimator = fade.GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D col) {
        if(col.tag=="Player"){
            faderAnimator.SetTrigger("FadeOut");
            Invoke("LoadCutscene2", 1.0f);
        }
    }

    private void LoadCutscene2() {
        SceneManager.LoadScene("Scene2_falling");
    }
}
