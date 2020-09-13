using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player2Movement : MonoBehaviour
{
    //changes the player type between puller or pusher
    public bool puller = true;
    private int directionSwitcher;

    //animation variables
    Animator anim;
    Animator burstAnim;
    private bool facingRight = true;
    private int lookDir = 0;
    public bool victory = false;

    //variables for line renderer
    LineRenderer line;
    private float maxWide = 0.85f;
    private float minWide = 0.35f;
    private float wid1 = 0.35f;
    private float wid2 = 0.85f;
    private bool lineState = true;
    private float lineRate = 0.05f;

    //general player parameters
    public float moveSpeed = 10f;
    public float topMoveSpeed = 30f;
    public float airTopMoveSpeed = 30f;
    public float maxPossibleSpeed = 100f;
    public float jumpHeight = 10f;
    public float airborneDampen = 2f;
    public float pullStrength = 10f;
    public float tractorStrength = 100f;
    private float originalPullStrength;
    public bool MoveStop = false;
    //public float pullDecay = 5f;
    private bool grounded = false;
    private Rigidbody2D rb;
    private Vector2 foot;
    private bool StartDelay = true;
    private float StartDelayTimer = 1f;
    public bool paused = false;
    

    //the aimer gameobject shows where the player is aiming
    // xAim and yAim are based on the right thumbstick of a controller
    public GameObject aimer;
    public float aimerOffset = 1f;
    private float xAim;
    private float yAim;
    private Vector2 lookDirection;

    public LayerMask magLayer;
    public LayerMask groundLayer;
    public LayerMask cornerLayer;

    //parameters for radial burst
    public float burstRadius = 20f;
    public float burstForce = 20f;
    public float burstCooldown = 5f;
    private bool burstOnCooldown = false;
    private float burstCooldownTimer = 0f;

    //These variables control which character is controlled by which player. Depend on functions in the PlayerHandler script on the buttons on CharSelect Menus.
    private static int CharNumber = 1;
    private int CharAssignment = 2;

    //variables for audio
    public AudioSource audioSrc;
    private bool playStep = true;
    public AudioClip[] footstepArray;
    //variables for beam sounds
    public AudioSource Beam;

    //Var for Handling player death
    private PlayerDeath deathScript;

    //loading p1 audio
    void Awake()
    {
        audioSrc = GetComponent<AudioSource>();
    }

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        burstAnim = transform.GetChild(1).GetComponent<Animator>();
        rb = gameObject.GetComponent<Rigidbody2D>();
        lookDirection = Vector2.up;
        if (puller)
        {
            directionSwitcher = 1;
        }
        else
        {
            directionSwitcher = -1;
        }
        foot = new Vector2(transform.position.x, transform.position.y - 0.5f);
        originalPullStrength = pullStrength;

        //set line renderer parameters here
        line = GetComponent<LineRenderer>();
        line.SetVertexCount(2);
        line.SetWidth(wid1, wid2);

        // Get PlayerDeath script reference
        deathScript = FindObjectOfType<PlayerDeath>();
    }

    // Update is called once per frame
    void Update()
    {
        CharNumber = PlayerHandler.CharDecider;
        if (CharNumber == 1)
        {
            CharAssignment = 2;
        }
        else
        {
            CharAssignment = 1;
        }

        if(StartDelay){
            MovePause();
            MoveStop = true;
            StartDelayTimer -= Time.deltaTime;
            if(StartDelayTimer <= 0f){
                StartDelay = false;
                MoveStop = false;
            }
        }

        //prevents player from wall jumping. Used in the OnCollisionStay function
        foot = new Vector2(transform.position.x, transform.position.y - 0.5f);
        
        //updates the right thumbstick's current values
        xAim = Input.GetAxis("R_horizontal" + CharAssignment);
        yAim = Input.GetAxis("R_vertical" + CharAssignment) * -1;
        //Debug.Log(xAim + ", " + yAim);

        //handles player movement in the horizontal
        float moveDirection = Input.GetAxis("L_horizontal" + CharAssignment) + Input.GetAxis("Dpad" + CharAssignment);
        float move = moveDirection * moveSpeed * Time.deltaTime;
        if(move > 0 || move < 0){
            anim.SetBool("isWalking", true);
        }
        else{
            anim.SetBool("isWalking", false);
        }

        //this if/else slows the player's movement while airborne
        if(!Input.GetButton("Square" + CharAssignment) && !MoveStop){
            if(grounded){
                if(rb.velocity.x < topMoveSpeed && rb.velocity.x > -topMoveSpeed){
                    rb.AddForce(transform.right * move);
                }
            }
            else{
                if(-airTopMoveSpeed <= rb.velocity.x && rb.velocity.x <= airTopMoveSpeed ){
                    rb.AddForce((transform.right * move) / airborneDampen);

                } 
                else if(rb.velocity.x > airTopMoveSpeed && move <= 0f){
                    rb.AddForce((transform.right * move) / airborneDampen);
                }
                else if(rb.velocity.x < -airTopMoveSpeed && move >= 0f){
                    rb.AddForce((transform.right * move) / airborneDampen);
                }
            }
        }

        // prevents player from exceeding a set maximum velocity
        if(rb.velocity.x >= maxPossibleSpeed){
            rb.velocity = new Vector2(maxPossibleSpeed, rb.velocity.y);
        }
        if(rb.velocity.y >= maxPossibleSpeed){
            rb.velocity = new Vector2(rb.velocity.x, maxPossibleSpeed);
        }

        //freezes character and plays victory animation
        if(victory){
            VictoryPause();
        }
        if(MoveStop){
            MovePause();
        }

        //scene reset
        if(Input.GetButton("Triangle" + CharAssignment)){
            // Death();
            deathScript.RestartLevel();
        }

        if((Input.GetButton("Circle" + CharAssignment) || Input.GetButtonDown("L1jump" + CharAssignment)) && grounded){
            rb.velocity = rb.velocity / 2;
        }
        
        //code for jumping, checks if player is contacting ground before allowing jump
        if ((Input.GetButtonDown("X" + CharAssignment) || Input.GetButtonDown("R1jump" + CharAssignment)) && paused == false)
        {
            if(Physics2D.OverlapCircle(foot, 0.7f, cornerLayer)){
                grounded = true;
            }
            if(grounded){
                anim.SetBool("jump", true);
                SoundManagerScript.PlaySound("Jump1");
                rb.AddForce(transform.up * jumpHeight);
                //grounded = false;
                //Debug.Log("Jumped!");
            }
        }

        //changes the position of the aimer based on the right thumbstick inputs
        if (xAim > 0.01f || xAim < -0.01f)
        {
            if (xAim > yAim && xAim > 0f)
            {
                lookDirection = Vector2.right;
                aimer.transform.position = new Vector3(transform.position.x + aimerOffset, transform.position.y, 0);
                aimer.transform.eulerAngles = new Vector3(0,0,270);
                lookDir = 0;               
            }
            if (xAim < yAim && xAim < 0f)
            {
                lookDirection = Vector2.left;
                aimer.transform.position = new Vector3(transform.position.x - aimerOffset, transform.position.y, 0);
                aimer.transform.eulerAngles = new Vector3(0,0,90);
                lookDir = 0;
            }
        }
        if (yAim > 0.01f || yAim < -0.01f)
        {
            if (yAim > xAim && yAim > 0f)
            {
                lookDirection = Vector2.up;
                aimer.transform.position = new Vector3(transform.position.x, transform.position.y + aimerOffset, 0);
                aimer.transform.eulerAngles = new Vector3(0,0,0);
                lookDir = 1;
            }
            if (yAim < xAim & yAim < 0f)
            {
                lookDirection = Vector2.down;
                aimer.transform.position = new Vector3(transform.position.x, transform.position.y - aimerOffset, 0);
                aimer.transform.eulerAngles = new Vector3(0,0,180);
                lookDir = 2;
            }
        }

        anim.SetInteger("pushDirection", lookDir);

        //sets direction of sprite
        if(moveDirection > 0 && !facingRight){
            Flip();
            if(lookDirection == Vector2.left){
                aimer.transform.position = new Vector3(transform.position.x - aimerOffset, transform.position.y, 0);
                aimer.transform.eulerAngles = new Vector3(0,0,90);
            }
            if(lookDirection == Vector2.right){
                aimer.transform.position = new Vector3(transform.position.x + aimerOffset, transform.position.y, 0);
                aimer.transform.eulerAngles = new Vector3(0,0,270);
            }
        } else if(moveDirection < 0 && facingRight){
            Flip();
            if(lookDirection == Vector2.right){
                aimer.transform.position = new Vector3(transform.position.x + aimerOffset, transform.position.y, 0);
                aimer.transform.eulerAngles = new Vector3(0,0,270);
            }
            if(lookDirection == Vector2.left){
                aimer.transform.position = new Vector3(transform.position.x - aimerOffset, transform.position.y, 0);
                aimer.transform.eulerAngles = new Vector3(0,0,90);
            }
        }

        //switches the player between push and pull, for playtesting
        if (Input.GetKeyDown("p"))
        {
            directionSwitcher = directionSwitcher * -1;
        }

        //changes width of linerenderer
        if(lineState){
            if(wid1 <= maxWide){
                wid1 += lineRate;
            }else{
                wid2 += lineRate;
            }
            if(wid2 >= maxWide){
                lineState = false;
            }
        }else{
            if(wid1 >= minWide){
                wid1 -= lineRate;
            }else{
                wid2 -= lineRate;
            }
            if(wid2 <= minWide){
                lineState = true;
            }
        }
        line.SetWidth(wid1, wid2);

        //Debug.Log(Input.GetButton("Fire2"));
        if (Input.GetButton("LT" + CharAssignment))
        {
            if (!burstOnCooldown)
            {
                SoundManagerScript.PlaySound("Radial" + CharAssignment);
                RadialBurst();
                burstAnim.Play("BurstPush");
                burstOnCooldown = true;
            }
        }

        //handles the radial burst cooldown
        burstCooldownTimer += Time.deltaTime;
        if (burstCooldownTimer >= burstCooldown)
        {
            burstOnCooldown = false;
            burstCooldownTimer = 0f;
        }
        
        //code for pulling / pushing objects
        // MAKE SURE THIS STAYS AT THE END OF THE UPDATE LOOP
        //Debug.Log(Input.GetAxis("RT" + CharAssignment));
        if (Input.GetButton("RT" + CharAssignment))
        {
            //sound for looping beam activation
            Beam.enabled = true;
            Beam.loop = true;
            FlipSet();

            anim.SetBool("pushing", true);
            //Debug.Log("Firing ray");
            RaycastHit2D hit = Physics2D.Raycast(aimer.transform.position, lookDirection, Mathf.Infinity, magLayer);
            RaycastHit2D groundHit = Physics2D.Raycast(aimer.transform.position, lookDirection, Mathf.Infinity, groundLayer);
            //exit case, happens if nothing is in range of magnet
            line.enabled = true;
            line.SetPosition(0, aimer.transform.position);
            // if(groundHit.collider == null){
            //     return;
            // }

            if(hit.collider == null){
                //line.enabled = false;
                line.SetPosition(1, groundHit.point);
                if(groundHit.collider.gameObject.tag == "Glass"){
                    line.SetPosition(1, groundHit.point + lookDirection * 2);
                }
                return;
            }

            if(groundHit.distance < hit.distance){
                //Debug.Log(groundHit.point);
                if(groundHit.collider.gameObject.tag != "Glass"){
                    return;
                }
            }

            if(hit.collider.gameObject.tag == "FreeMagnet" || hit.collider.gameObject.tag == "Player" || hit.collider.gameObject.tag == "HeavyMetal"){
                line.SetPosition(1, hit.point);
                
                Debug.Log("Hitting FreeMagnet");
                Rigidbody2D rbOther = hit.collider.gameObject.GetComponent<Rigidbody2D>();
                if(lookDirection == Vector2.up || lookDirection == Vector2.down){
                    rbOther.velocity = new Vector2(0,rbOther.velocity.y);
                }
                rbOther.AddForce(-lookDirection * pullStrength * directionSwitcher);
                //add force in perpendicular direction here, to keep things in the beam
                rbOther.AddForce(Centralizer(rbOther));
                
            }
            if(hit.collider.gameObject.tag == "LockMagnet"){
                line.enabled = true;
                line.SetPosition(0, aimer.transform.position);
                line.SetPosition(1, hit.point);
                
                Debug.Log("Hitting LockMagnket");
                rb.AddForce(lookDirection * pullStrength * directionSwitcher);
            }
            
            // if(pullStrength >= originalPullStrength / 4){
            //     pullStrength -= pullDecay;
            // }
            
        }else
        {
            //turns off beam loop audio
            Beam.enabled = false;
            Beam.loop = false;
            anim.SetBool("pushing", false);
            pullStrength = originalPullStrength;
            line.enabled = false;
        }

        StartCoroutine("Footsteps");

    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Ground"){
            grounded = Physics2D.OverlapCircle(foot, 0.2f, groundLayer);
            
        }else{
            grounded = true;
        }
        
        anim.SetBool("jump", false);
        if(collision.gameObject.tag == "Spikes"){
            // Death
            //deathScript.RespawnLevel();
        }
    }

    void OnCollisionExit2D(Collision2D collision){
        grounded = false;
    }

    void OnTriggerEnter2D(Collider2D coll){
        if(coll.gameObject.tag == "Lethal"){
            // Death
            deathScript.RespawnLevel();
        }
    }

    //code for the radial burst
    void RadialBurst()
    {
        //Debug.Log("burst attempt");
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, burstRadius, magLayer);
        int i = 0;
        while (i < hitColliders.Length)
        {
            Rigidbody2D rbOther = hitColliders[i].gameObject.GetComponent<Rigidbody2D>();
            Vector2 pullDir = (transform.position - hitColliders[i].transform.position).normalized;
            float fallOff = (Vector2.Distance(transform.position, hitColliders[i].transform.position) / 10) + 1;
            if(hitColliders[i].gameObject.tag == "Player"){
                rbOther.AddForce((pullDir * (burstForce/2) * directionSwitcher)/fallOff);
            }
            else{
                rbOther.AddForce((pullDir * burstForce * directionSwitcher)/fallOff);
            }
            i++;
        }
    }

    void VictoryPause(){
        rb.velocity = new Vector2(0,0);
        anim.SetBool("victory", true);
    }

    void MovePause(){
        rb.velocity = new Vector2(0,0);
    }

    void Flip(){
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
        
    }

    void FlipSet(){
        if(lookDirection == Vector2.right && !facingRight){
            Flip();
            aimer.transform.position = new Vector3(transform.position.x + aimerOffset, transform.position.y, 0);
            aimer.transform.eulerAngles = new Vector3(0,0,270);
        }
        if(lookDirection == Vector2.left && facingRight){
            Flip();
            aimer.transform.position = new Vector3(transform.position.x - aimerOffset, transform.position.y, 0);
            aimer.transform.eulerAngles = new Vector3(0,0,90);
        }
    }

    //function that loops footsteps at appropriate timings
    IEnumerator Footsteps()
    {
        if (Input.GetAxis("L_horizontal" + CharAssignment) != 0f && grounded && playStep)
        {
            playStep = false;
            audioSrc.clip = footstepArray[Random.Range(0, footstepArray.Length)];
            audioSrc.PlayOneShot(audioSrc.clip);
            yield return new WaitForSeconds(.4f);
            playStep = true;
        }
    }

    Vector2 Centralizer(Rigidbody2D rbOther){
        if(lookDirection == Vector2.up || lookDirection == Vector2.down){
            float distForce = transform.position.x - rbOther.transform.position.x;
            float fixForce = distForce * tractorStrength;
            if(rbOther.gameObject.tag == "Player"){
                fixForce = fixForce / 2; 
            }
            return(new Vector2(fixForce, 0));
        }
        if(lookDirection == Vector2.right || lookDirection == Vector2.left){
            float distForce = transform.position.y - rbOther.transform.position.y;
            float fixForce = distForce * tractorStrength;
            if(rbOther.gameObject.tag == "Player"){
                fixForce = fixForce / 2; 
            }
            return(new Vector2(0, fixForce));
        }
        return(new Vector2(0,0));
    }

    public void Death(){
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
    }
}