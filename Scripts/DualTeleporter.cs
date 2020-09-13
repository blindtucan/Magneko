using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DualTeleporter : MonoBehaviour
{
    public GameObject levelTransitionCanvas;
    public int checker = 0;
    public string newScene;
    Animator anim1;
    Animator anim2;
    private GameObject player1;
    private GameObject player2;
    public AudioSource audioSrc;
    public bool soundCheck;

    // Start is called before the first frame update
    void Awake()
    {
        audioSrc = GetComponent<AudioSource>();
    }
    void Start()
    {
        checker = 0;
        anim1 = levelTransitionCanvas.transform.GetChild(0).GetComponent<Animator>();
        anim2 = levelTransitionCanvas.transform.GetChild(1).GetComponent<Animator>();
        player1 = GameObject.Find("Player1");
        player2 = GameObject.Find("Player2");
        soundCheck = false;
    }

    // Update is called once per frame
    void Update()
    {
        // Debug.Log("checker: " + checker);
        
        if(checker >= 4){
            //trigger animations before changing scene
            // Debug.Log("About to load new scene...");
            player1.GetComponent<Player1Movement>().victory = true;
            player2.GetComponent<Player2Movement>().victory = true;
            Invoke("SuccessSound",0);
            StartCoroutine(StartLevelTransition());
            checker = 0;
        }
    }

    void SuccessSound()
    {
        soundCheck = true;
        if(soundCheck == true)
        {
            audioSrc.Play(0);
            soundCheck = false;
        }
        else
        {
            audioSrc.Stop();
        }
    }

    IEnumerator StartLevelTransition() {
        // Start the level transitions
        anim1.SetBool("Closed", true);
        anim2.SetBool("Closed", true);
        yield return new WaitForSeconds(1.2f);


        // Load the proper scene
        SceneManager.LoadScene(newScene);
    }
}
