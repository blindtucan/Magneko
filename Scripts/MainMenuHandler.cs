using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuHandler : MonoBehaviour
{
    public Button startBtn;
    public Button levelBtn;
    public Button controllerBtn;
    public Button exitBtn;

     // To toggle the image
    private GameObject controlsImg;
    private GameObject controlsText;
    private Navigation customNav;
    
    private bool canGoBack;
    private bool showControl;
    private bool hideControl;  

    // Start is called before the first frame update
    void Start()
    {
        controlsImg = GameObject.Find("ControlsImage");
        controlsText = GameObject.Find("ControlsText");   
        controlsImg.SetActive(false);
        controlsText.SetActive(false);
        showControl = false;
        hideControl = false;
        canGoBack = true;
    }

    // Update is called once per frame
    void Update()
    {
        // Allows player to click any button to close control "menu"
        if (hideControl) {
            if (Input.anyKeyDown){
            // if (Input.GetButtonDown("Circle"+1) || Input.GetButtonDown("Circle"+1)) {
                //Debug.Log("Exiting");
                SoundManagerScript.PlaySound("MenuPress");
                showControl = false;
                hideControl = false;
                // Restart coroutine so it checks to hide/reset UI elements
                StartCoroutine(ControlsCoroutine());
            }
        }
        // implementation of back button (gose to main character select)
        else if (canGoBack) {
            if (Input.GetButtonDown("Circle"+1) || Input.GetButtonDown("Circle"+2)) {
                SceneManager.LoadScene("CharacterSelect");
            }
        } 
    }

    // Helper function for ControlsCoroutine: toggles a boolean on player click
    public void ToggleControls() {
        //Debug.Log("Toggling");
        if (!showControl) {showControl = true;}
        else {showControl = false;}
        // showControl = true;
        StartCoroutine(ControlsCoroutine());
    }

    // Triggered when "CONTROLS' button is clicked, blocks players from selected other buttons when looking at controls
    public IEnumerator ControlsCoroutine() {
        
        // need to disable horizontal movement of selection
        
        //Debug.Log("Opening controller menu (ControlsCoroutine())");
        if (showControl) {
            // Show the image and text
            controlsImg.SetActive(true);
            controlsText.SetActive(true);

            // Set all the buttons except controller button to navigation mode "None" so 
            // the the user cannot move and select another button when in this image is being showed.
            customNav.mode = Navigation.Mode.None;
            
         yield return new WaitForSeconds(0.2f);
          //Debug.Log("Press to exit");
          hideControl = true;
          canGoBack = false;
        }
        else {
            // Debug.Log("in the else loop");
            // Hide the image and text
            controlsImg.SetActive(false);
            controlsText.SetActive(false);
            yield return new WaitForSeconds(0.2f);
            canGoBack = true;

            // re-enable navigation mode;
            customNav.mode = Navigation.Mode.Horizontal;
        }
        // Apply navigation mode changes
        startBtn.navigation = customNav;
        levelBtn.navigation = customNav;
        exitBtn.navigation = customNav;
        yield return null;
    }
}