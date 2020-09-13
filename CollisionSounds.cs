using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionSounds : MonoBehaviour
{
    public AudioSource ColSrc;
    // Start is called before the first frame update
    void Start()
    {
        ColSrc = GetComponent<AudioSource>();
    }

    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (collision.gameObject.tag == "FreeMagnet" || collision.gameObject.tag == "Player")
        {
            Debug.Log("sound");
            ColSrc.Play();
        }
        
    }

}
