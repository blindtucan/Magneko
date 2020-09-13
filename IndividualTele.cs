using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndividualTele : MonoBehaviour
{
    public string PlayerName;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collision) {
		//Debug.Log("Enter");
        if (collision.gameObject.name == PlayerName) {
			this.transform.parent.gameObject.GetComponent<DualTeleporter>().checker++;
		}		
	}

    void OnTriggerExit2D(Collider2D collision){
        //Debug.Log("Exit");
        if(collision.gameObject.name == PlayerName){
            this.transform.parent.gameObject.GetComponent<DualTeleporter>().checker--;
        }
    }
}
