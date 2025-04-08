using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogosFarm : MonoBehaviour
{
    public Text dialogos;
    public int contador;
    public AnimationsMain animationsMain;
    private GameController gameController;

    // Start is called before the first frame update
    void Start()
    {
        contador=0;
        animationsMain.panelDialogos();
        SecuenciaDialogos();
        gameController= FindObjectOfType<GameController>();


        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SecuenciaDialogos()
    {
        if(contador==0)
        {
            dialogos.text="Interactua con la flecha  de abajo para ver las semillas disponibles.";
        }else if(contador==1)
        {
            dialogos.text="Arrastra la semilla y Cuando termines de plantar cierra el menú de semillas para poder cosechar.";
            StartCoroutine(animationsMain.fadedUpPanel());
        }else if(contador==2)
        {
            dialogos.text="Muy bien, has cosechado tu primer maiz, ahora necesitamos construir la choza almacén, recolecta algo de madera";

            animationsMain.panelDialogos();

        }else if(contador==3)
        {
            dialogos.text="Perfecto, tenemos suficiente, abre el cofre, de aquí en adelante verás tus recursos ahí.";
            gameController.ButtonMenuMaterial.SetActive(true);
            StartCoroutine(animationsMain.MoveOffUiTexts());
            StartCoroutine(gameController.UiTextOff());
        }
        else if(contador==4)
        {
            dialogos.text="Muy bien, ahora podrás desplegar el menú de construcción al lado de materiales. Construye una choza";
            gameController.buttonMenuConstruccion.SetActive(true);
        }
        else if(contador==5)
        {
            dialogos.text="Ahora con la choza construida, puedes almacenar tu maiz";
            StartCoroutine(warningC5()); 
                 
        }else if(contador==6)
        {
            dialogos.text= "Podrás ver tu maiz almacenado. Ahora abre el menú de construcción y pon una fogata";
            gameController.fogataButton.SetActive(true);
        }else if(contador==7)
        {
            dialogos.text="¡Perfecto!, una linda y cálidad fogata, esto nos permitirá asar las mazorcas para poder venderlas, presiona encima de la fogata.";
        }
    }


    private IEnumerator warningC5()
    {
        yield return new WaitForSeconds(6);
        dialogos.text= "Pero cuidado... no dejes mucho los cultivos afuera, hay animales cerca, interactua con la carreta para guardar tus cultivos";
        gameController.car.SetActive(true);  
    }
}
