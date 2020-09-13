using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonAudio : MonoBehaviour
{
    public void HoverSound()
    {
        SoundManagerScript.PlaySound("MenuHover");
    }

    public void PressSound()
    {
        SoundManagerScript.PlaySound("MenuPress");
    }
}
