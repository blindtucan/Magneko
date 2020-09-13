using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerToken : MonoBehaviour
{
    // Set in inspector
    public GameObject player1Token;
    public GameObject player2Token;
    public GameObject textGO;
    public Sprite player1ActiveSprite;
    public Sprite player2ActiveSprite;

    // the public vars are used to access these
    private RectTransform p1rect;
    private RectTransform p2rect;
    private Text text;    

    // vars for positioning
    private int lx;
    private int mx;
    private int rx;
    private int p1y;
    private int p2y;
 
    // Vars for count down and game loading 
    private float timerTime = 3.5f;
    private float timer;
    private string ogtext;
    private PlayerHandler playerHandler;
    private ButtonAudio buttonAudio;
    private Color textTransparent;
    private Color textOpaque;
    private bool transparent;
    private Sprite player1OgSprite;
    private Sprite player2OgSprite;
    private Image player1ImageRef;
    private Image player2ImageRef;

    // Vars for dead zone
    private float deadTime = 0.1f;
    private float deadTimer = 0.0f;

    //vars for fade out
    public Image fade;
    private Animator faderAnimator;

    //Vars for token sprite
    

    // Start is called before the first frame update
    void Start()
    {
                
        // Set Text transparent and timer
        text = textGO.GetComponent<Text>();
        textTransparent = new Color(text.color.r, text.color.g, text.color.b, 0.0f);
        textOpaque = new Color(text.color.r, text.color.g, text.color.b, 1.0f);
        text.color = textTransparent;
        ogtext = text.text;
        timer = timerTime;
        transparent = true;

        //Get Script reference
        playerHandler = FindObjectOfType<PlayerHandler>();
        buttonAudio = FindObjectOfType<ButtonAudio>();
        faderAnimator = fade.GetComponent<Animator>();
        
        // Get rects and positions
        p1rect = player1Token.GetComponent<RectTransform>();
        p2rect = player2Token.GetComponent<RectTransform>();
        p1y = (int) p1rect.anchoredPosition.y;
        p2y = (int) p2rect.anchoredPosition.y;
        lx = -50;
        mx = 0;
        rx = 50;

        // Get OG sprites
        player1OgSprite = player1Token.GetComponent<Image>().sprite;
        player2OgSprite = player2Token.GetComponent<Image>().sprite;
        player1ImageRef = player1Token.GetComponent<Image>();
        player2ImageRef = player2Token.GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if (deadTimer > deadTime) {
            MoveToken();
            deadTimer = 0.0f;
        }
        deadTimer = deadTimer + Time.deltaTime;
        
        CheckTokenLocation();

        //Play sound and change sprite if players press X
        if (Input.GetButtonDown("X" + 1)){
            buttonAudio.PressSound();
            player1ImageRef.sprite = player1ActiveSprite;
            Invoke("ResetToken1Sprite", 0.1f);
        }
        if (Input.GetButtonDown("X" + 2)) { 
            player2ImageRef.sprite = player2ActiveSprite;
            buttonAudio.PressSound();
            Invoke("ResetToken2Sprite", 0.1f);
        }

        // Implement dead zone: https://answers.unity.com/questions/537638/capture-joystick-direction-accurately.html
        
    }

    // make this a coroutine?  
    void MoveToken() {
        float moveDirection1 = Input.GetAxis("L_horizontal" + 1) + Input.GetAxis("Dpad" + 1);
        float moveDirection2 = Input.GetAxis("L_horizontal" + 2) + Input.GetAxis("Dpad" + 2);
        
        // TESTING //
        // if(Input.anyKey) {
        //     SetTokenPosition(p1rect,rx,p1y);
        //     SetTokenPosition(p2rect,lx,p2y);
        // }

        // player 1 token
        // move right
        if ((moveDirection1 > 0) && (p1rect.anchoredPosition.x == lx)) {
            timer = timerTime;
            buttonAudio.HoverSound();
            SetTokenPosition(p1rect, mx, p1y);
        }
        else if ((moveDirection1 > 0) && (p1rect.anchoredPosition.x == mx)) {
            timer = timerTime;
            buttonAudio.HoverSound();
            SetTokenPosition(p1rect, rx, p1y);
        }
        //move left
        if ((moveDirection1 < 0) && (p1rect.anchoredPosition.x ==rx)) {
            timer = timerTime;
            buttonAudio.HoverSound();
            SetTokenPosition(p1rect, mx, p2y);
        } 
        else if((moveDirection1 < 0) && (p1rect.anchoredPosition.x == mx)) {
           timer = timerTime;
           buttonAudio.HoverSound();
           SetTokenPosition(p1rect, lx, p1y);
        }

        // player 2 token
        // move right
        if ((moveDirection2 > 0) && (p2rect.anchoredPosition.x == lx)) {
            timer = timerTime;
            buttonAudio.HoverSound();
            SetTokenPosition(p2rect, mx, p2y);
        }
        else if ((moveDirection2 > 0) && (p2rect.anchoredPosition.x == mx)) {
            timer = timerTime;
            buttonAudio.HoverSound();
            SetTokenPosition(p2rect, rx, p2y);
        }
        //move left
        if ((moveDirection2 < 0) && (p2rect.anchoredPosition.x ==rx)) {
            timer = timerTime;
            buttonAudio.HoverSound();
            SetTokenPosition(p2rect, mx, p2y);
        } 
        else if((moveDirection2 < 0) && (p2rect.anchoredPosition.x == mx)) {
            timer = timerTime;
            buttonAudio.HoverSound();
            SetTokenPosition(p2rect, lx, p2y);
        }
    }

    void CheckTokenLocation() {
        // Do not start
        if ((p1rect.anchoredPosition.x == p2rect.anchoredPosition.x) && (p1rect.anchoredPosition.x != mx)) {
            //Debug.Log("You must select different characters.");
            // tell players that they cannot choose the same character
            text.text = ogtext;
            if (transparent) {
                StartCoroutine(FadeTextToFullAlpha(0.7f, text));
            }            
        }
        else if ((p1rect.anchoredPosition.x == rx && p2rect.anchoredPosition.x == lx) || (p1rect.anchoredPosition.x == lx && p2rect.anchoredPosition.x == rx)) {
            //Change text to timer
            if (timer > 0.5) {
                // Starting countdown
                timer -= Time.deltaTime;
                text.text = "Starting in " + (timer).ToString("0");
                if (transparent) {
                  StartCoroutine(FadeTextToFullAlpha(0.7f, text));
                }    
            }
            else {
                text.text = "Starting meow!";
                
                // Set characters to controllers based on player one
                if (p1rect.anchoredPosition.x < 0) {
                    playerHandler.ChooseKnife();
                }
                else if (p1rect.anchoredPosition.x > 0) {
                    playerHandler.ChooseFridge();
                }
                // Start game
                faderAnimator.SetTrigger("FadeOut");
                Invoke("StartGame", 0.75f);
            }
        }
    }

    // HELPER FUNCTIONS

    // Reset the sprite's image
    public void ResetToken1Sprite() {
        player1ImageRef.sprite = player1OgSprite;
    }

    public void ResetToken2Sprite() {
        player2ImageRef.sprite = player2OgSprite;
    }

    // In its own funtion to invoke it after a time period
    public void StartGame() {
        SceneManager.LoadScene("MainMenu"); 
    }

    void SetTokenPosition(RectTransform rect, int a, int b) {
        rect.anchoredPosition = new Vector2(a, b);    
    }

    // The two following were taken from: https://forum.unity.com/threads/fading-in-out-gui-text-with-c-solved.380822/

    public IEnumerator FadeTextToFullAlpha(float t, Text i)
    {
        i.color = new Color(i.color.r, i.color.g, i.color.b, 0);
        //yield return new WaitForSeconds(0.2f);
        while (i.color.a < 1.0f)
        {
            i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a + (Time.deltaTime / t));
            yield return null;
        }
        transparent = false;
    }
 
    public IEnumerator FadeTextToZeroAlpha(float t, Text i)
    {
        i.color = new Color(i.color.r, i.color.g, i.color.b, 1);
        //yield return new WaitForSeconds(0.2f);
        while (i.color.a > 0.0f)
        {
            i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a - (Time.deltaTime / t));
            yield return null;
        }
        transparent = true;
    }
}

