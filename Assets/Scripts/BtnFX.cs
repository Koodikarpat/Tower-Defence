using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtnFX : MonoBehaviour
{
    public AudioSource myFx;
    public AudioClip hoverFx;
    public AudioClip clickFx;

	// Use this for initialization
	public void HoverSound ()
    {
        myFx.PlayOneShot(hoverFx);
	}
	
	// Update is called once per frame
	public void ClickSound ()
    {
        myFx.PlayOneShot(clickFx);
    }
}
