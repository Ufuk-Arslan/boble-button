using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXPlaying : MonoBehaviour
{
    public AudioSource timeDing, timeDoubleDing, megaTrueSound, buttonPressSound;

    public void PlayTimeDing ()
    {
        if (Button_Add.goldBool)
        {
            timeDoubleDing.Play();
        }
        else
        {
            timeDing.Play();
        }                           
    }

    public void PlayMegaTrueSound()
    {
        megaTrueSound.Play();
    }

    public void ButtonPressSound()
    {
        buttonPressSound.Play();
    }


}
