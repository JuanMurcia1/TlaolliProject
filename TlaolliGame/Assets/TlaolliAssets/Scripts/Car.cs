using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{
     GameController gameController;
     private Animation animation;

     private bool firstTouchCar= false;
     private DialogosFarm dialogosFarm;

     public bool carretaMoving = false;

    // Start is called before the first frame update
    void Start()
    {
       gameController= FindObjectOfType<GameController>();
        animation=GetComponent<Animation>();
        dialogosFarm=FindObjectOfType<DialogosFarm>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    

    public void OnMouseDown()
    {
        if (gameObject.CompareTag("Car"))
        {
            if(GameController.cornCosechado>=0)
            {
            animation.Play("DirectionWarehouse");
            carretaMoving=true;
            gameController.UpdateCosechadoText();
            
            
            if(!firstTouchCar)
            {
                dialogosFarm.contador= 6;
                dialogosFarm.SecuenciaDialogos();
                firstTouchCar=true;

            }

            StartCoroutine(returnCar(5));
            }

        }
    }

    private IEnumerator returnCar(int wait)
    {
        yield return new WaitForSeconds(wait);
        gameController.boxCorns.SetActive(false);
        animation.Play("DirectionZonePlanting");
        gameController.boxCorns.SetActive(false);
    }
}
