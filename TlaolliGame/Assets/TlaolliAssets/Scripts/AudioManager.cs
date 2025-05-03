using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioClip cornHarvesting;
    public AudioClip cornPlanting;
    public AudioSource managerAudio;
    public AudioClip LogPick;
    public AudioClip buttonClick;

    public AudioClip raven;
    
   
    public void cornHarvest()
    {
        managerAudio.clip=  cornHarvesting;
        managerAudio.loop= false;
        managerAudio.Play();
    }
    public void cornPlantingSound()
    {
        managerAudio.clip=  cornPlanting;
        managerAudio.loop= false;
        managerAudio.Play();
    }
     public void logPickSound()
    {
        managerAudio.clip=  LogPick;
        managerAudio.loop= false;
        managerAudio.Play();
    }
     public void buttonClickSound()
    {
        managerAudio.clip=  buttonClick;
        managerAudio.loop= false;
        managerAudio.Play();
    }

    public void ravenSound()
    {
        managerAudio.clip=  raven;
        managerAudio.loop= false;
        managerAudio.Play();
    
    }



   

}
