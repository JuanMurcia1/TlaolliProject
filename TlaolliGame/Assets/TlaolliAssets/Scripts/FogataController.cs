using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class FogataController : MonoBehaviour
{
    public Sprite[] stagesFire;
    private SpriteRenderer spriteFogata;
    public float timeBetweenStages = 0.01f;
    public GameObject fogatinha;
    public Light2D light2D;
    public float minRadius = 1f;
    public float maxRadius = 3f;
    public float speed = 1f;
    private bool increasing = true;
    public int cornToAsar= 0;

    public GameObject panelAsarCorn;

    public Text cornToAsarText;
    public Text letreroPanelCornToAsar;
    public GameController gameController;
    public DialogosFarm dialogosFarm;

    private bool firstCornAsado = false;
    private bool noHaySuficientes= false;

    void Start()
    {
        fogatinha = GameObject.Find("Fogata");
        light2D = GetComponentInChildren<Light2D>();
        spriteFogata = fogatinha.GetComponent<SpriteRenderer>();
        InvokeRepeating("PlayGrowSequenceFogata", 1f, 1.5f);
    }
    void Update()
    {
        lightFogata();
    }

    void PlayGrowSequenceFogata()
    {
        StartCoroutine(GrowSequenceFogata(spriteFogata));
    }

    public void OnMouseDown()
    {
        panelAsarCorn.SetActive(true);

    }

    public void ClosePanelAsarCorn()
    {
        panelAsarCorn.SetActive(false);
        cornToAsarText.text ="0";
    }

    public void plusCorn()
    {
       
        cornToAsar++;
        cornToAsarText.text= ""+ cornToAsar.ToString();
    }

     public void minusCorn()
    {
        cornToAsar--;
        cornToAsarText.text= ""+ cornToAsar.ToString();
    }

    public void checkAsar()
    {
        if(cornToAsar<=GameController.cornSaved&& cornToAsar>0)
        {
            gameController.cornAsado+= cornToAsar;
            GameController.cornSaved-=cornToAsar;

            if(gameController.spawnChoza)
            {
            // gameController.actualChozaText= textoChoza.GetComponent<Text>();
                gameController.actualChozaText.text="Actual: "+ GameController.cornSaved.ToString();

            }
            gameController.UpdateCornGrilledText();
            panelAsarCorn.SetActive(false);
            cornToAsarText.text ="0";
            gameController.UpdateCosechadoText();
            noHaySuficientes=false;
            cornToAsar=0;
        }else
        {
            letreroPanelCornToAsar.text= "No hay suficientes";
            noHaySuficientes=true;
            StartCoroutine(messageInitialPanelCornGrilled());
            
            
        }
        if(!firstCornAsado&& !noHaySuficientes)
        {
            dialogosFarm.contador=8;
            dialogosFarm.SecuenciaDialogos();
            firstCornAsado=true;
        }

        if (gameController.cornAsado>=10)
        {
            dialogosFarm.contador=15;
            dialogosFarm.SecuenciaDialogos();
        }
        
        

        
    }

    IEnumerator GrowSequenceFogata(SpriteRenderer sr)
    {
        for (int i = 0; i < stagesFire.Length; i++)
        {
            sr.sprite = stagesFire[i];
            yield return new WaitForSeconds(timeBetweenStages);
        }
    }

     public void lightFogata()
    {
        float current = light2D.pointLightOuterRadius;

        if (increasing)
        {
            current += Time.deltaTime * speed;
            if (current >= maxRadius)
            {
                current = maxRadius;
                increasing = false;
            }
        }
        else
        {
            current -= Time.deltaTime * speed;
            if (current <= minRadius)
            {
                current = minRadius;
                increasing = true;
            }
        }

        light2D.pointLightOuterRadius = current;
    }

    IEnumerator messageInitialPanelCornGrilled()
    {
         yield return new WaitForSeconds(2);
        letreroPanelCornToAsar.text="¿Cuántas mazorcas quieres asar?";
       
       
        
    }
}
