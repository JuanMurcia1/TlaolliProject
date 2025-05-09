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
    private DayNightController dayNightController;
    public GameObject buttonAvanza;

    private TreeSpawner treeSpawner;

    // Start is called before the first frame update
    void Start()
    {
        contador=0;
        animationsMain.panelDialogos();
        SecuenciaDialogos();
        gameController= FindObjectOfType<GameController>();
        dayNightController= FindObjectOfType<DayNightController>();
        treeSpawner= FindObjectOfType<TreeSpawner>();
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
            dialogos.text="Cuando coseches un maiz, en el baul podrás ver la cantidad, pero solo cuando interactues con la carreta, la choza mostrará lo que almacenaste.";
        }
        else if(contador==10)
        {
            dialogos.text="¡Recuerda!, solo puedes asar si has almacenano maiz en tu choza. Presiona Avanza ";
        }else if(contador==11)
        {
            dialogos.text="Si no almacenas tu maiz, podrá ser robado por algunos animales, ten cuidado en las noches.";
            
        }else if(contador==12)
        {
            dialogos.text="La choza te mostrará el maiz real almacenado después de guardarlo, aunque en el baul veas mazorcas, no significa que las hayas guardado, asegurate de llevarlo todo a la choza.";
        }else if(contador==13)
        {
            dialogos.text=" Por cada semilla que plantes y coseches de esta clase, te brindará dos semillas para volver a plantar.";
        }
        else if(contador==14)
        {
            dialogos.text="Y ahora, como misión inicial, debes cultivar, cosechar y asar 10 mazorcas, cuando lo consigas, llegará el mercader.";
            buttonAvanza.SetActive(false);

        }else if(contador==15)
        {
            dialogos.text="Prepárate, ya viene el mercader";
            gameController.StartCoroutine(gameController.mercaderDrop());
        }else if(contador==16)
        {
            dialogos.text="Interactua con el mercader para ver lo que ofrece.";
            buttonAvanza.SetActive(false);  
        }else if(contador==17)
        {
            dialogos.text="El mercader vendrá eventualmente y arriba a la derecha podrás ver tus recursos.";
            buttonAvanza.SetActive(true); 
            gameController.mercader.SetActive(false);
            
            
        }else if(contador==18)
        {
            dialogos.text="Muy bien, parece que se está haciendo de noche, ten cuidado, no solo tu aldea necesita el maiz.";
            StartCoroutine(dayNightController.CycleDayNight());
            buttonAvanza.SetActive(false);

        }else if(contador== 19)
        {
            dialogos.text="Reune recursos y ten cuidado con las amenazas, pronto tu comunidad crecerá, hay árboles por la zona, reune madera cuando estos se caigan.";
            buttonAvanza.SetActive(true);
        }else if(contador==20)
        {
            dialogos.text="Ahora podrás ver el nivel de tu aldea, y los puntos de experiencia, cosecha un poco más y reune madera, seguramente alguien llegará a tu aldea.";
            
            StartCoroutine(animationsMain.fadedUpPanel());
            buttonAvanza.SetActive(false);
            StartCoroutine(treeSpawner.SpawnTreesLoop());
            gameController.UIlevelBar.SetActive(true);  
        }else if(contador==21)
        {
            animationsMain.panelDialogos();
            dialogos.text="Perfecto, has alcanzado el nivel 2, y tal parece que nos visita alguien buscando refugio, rápido, construye una choza para tu visitante.";
            StartCoroutine(animationsMain.fadedUpPanel());
            gameController.chozaGenerateButton.SetActive(true);
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
