using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehavior : MonoBehaviour
{
    public Camera cam;
    private GameObject player1;        //Public variable to store a reference to the player game object
    private GameObject player2;

    private Vector2 lookDirection;
    public float panDistance = 500f;
    private float YpanDistance;
    //public float distance = 20;

    private Vector3 offset;            //Private variable to store the offset distance between the player and camera
    private float offsetX;
    private float offsetY;
    private float offsetZ;
    private float dist;
    private float prevDist;
    public float zoomMultiplier = 1;
    public float zoomTime = 0.1f;
    public float moveTime = 1f;
    public float cameraZoom = 2;
    public float minZoom = 20;

    //POLISH NOTE: try to "smooth" the camera zooming
    
    // Use this for initialization
    void Start () 
    {
        //Calculate and store the offset value by getting the distance between the player's position and camera's position.
        player1 = GameObject.Find("Player1");
        player2 = GameObject.Find("Player2");
        offset = (player1.transform.position + player2.transform.position)/2;
        cam.orthographicSize = cameraZoom;
        YpanDistance = panDistance * 4;
        dist = minZoom;
    }

    // LateUpdate is called after Update each frame
    void Update () 
    {
        
        
        // Set the position of the camera's transform to be the same as the player's, but offset by the calculated offset distance.
        // dist = Vector2.Distance(player1.transform.position, player2.transform.position);
        // if(dist < minZoom){
        //     dist = minZoom;
        // }
    
        //float newDist = Mathf.Lerp(prevDist, dist, zoomTime);
        //float refe = 0.0f;
        //cam.orthographicSize = newDist * zoomMultiplier;
        //Debug.Log(Time.deltaTime * zoomTime);
        //cam.orthographicSize = Mathf.Lerp(cam.orthographicSize * zoomMultiplier, dist * zoomMultiplier, Time.deltaTime * zoomTime);
        cam.orthographicSize = Mathf.Lerp(cam.orthographicSize * zoomMultiplier, dist * zoomMultiplier, 0.03f);
        // public static float SmoothDamp(float current, float target, ref float currentVelocity, float smoothTime, float maxSpeed = Mathf.Infinity, float deltaTime = Time.deltaTime);
        //cam.orthographicSize = Mathf.SmoothDamp(cam.orthographicSize * zoomMultiplier, dist * zoomMultiplier, ref refe, 1);
        
        // offsetZ = dist * -1;
        offsetX = (player1.transform.position.x + player2.transform.position.x) / 2;
        offsetY = (player1.transform.position.y + player2.transform.position.y) / 2;
        

        if(Input.GetButton("Square1")){
            float xAim = Input.GetAxis("L_horizontal1") * panDistance;
            float yAim = Input.GetAxis("L_vertical1") * YpanDistance * -1;
            offsetX += xAim;
            offsetY += yAim;
            //Debug.Log(xAim + ", " + yAim);
        }
        if(Input.GetButton("Square2")){
            float xAim = Input.GetAxis("L_horizontal2") * panDistance;
            float yAim = Input.GetAxis("L_vertical2") * YpanDistance * -1;
            offsetX += xAim;
            offsetY += yAim;
            //Debug.Log(xAim + ", " + yAim);
        }
        offset = new Vector3(offsetX, offsetY, -1);
        //transform.position = offset;
        prevDist = dist;

        transform.position = Vector3.Slerp(transform.position, offset, moveTime * Time.deltaTime);
    }

    void LateUpdate(){
        dist = Vector2.Distance(player1.transform.position, player2.transform.position);
        if(dist < minZoom){
            dist = minZoom;
        }
    }
}