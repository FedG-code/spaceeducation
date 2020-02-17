using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonSounds : MonoBehaviour
{
    public AudioSource myFx;
    public AudioClip hoverFx;
    public AudioClip clickFx;


    public void HoverSound()
    {

        myFx.PlayOneShot(hoverFx);

    }

    public void ClickSound()
    { 
            myFx.PlayOneShot(clickFx);
        
    }


}
