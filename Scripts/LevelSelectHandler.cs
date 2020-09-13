using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelectHandler : MonoBehaviour
{
    // Update is called once per frame
    void Update() {
    // implementation of back button (goes to main menu)
    if (Input.GetButton("Circle"+1) || Input.GetButton("Circle"+2)) {
        SceneManager.LoadScene("MainMenu");
        }
    }
}
