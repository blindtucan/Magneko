using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDeath : MonoBehaviour
{
    private Material defaultMat;
    private SpriteRenderer sr;
    private Vector3 deathPos;
    private bool spiked = false;
    private Vector3 p1Pos;
    private Vector3 p2Pos;
    private Animator anim1;
    private Animator anim2;
    private GameObject player1;
    private GameObject player2;

    public GameObject levelTransitionCanvas;
    public GameObject p1;
    public GameObject p2;
    public UnityEngine.Object explosionRef;

    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        defaultMat = sr.material;

        // Level transition
        anim1 = levelTransitionCanvas.transform.GetChild(0).GetComponent<Animator>();
        anim2 = levelTransitionCanvas.transform.GetChild(1).GetComponent<Animator>();
        player1 = GameObject.Find("Player1");
        player2 = GameObject.Find("Player2");
        // explosionRef = Resources.Load("SpikeHitParticle");  
    }

    // Update is called once per frame
    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Spikes"){
            Debug.Log("Dying...");   
            player1.GetComponent<Player1Movement>().MoveStop = true;
            player2.GetComponent<Player2Movement>().MoveStop = true;
            // sr.material = deathMat;
            // deathPos = collision.otherCollider.transform.position;
            // Invoke("ResetMaterial", 0.25f);
            RespawnLevel();
        }
    }

    void OnTriggerEnter2D(Collider2D collision){
        if(collision.gameObject.tag == "Spikes"){
            Debug.Log("Dying...");   
            player1.GetComponent<Player1Movement>().MoveStop = true;
            player2.GetComponent<Player2Movement>().MoveStop = true;
            // sr.material = deathMat;
            // deathPos = collision.otherCollider.transform.position;
            // Invoke("ResetMaterial", 0.25f);
            RespawnLevel();
        }
    }

    void Update() {
        if (spiked) {
            //lock player movement
            p1.transform.position = p1Pos;
            p2.transform.position = p2Pos;
        }
    }

    // Sets the player's sprite to transparent
    void FlashTransparent() {
        sr.color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
        Invoke("ResetMaterial", 0.1f);
    }

    // Sets the player's sprite back to normal
    void ResetMaterial() {
        sr.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
        Invoke("FlashTransparent", 0.1f);
    }

    // Triggers flashing and Death function
    IEnumerator StaggerRestart() {
        p1Pos = p1.transform.position;
        p2Pos = p2.transform.position;
        spiked = true;
        Debug.Log("Stagger Death");
        FlashTransparent();

        Debug.Log("Waiting");
        
        yield return new WaitForSeconds(0.2f);
        StartCoroutine("ResetLevelTransition");
    }

    IEnumerator ResetLevelTransition() {
        // Start the level transitions
        anim1.SetBool("Closed", true);
        anim2.SetBool("Closed", true);
        yield return new WaitForSeconds(0.5f);

        // Reload the current scene
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
    }

    // triggers on player death
    public void RespawnLevel() {
        // Do particle effect
        GameObject explosion = (GameObject)Instantiate(explosionRef);
        explosion.transform.position = gameObject.transform.position;
        StartCoroutine("StaggerRestart");
        
    }
    // triggers on player restart
    public void RestartLevel() {
        StartCoroutine("ResetLevelTransition");
    }
}