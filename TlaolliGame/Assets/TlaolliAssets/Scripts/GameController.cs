using UnityEngine;
using UnityEngine.UI;  
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Rendering.Universal;

public class GameController : MonoBehaviour
{
    public Text granitoDeOROT;
    public Text cosechadoText; 
    public Text woodT;
    public Text menuCosechadoText;
    public Text menuWoodT;
    public Text actualChozaText;
    public Text cornGrilledT;
    public static int cornCosechado = 0; 
    public static int cornSaved= 0;
    public static int wood = 0;
    public static int granitosDeOro=0;

    public int cornAsado= 0;
    public static GameController instance;
    public GameObject woodPrefab;
    public List<GameObject> woodGrowthList = new List<GameObject>(); 
    private bool fisrtCornDestroyed = false;
    private bool lastLogTake = false;
    private bool menuMaterialOpen = false;
    private bool menuConstruccionOpen = false;
    private bool openFirstTimeMenu= false;
    private bool firstChozaBuild= false;
    public bool spawnChoza= false;
    public DialogosFarm dialogosFarm;
    public GameObject ButtonMenuMaterial;
    public GameObject menuMaterial;
    public GameObject choza;
    public GameObject boxCorns;
    public GameObject[] UiTextShutDown;
    public GameObject buttonMenuConstruccion;
    public GameObject menuConstruccion;
    public GameObject textoChoza;
    
    public GameObject car;

    public GameObject fogata;
    public GameObject fogataButton;

    public Car carScript;

    public Button buttonFogataSpawn;
    public GameObject mercader;



    void Awake()
    {
        
        if (instance == null)
        {
            instance = this;  // Si no hay otra instancia, asignamos la instancia actual
            DontDestroyOnLoad(gameObject); // Para que no se destruya entre escenas
        }
        else
        {
            Destroy(gameObject); // Si ya hay una instancia, destruye este objeto
        }
    }

    void Start()
    {
        UpdateCosechadoText();
        dialogosFarm = FindObjectOfType<DialogosFarm>();
        
        
    }

    void Update()
    {
        firstCornCosechado();
        lastWoodTake();
        
        
    }


    public void IncrementarCosechado()
    {
        cornCosechado++; 
        UpdateCosechadoText(); 
    }

    // Método para actualizar el texto de las semillas cosechadas en la UI
    public void UpdateCosechadoText()
    {
        cosechadoText.text = "" + cornCosechado.ToString();
        menuCosechadoText.text=""+ cornCosechado.ToString();
       
        
        if(spawnChoza & carScript.carretaMoving )
        {
            cornSaved += cornCosechado;
            cornCosechado=0;
            actualChozaText= textoChoza.GetComponent<Text>();
            menuCosechadoText.text=""+ cornCosechado.ToString();
            actualChozaText.text="Actual: "+ cornSaved.ToString();
            carScript.carretaMoving=false;

        }
        
       
    }

    public void firstCornCosechado()
    {
        if(cornCosechado==1)
        {
            if(!fisrtCornDestroyed)
            {
                dialogosFarm.contador=2;
                dialogosFarm.SecuenciaDialogos();
                for (int i = 0; i < 3; i++)
                {
                    woodSpawner();

                }
                fisrtCornDestroyed=true;
            }
        }
    }

    // CONTROLER INSTANTIATE WOOD AND TEXT

    public void woodSpawner()
    {
        float rangeX = Random.Range(-3.0f,1.21f);
        float rangeY = Random.Range(-3.89f,-2.19f);
        Vector2 positionSpawner= new Vector2(rangeX,rangeY);
        GameObject woodPP= Instantiate(woodPrefab,positionSpawner,Quaternion.identity);
    }
    public void UpdateWoodText()
    {
        woodT.text = "" + wood.ToString();
        menuWoodT.text="" + wood.ToString();
        
    }

    

     public void lastWoodTake()
    {
        if(wood==3)
        {
            if(!lastLogTake)
            {
                dialogosFarm.contador=3;
                dialogosFarm.SecuenciaDialogos();

            
                lastLogTake= true;
            }
        }
    }

     public void UpdateCornGrilledText()
    {
        
       cornGrilledT.text="" + cornAsado.ToString();
        
    }

    

    public void openMenuMaterial()
    {
        if(!menuMaterialOpen)
        {
            menuMaterial.SetActive(true);
            menuMaterialOpen=true;
            menuConstruccion.SetActive(false);
            menuConstruccionOpen= false;
            if(!openFirstTimeMenu)
            {
                dialogosFarm.contador=4;
                dialogosFarm.SecuenciaDialogos();
                openFirstTimeMenu= true;
            }

        }else
        {
            menuMaterial.SetActive(false);
            menuMaterialOpen= false;

        }
        
    }
    public void openMenuConstruccion()
    {
        if(!menuConstruccionOpen)
        {
            menuConstruccion.SetActive(true);
            menuConstruccionOpen=true;
            menuMaterial.SetActive(false);
            menuMaterialOpen= false;
            
        }else
        {
            menuConstruccion.SetActive(false);
            menuConstruccionOpen= false;
        }
    }

    public void chozaBuilder()
    {
        if(wood>=3)
        {
            Vector2 positionChoza= new Vector2(-1.8f,4.22f);
            Instantiate(choza,positionChoza,Quaternion.identity);
            wood -= 3;
            UpdateWoodText();
            textoChoza= GameObject.Find("TextActualCapacity");
            textoChoza.GetComponent<Text>();
            spawnChoza= true;
        if(!firstChozaBuild)
        {
            dialogosFarm.contador=5;
            dialogosFarm.SecuenciaDialogos();
            firstChozaBuild= true;
            
        }

        }
      
        
    }

 

    public IEnumerator  UiTextOff()
    {
        yield return new WaitForSeconds(6);
        UiTextShutDown[0].SetActive(false);
        UiTextShutDown[1].SetActive(false);
        
    }

    public void spawnFogata()
    {
        fogata.SetActive(true);
        buttonFogataSpawn.interactable= false;
        dialogosFarm.contador=7;
        dialogosFarm.SecuenciaDialogos();
    }

    public IEnumerator mercaderDrop()
    {
        yield return new WaitForSeconds(3);
        mercader.SetActive(true);

    }

}
