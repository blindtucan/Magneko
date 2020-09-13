using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneFade : MonoBehaviour
{
    
    public Image fade;
    public GameObject leftSlide;
    public GameObject rightSlide;
    
    private Animator faderAnimator;
    private Animator leftAnimator;
    private Animator rightAnimator;
    
    private int sceneNumber;

    void Start() {
        sceneNumber = SceneManager.GetActiveScene().buildIndex;
        faderAnimator = fade.GetComponent<Animator>();
        leftAnimator = leftSlide.GetComponent<Animator>();
        rightAnimator = rightSlide.GetComponent<Animator>();

        // UPDATE CHECK INTS WITH BUILD INDEX FOR CUTSCENES!
        if(sceneNumber == 33) {
            Invoke("TransitionToLevel", 16.0f);
        }
        if(sceneNumber == 34) {
            Invoke("TransitionToLevel", 6.5f);
        }
        // Debug.Log("Scene Number: " + sceneNumber.ToString());
        if(sceneNumber == 35) {
            Invoke("TransitionToLevel", 7.5f);
        }
    }

    public void TransitionToLevel() {
        leftAnimator.SetBool("Closed", true);
        rightAnimator.SetBool("Closed", true);
        faderAnimator.SetTrigger("FadeOut");
    }

    public void OnFadeComplete() {
        sceneNumber = SceneManager.GetActiveScene().buildIndex;
        // Debug.Log("In OnFadeComplete()");
        // Debug.Log("Scene Number: " + sceneNumber.ToString());
        if(sceneNumber == 33){
            SceneManager.LoadScene("Tutorial01");
        }
        else if(sceneNumber==34){
            SceneManager.LoadScene("Magma0.5");
        }        
        else if(sceneNumber == 35) {
            SceneManager.LoadScene("MainMenu");
        }
    }
}
