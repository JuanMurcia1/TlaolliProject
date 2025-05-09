using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HarveLog : MonoBehaviour
{
    private AudioManager audioManager;

    void Start()
    {
        audioManager= FindObjectOfType<AudioManager>();
    }
    
    public void OnMouseDown()
    {
        
        if (gameObject.CompareTag("Log"))
        {
            audioManager.logPickSound();
            GameController.wood++; 
            GameController.instance.UpdateWoodText(); 
            GameController.instance.actualExperience++;
            GameController.instance.experienceSum();

            Destroy(gameObject);
        }


    }
}
