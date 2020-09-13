using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class teleporter : MonoBehaviour
{
    public string newScene;
    // public float newX;
    // public float newY;
    // private Vector2 newLocation;
    // Start is called before the first frame update
    void Start()
    {
        //newLocation = new Vector2(newX, newY);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D collision) {
		if (collision.gameObject.tag == "Player") {
			//collision.gameObject.transform.position = newLocation;
            SceneManager.LoadScene(newScene);
            SoundManagerScript.PlaySound("Success");
        }		
	}
}
