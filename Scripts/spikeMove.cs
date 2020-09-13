using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spikeMove : MonoBehaviour
{
    public float secondsRotation = 3f;
    public float moveSpeed = 5f;
    private float direction = 1;
    private float timer = 0f;
    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if(timer >= secondsRotation){
            direction = direction * -1;
            timer = 0f;
        }
        Vector2 move = new Vector2(moveSpeed, 0);
        rb.AddForce(move * direction);
    }
}
