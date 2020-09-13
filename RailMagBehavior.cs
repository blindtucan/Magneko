using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RailMagBehavior : MonoBehaviour
{
    public GameObject rail;
    public float railThickness = 0.15f;
    private Rigidbody2D rbRail;
    
    private Vector2 startPos;
    public bool xMovement = false;
    public bool yMovement = false;
    public float movementRange;
    private float moveLimitUpper;
    private float moveLimitLower;
    private Rigidbody2D rb;
    

    // Start is called before the first frame update
    void Start()
    {
        startPos = this.transform.position;
        rb = gameObject.GetComponent<Rigidbody2D>();

        if(xMovement){
            moveLimitUpper = startPos.x + movementRange;
            moveLimitLower = startPos.x - movementRange;
            rail.transform.localScale = new Vector3((movementRange * 2) + 0.9f, railThickness, 1);
            rb.constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
        }
        if(yMovement){
            moveLimitUpper = startPos.y + movementRange;
            moveLimitLower = startPos.y - movementRange;
            rail.transform.localScale = new Vector3(railThickness, (movementRange * 2) + 0.9f, 1);
            rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(xMovement){
            if(this.transform.position.x > moveLimitUpper){
                Vector2 fixedPos = new Vector2(moveLimitUpper, this.transform.position.y);
                rb.velocity = Vector2.zero;
                this.transform.position = fixedPos;
            }
            if(this.transform.position.x < moveLimitLower){
                Vector2 fixedPos = new Vector2(moveLimitLower, this.transform.position.y);
                rb.velocity = Vector2.zero;
                this.transform.position = fixedPos;
            }
        }
        if(yMovement){
            if(this.transform.position.y > moveLimitUpper){
                Vector2 fixedPos = new Vector2(this.transform.position.x, moveLimitUpper);
                rb.velocity = Vector2.zero;
                this.transform.position = fixedPos;
            }
            if(this.transform.position.y < moveLimitLower){
                Vector2 fixedPos = new Vector2(this.transform.position.x, moveLimitLower);
                rb.velocity = Vector2.zero;
                this.transform.position = fixedPos;
            }
        }
    }
}
