using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spring : MonoBehaviour
{
    public float force = 20f;
    public Animator anim;
    public AudioSource audioSrc;
    // Start is called before the first frame update
    void Awake()
    {
        audioSrc = GetComponent<AudioSource>();
    }
    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
           
    }

    void OnCollisionEnter2D(Collision2D col){
        Rigidbody2D rbCol = col.collider.gameObject.GetComponent<Rigidbody2D>();
        if(rbCol.gameObject.tag=="Player") {
            rbCol.AddForce(transform.up * (force/2));
           
        }
        else {
            rbCol.AddForce(transform.up * force);
        }
        anim.SetTrigger("Mushroom_Bounce");
    }

    void OnCollisionStay2D(Collision2D col){
        Rigidbody2D rbCol = col.collider.gameObject.GetComponent<Rigidbody2D>();
        if(rbCol.gameObject.tag=="Player") {
            rbCol.AddForce(transform.up * (force/2));
            audioSrc.Play(0);
        }
        else {
            rbCol.AddForce(transform.up * force);
        }
        anim.SetTrigger("Mushroom_Bounce");
    }
}
