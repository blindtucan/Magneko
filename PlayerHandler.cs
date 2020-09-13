using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/*This script sets up the Variable CharDecider which determines which character is controlled by which controller. The buttons on the Character select scene 
each execute on of the two functions below.*/
public class PlayerHandler : MonoBehaviour
{
    public static int CharDecider;

    //vars for fade out
    public Image fade;
    private Animator faderAnimator;

    public void Start() {
        faderAnimator = fade.GetComponent<Animator>();
    }

    public void ChooseKnife() {
        CharDecider = 1;
    }

    public void ChooseFridge() {
        CharDecider = 2;
    }

    public void ExitGame() {
        Application.Quit();
    }

    /* THE FOLLOWING ARE FUNCTIONS FOR LOADING LEVELS IN LEVEL SELECT MENU*/

    public void LoadMainMenu(int LevelNumber) {
        SceneManager.LoadScene(0);
    }
 
    public void LoadLevelSelect() {
        SceneManager.LoadScene("LevelSelect");
    }

    //Load levels
    public void FadeAndLoad(int LevelNumber) {
        faderAnimator.SetTrigger("FadeOut");
        if(LevelNumber == 33) {
            //start cutsenece/game
            Invoke("LoadCutscene1",0.95f);
        }
        else if(LevelNumber == 0) {
            Invoke("LoadLevelSelect",0.95f);
        }
        else if(LevelNumber == 3) {
            Invoke("LoadLab",0.95f);
        }
        else if(LevelNumber == 11) {
            Invoke("LoadMagma", 0.95f);
        }
        else if(LevelNumber == 23) {
            Invoke("LoadMineshaft", 0.95f);
        }
        else if(LevelNumber == 36) {
            Invoke("LoadSewer", 0.95f);
        }
    }

    public void LoadCutscene1() {
        SceneManager.LoadScene(33);     //MAKE SURE THIS MATCHES BUILD INDEX FOR CUTSCENE 1
    }

    // Used for level select
    public void LoadLab() {
        SceneManager.LoadScene("Tutorial01");
    }
    public void LoadMagma() {
        SceneManager.LoadScene("Magma0.5");
    }
    public void LoadMineshaft() {
        SceneManager.LoadScene("Mineshaft01");
    }
    public void LoadSewer() {
        SceneManager.LoadScene(36);
    }
}
