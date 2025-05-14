using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HarveLog : MonoBehaviour
{
    private AudioManager audioManager;
    private AnimationsMain animationsMain;
    private DialogosFarm  dialogosFarm;
  

    void Start()
    {
        audioManager= FindObjectOfType<AudioManager>();
        animationsMain = FindObjectOfType<AnimationsMain>();
        dialogosFarm = FindObjectOfType<DialogosFarm>();
        

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
            if(dialogosFarm.contador<=2)
            {
                Destroy(gameObject);
            }else if(dialogosFarm.contador>2)
            {
            StartCoroutine(GameController.instance.sumChest()); 
            StartCoroutine(WaitAndDestroy());

            }
        
            
        }

        IEnumerator WaitAndDestroy()
{
    // Esperar mientras la animación aún se esté reproduciendo
    while (animationsMain.sumChest1.IsPlaying("+1_Sum"))
    {
        yield return null; // Espera un frame
    }

    Destroy(gameObject);
}


    }




}
