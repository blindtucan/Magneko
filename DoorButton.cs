using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorButton : MonoBehaviour
{
    //Animator anim;
    public GameObject door;
    BoxCollider2D doorCollider;
    public GameObject frame;
    public GameObject frameBack;
    private Animator buttonAnimator;
    public float moveDistance = 10f;
    private float maxHeight;
    public float moveSpeed = 1f;
    private float startPos;
    private bool active = false;
    private int touchCount = 0;
    public bool up = true;
    public bool down = false;
    public bool left = false;
    public bool right = false;

    private GameObject player1;
    private GameObject player2;

    // Start is called before the first frame update
    void Start()
    {
        doorCollider = door.GetComponent<BoxCollider2D>();
        player1 = GameObject.Find("Player1");
        player2 = GameObject.Find("Player2");
        if(up){
            startPos = door.transform.position.y;
            maxHeight = startPos + moveDistance;
            frame.transform.position = new Vector2(door.transform.position.x, door.transform.position.y + (door.transform.localScale.y * 2));
            frameBack.transform.position = frame.transform.position;
        }
        if(down){
            startPos = door.transform.position.y;
            maxHeight = startPos - moveDistance;
            frame.transform.position = new Vector2(door.transform.position.x, door.transform.position.y - (door.transform.localScale.y * 2));
            frameBack.transform.position = frame.transform.position;
            frame.transform.Rotate(0, 0, 180);
            frameBack.transform.Rotate(0, 0, 180);
        }
        if(right){
            startPos = door.transform.position.x;
            maxHeight = startPos + moveDistance;
            frame.SetActive(false);
            frameBack.SetActive(false);
            //door.transform.Rotate(0, 0, 90, Space.Self);
        }
        if(left){
            startPos = door.transform.position.x;
            maxHeight = startPos - moveDistance;
            frame.SetActive(false);
            frameBack.SetActive(false);
        }
        if(door.transform.localScale.x > 1){
            frame.SetActive(false);
            frameBack.SetActive(false);
        }
        buttonAnimator = gameObject.GetComponent<Animator>();
    }

    void Update(){
        if(active){
            if(up){
                if(door.transform.position.y <= maxHeight){
                    door.transform.Translate(0, moveSpeed, 0, Space.World);
                    //Debug.Log(door.transform.position.y);
                }
            }
            if(down){
                if(door.transform.position.y >= maxHeight){
                    door.transform.Translate(0, -moveSpeed, 0, Space.World);
                    //Debug.Log(door.transform.position.y);
                }
            }
            if(right){
                if(door.transform.position.x <= maxHeight){
                    door.transform.Translate(moveSpeed, 0, 0, Space.World);
                    //Debug.Log(door.transform.position.y);
                }
            }
            if(left){
                if(door.transform.position.x >= maxHeight){
                    door.transform.Translate(-moveSpeed, 0, 0, Space.World);
                    //Debug.Log(door.transform.position.y);
                }
            }
        }else{
            if(up){
                if(door.transform.position.y > startPos){
                    door.transform.Translate(0, -moveSpeed, 0, Space.World);
                }
            }
            if(down){
                if(door.transform.position.y < startPos){
                    door.transform.Translate(0, moveSpeed, 0, Space.World);
                }
            }
            if(right){
                if(door.transform.position.x > startPos){
                    door.transform.Translate(-moveSpeed, 0, 0, Space.World);
                }
            }
            if(left){
                if(door.transform.position.x < startPos){
                    door.transform.Translate(moveSpeed, 0, 0, Space.World);
                }
            }
        }
        if(doorCollider.bounds.Contains(player1.transform.position)){
            player1.GetComponent<Player1Movement>().Death();
        }
        if(doorCollider.bounds.Contains(player2.transform.position)){
            player2.GetComponent<Player2Movement>().Death();
        }
    }

    void OnTriggerEnter2D(Collider2D col){
        if((col.gameObject.tag != "Ground" && col.gameObject.tag != "Glass") || col.gameObject.name == "Door"){
            if(touchCount == 0){
                touchCount++;
                active = true;
                SoundManagerScript.PlaySound("ButtonOn");
                Debug.Log("anim");
                buttonAnimator.SetBool("pushed", true);
            }
            else{
                touchCount++;
            }
        }
        
    }
    // void OnTriggerStay2D(Collider2D col){
    //     if(col.gameObject.tag != "Ground"){
    //         active = true;
    //     }
    // }

    void OnTriggerExit2D(Collider2D col){
        if(col.gameObject.tag != "Ground"){
            touchCount--;
        }
        if(touchCount == 0){
            active = false;
            SoundManagerScript.PlaySound("ButtonOff");
            buttonAnimator.SetBool("pushed", false);
        }
    }
}
