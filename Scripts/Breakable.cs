using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breakable : MonoBehaviour
{
    private Vector2 breakSpeed;
    public GameObject brokenBits;
    public float xSpeed = 0f;
    public float ySpeed = 0f;
    
    // Start is called before the first frame update
    void Start()
    {
        breakSpeed = new Vector2(xSpeed, ySpeed);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D col){
        if(col.collider.tag == "HeavyMetal"){
            Rigidbody2D rbCol = col.collider.gameObject.GetComponent<Rigidbody2D>();
            if(rbCol.velocity.x >= xSpeed){
                Vector2 newPos = transform.position;
                Destroy(this.gameObject);
                Instantiate(brokenBits, newPos, Quaternion.identity);
            }
            if(rbCol.velocity.y >= ySpeed){
                Vector2 newPos = transform.position;
                Destroy(this.gameObject);
                Instantiate(brokenBits, newPos, Quaternion.identity);
            }
        }
    }
}
