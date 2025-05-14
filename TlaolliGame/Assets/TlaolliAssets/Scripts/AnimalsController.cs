using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalsController : MonoBehaviour
{
   
    private bool firstAnimal = false;
    private AudioManager audioManager;
    private DialogosFarm dialogosFarm;
    private GameController gameController;
    private MaizGroth maizGroth;

    // Start is called before the first frame update
    void Start()
    {
        audioManager= FindObjectOfType<AudioManager>();
        dialogosFarm= FindObjectOfType<DialogosFarm>();
        gameController= FindObjectOfType<GameController>();
        maizGroth= FindObjectOfType<MaizGroth>();
        
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator spawnerAnimal()
    {

        if(!firstAnimal)
        {
            audioManager.ravenSound();
            audioManager.arbustosPlay();
            GameController.instance.spawnRaven();
            Vector2 positionSpawned = new Vector2 (-1.66f, -2.23f);
            yield return new WaitForSeconds(2);
            Debug.Log("AnimalEnCamino");
            firstAnimal= true;
            animalRob();

            dialogosFarm.contador= 19;
            dialogosFarm.SecuenciaDialogos();
            

        }else if (firstAnimal)
        {
            
            int SpawnOn= Random.Range(0,1);
            Debug.Log(SpawnOn);
            if(SpawnOn == 0)
            {
            GameController.instance.spawnRaven();
            audioManager.ravenSound();
             audioManager.arbustosPlay();
           
            yield return new WaitForSeconds(2);
            
            Debug.Log("AnimalEnCamino");
            firstAnimal= true;
            animalRob();

            }
          

        }
        

    }

    public void animalRob()
    {
        if (GameController.cornCosechado > 0 || maizGroth.cornSembradoActual > 0)
            {
                float porcentaje = 0.7f;

                if (GameController.cornCosechado > 0)
                {
                    int descuentoCosechado = Mathf.FloorToInt(GameController.cornCosechado * porcentaje);
                    GameController.cornCosechado -= descuentoCosechado;
                    Debug.Log($"Se descontaron {descuentoCosechado} de cornCosechado.");
                    gameController.UpdateCosechadoText();
                }

                if (maizGroth.cornSembradoActual > 0)
                {
                    int descuentoSembrado = Mathf.FloorToInt(maizGroth.cornSembradoActual * porcentaje);
                    maizGroth.cornSembradoActual -= descuentoSembrado;
                    Debug.Log($"Se descontaron {descuentoSembrado} de cornSembradoActual.");
                    maizGroth.EliminarSembrados(descuentoSembrado);

                }
            }

    }
}
