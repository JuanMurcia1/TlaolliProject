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
        cornToAsar=0;
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
}
