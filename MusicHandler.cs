using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicHandler : MonoBehaviour
{
    public AudioClip[] songs;

    private int sceneNumber;
    private AudioSource audioSource;
    private GameObject[] objs;

    private float menuVol = 0.25f;
    private float gameVol;
    void Awake() {
        audioSource = gameObject.GetComponent<AudioSource>();
        objs = GameObject.FindGameObjectsWithTag("Music");
        if(objs.Length > 1) {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
    }

    void Update() {
        sceneNumber = SceneManager.GetActiveScene().buildIndex;
        
        if(sceneNumber <= 2) {
            // Debug.Log("Playing TitleSong.mp3");
            audioSource.clip = songs[0];
            audioSource.volume = menuVol;
        }
        else {
            if(sceneNumber >= 3 && sceneNumber <= 10) {
                // Debug.Log("Playing LabSong.mp3");
                audioSource.clip = songs[1];
                gameVol = 0.5f;
            }   
            else if(sceneNumber >= 11 && sceneNumber <= 22) {
                // Debug.Log("Playing MagmaSong.mp3");
                audioSource.clip = songs[2];
                gameVol = 0.5f;
            }
            else if(sceneNumber >= 23 && sceneNumber <= 32) {
                // Debug.Log("Playing MineSong.mp3");
                audioSource.clip = songs[3];
                gameVol = 1.0f;
            }
            else if(sceneNumber >= 36 && sceneNumber <=45) {
                // Debug.Log("Playing SewerSong.mp3");
                audioSource.clip = songs[4];
                gameVol = 1.0f;
            }
            else {
                gameVol = 0.0f;
            }

            if(PauseMenu.GameIsPaused) {
                audioSource.volume = gameVol/2;
            }
            else {
                audioSource.volume = gameVol;
            }
        }
                
        if(!audioSource.isPlaying) {
            audioSource.Play();
        }
    }
}
