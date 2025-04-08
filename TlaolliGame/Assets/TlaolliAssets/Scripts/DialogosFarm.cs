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

    public GameObject buttonAvanza;

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
            dialogos.text="Arrastra la semilla y cuando termines de plantar cierra el menú de semillas para poder cosechar.";
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
        }else if(contador==8)
        {
            dialogos.text="El número de mazorcas asadas se verán en tu baul.";
            buttonAvanza.SetActive(true);

        }else if(contador==9)
        {
            dialogos.text="Cuando coseches, en el baul podrás ver la cantidad, pero solo cuando almacenes la choza mostrará lo que almacenaste.";
        }
        else if(contador==10)
        {
            dialogos.text="Con estos elementos básicos podrás empezar por ahora, ¡Recuerda!, solo puedes asar si has almacenano en tu choza el maiz. Presiona Avanza ";
        }else if(contador==11)
        {
            dialogos.text="Otro tip: mientras no guardes mediante la carreta tu maiz, podrá ser robado por algunos animales, ten cuidado en las noches.";
            
        }else if(contador==12)
        {
            dialogos.text="Solo la choza te mostrará el maiz real almacenado después de guardarlo, aunque en el baul veas más, no significa que las hayas guardado, asegurate de llevarlo a la choza todo.";
        }else if(contador==13)
        {
            dialogos.text=" Por cada semilla que plantes y coseches de esta clase, te brindará dos semillas para volver a plantar.";
        }
        else if(contador==14)
        {
            dialogos.text="Y ahora, como misión inicial, debes cultivar, cosechar y asar 10 mazorcas, cuando lo consigas, llegará el mercader.";
            buttonAvanza.SetActive(false);

        }
        
    }

    public void nextDialogo()
    {
        contador++;
        SecuenciaDialogos();
    }


    private IEnumerator warningC5()
    {
        yield return new WaitForSeconds(6);
        dialogos.text= "Y cuidado... no dejes mucho los cultivos afuera, hay animales cerca, interactua con la carreta para guardar tus cultivos";
        gameController.car.SetActive(true);  
    }
}
