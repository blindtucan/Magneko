using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//this is a script to control most soundFX and ambience. Footsteps are hear, but are primarily handle in each players' control script.

public class SoundManagerScript : MonoBehaviour
{
    public static AudioClip  BoxHit, ButtonOff, ButtonOn, CaveDripping, CaveDripping2, CaveWind, DeathSound,
        DeepCave, Footstep1, Footstep2, Footstep3, Footstep4,
        IdleTail, Jump1, Jump2, Jump3,
        LavaBoil, LavaSizzle, MenuHover, MenuPress, Mushroom, PressurePlate, PullBeam, PushBeam,
        Radial1, Radial2, SpikeHit, SpookyCave, Success,
        SpookyCaveWind, WaterDrip, Wooshes;

    static AudioSource audioSrc;
    // Start is called before the first frame update
    
    void Start()
    {
        BoxHit = Resources.Load<AudioClip>("BoxHit");
        ButtonOff = Resources.Load<AudioClip>("ButtonOff");
        ButtonOn = Resources.Load<AudioClip>("ButtonOn");
        CaveDripping = Resources.Load<AudioClip>("CaveDripping");
        CaveDripping2 = Resources.Load<AudioClip>("CaveDripping2");
        CaveWind = Resources.Load<AudioClip>("CaveWind");
        DeathSound = Resources.Load<AudioClip>("DeathSound");
        DeepCave = Resources.Load<AudioClip>("DeepCave");
        Footstep1 = Resources.Load<AudioClip>("Footstep1");
        Footstep2 = Resources.Load<AudioClip>("Footstep2");
        Footstep3 = Resources.Load<AudioClip>("Footstep3");
        Footstep4 = Resources.Load<AudioClip>("Footstep4");
        IdleTail = Resources.Load<AudioClip>("IdleTail");
        Jump1 = Resources.Load<AudioClip>("Jump1");
        Jump2 = Resources.Load<AudioClip>("Jump2");
        Jump3 = Resources.Load<AudioClip>("Jump3");
        LavaBoil = Resources.Load<AudioClip>("LavaBoil");
        LavaSizzle = Resources.Load<AudioClip>("LavaSizzle");
        MenuHover = Resources.Load<AudioClip>("MenuHover");
        MenuPress = Resources.Load<AudioClip>("MenuPress");
        MenuPress = Resources.Load<AudioClip>("Mushroom");
        PressurePlate = Resources.Load<AudioClip>("PressurePlate");
        PullBeam = Resources.Load<AudioClip>("PullBeam");
        PushBeam = Resources.Load<AudioClip>("PushBeam");
        Radial1 = Resources.Load<AudioClip>("Radial1");
        Radial2 = Resources.Load<AudioClip>("Radial2");
        SpikeHit = Resources.Load<AudioClip>("SpikeHit");
        SpookyCave = Resources.Load<AudioClip>("SpookyCave");
        SpookyCaveWind = Resources.Load<AudioClip>("SpookyCaveWind");
        Success = Resources.Load<AudioClip>("Success");
        WaterDrip = Resources.Load<AudioClip>("WaterDrip");
        Wooshes = Resources.Load<AudioClip>("Wooshes");

        audioSrc = GetComponent<AudioSource>(); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void PlaySound (string clip)
    {
        switch (clip)
        {
            case "BoxHit":
                audioSrc.PlayOneShot(BoxHit);
                break;
            case "ButtonOff":
                audioSrc.PlayOneShot(ButtonOff);
                break;
            case "ButtonOn":
                audioSrc.PlayOneShot(ButtonOn);
                break;
            case "CaveDripping":
                audioSrc.PlayOneShot(CaveDripping);
                break;
            case "CaveDripping2":
                audioSrc.PlayOneShot(CaveDripping2);
                break;
            case "CaveWind":
                audioSrc.PlayOneShot(CaveWind);
                break;
            case "DeathSound":
                audioSrc.PlayOneShot(DeathSound);
                break;
            case "DeepCave":
                audioSrc.PlayOneShot(DeepCave);
                break;
            case "Footstep1":
                audioSrc.PlayOneShot(Footstep1);
                break;
            case "Footstep2":
                audioSrc.PlayOneShot(Footstep2);
                break;
            case "Footstep3":
                audioSrc.PlayOneShot(Footstep3);
                break;
            case "Footstep4":
                audioSrc.PlayOneShot(Footstep4);
                break;
            case "IdleTail":
                audioSrc.PlayOneShot(IdleTail);
                break;
            case "Jump1":
                audioSrc.PlayOneShot(Jump1);
                break;
            case "Jump2":
                audioSrc.PlayOneShot(Jump2);
                break;
            case "Jump3":
                audioSrc.PlayOneShot(Jump3);
                break;
            case "LavaBoil":
                audioSrc.PlayOneShot(LavaBoil);
                break;
            case "LavaSizzle":
                audioSrc.PlayOneShot(LavaSizzle);
                break;
            case "MenuHover":
                audioSrc.PlayOneShot(MenuHover);
                break;
            case "MenuPress":
                audioSrc.PlayOneShot(MenuPress);
                break;
            case "Mushroom":
                audioSrc.PlayOneShot(Mushroom);
                break;
            case "PressurePlate":
                audioSrc.PlayOneShot(PressurePlate);
                break;
            case "PullBeam":
                audioSrc.PlayOneShot(PullBeam);
                break;
            case "PushBeam":
                audioSrc.PlayOneShot(PushBeam);
                break;
            case "Radial1":
                audioSrc.PlayOneShot(Radial1);
                break;
            case "Radial2":
                audioSrc.PlayOneShot(Radial2);
                break;
            case "SpikeHit":
                audioSrc.PlayOneShot(SpikeHit);
                break;
            case "SpookyCave":
                audioSrc.PlayOneShot(SpookyCave);
                break;
            case "SpookyCaveWind":
                audioSrc.PlayOneShot(SpookyCaveWind);
                break;
            case "Success":
                audioSrc.PlayOneShot(Success);
                break;
            case "WaterDrip":
                audioSrc.PlayOneShot(WaterDrip);
                break;
            case "Wooshes":
                audioSrc.PlayOneShot(Wooshes);
                break;

        }
    }
}
