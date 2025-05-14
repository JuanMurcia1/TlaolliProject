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
    public Text actuaLevelText;
    public Text actualExperienceText;
    public static int cornCosechado = 0; 
    public static int cornSaved= 0;
    public static int wood = 0;
    public static int granitosDeOro=0;
    public int cornAsado= 0;
    public int level= 0;
    public int actualExperience= 0;
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

    public bool nextLvl= false;
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
    public GameObject chozaGenerateButton;

    public Car carScript;

    public Button buttonFogataSpawn;
    public GameObject mercader;
    private TreeSpawner treeSpawner;

    public GameObject UIlevelBar;

    public GameObject congratsLvl;

    public GameObject textSum1;
   

    public ParticleSystem rain;

    public AnimationsMain animationsMain;

    public GameObject ravenObject;

    public GameObject aldeanoFirst;


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
        level=1;
        UpdateCosechadoText();
        dialogosFarm = FindObjectOfType<DialogosFarm>();
        treeSpawner= FindObjectOfType<TreeSpawner>();
        actuaLevelText.text="" + level.ToString();
        actualExperienceText.text= "" + actualExperience.ToString();
        animationsMain= FindObjectOfType<AnimationsMain>();
    }

    void Update()
    {
        firstCornCosechado();
        lastWoodTake();
        
        
    }

    void LateUpdate()
    {
        nextLevel();
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


    public void woodGenerateAlways()
    {
        for (int i = 0; i < 3; i++)
        {
            woodSpawner();

        }

    }

    // CONTROLER INSTANTIATE WOOD AND TEXT

    public void woodSpawner()
    {
        if(dialogosFarm.contador==2)
        {
        float rangeX = Random.Range(-3.0f,1.21f);
        float rangeY = Random.Range(-3.89f,-2.19f);
        Vector2 positionSpawner= new Vector2(rangeX,rangeY);
        GameObject woodPP= Instantiate(woodPrefab,positionSpawner,Quaternion.identity);

        }else if(dialogosFarm.contador>2)
        {
        float rangeX = Random.Range(-6.41f,-2.96f);
        float rangeY = Random.Range(5.43f,-5.31f);
        Vector2 positionSpawner= new Vector2(rangeX,rangeY);
        GameObject woodPP= Instantiate(woodPrefab,treeSpawner.spawnPosition,Quaternion.identity);
        }
      


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
            wood -= 2;
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
        wood -= 1;
        UpdateWoodText();
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

    public void nextLevel()
    {
       
        if(actualExperience>35 && level==1)
        {
            nextLvl=true;
            level=2;
            actuaLevelText.text="" + level.ToString();
            dialogosFarm.contador=21;
            dialogosFarm.SecuenciaDialogos();
            if(nextLvl==true)
            {
                StartCoroutine(lvlFalse());

            }
            


        }else if(actualExperience> 100 && level==2)
        {
            nextLvl= true;
            level=3;
            actuaLevelText.text="" + level.ToString();
            if(nextLvl==true)
            {
                StartCoroutine(lvlFalse());

            }


        }

    }

    public void experienceSum()
    {
        actualExperienceText.text="" + actualExperience.ToString();
    }

    public IEnumerator lvlFalse()
    {
        congratsLvl.SetActive(true);
        yield return new WaitForSeconds(2);
        nextLvl= false;
        congratsLvl.SetActive(false);
    }


    public IEnumerator rainStart()
    {
        rain.Play();
        yield return new WaitForSeconds(100);
        rain.Stop();
        dialogosFarm.rainSound.Stop();

    }

    public IEnumerator sumChest()
    {
        textSum1.SetActive(true);
        animationsMain.sumPlay();
        yield return new WaitForSeconds(0.5f);
        textSum1.SetActive(false);
        
            
    }

    public void spawnRaven()
    {
        Vector2 positionSpawnRaven = new Vector2(-4.74f,1.16f);
        Instantiate(ravenObject, positionSpawnRaven,Quaternion.identity);
       

    }
    

    



}
