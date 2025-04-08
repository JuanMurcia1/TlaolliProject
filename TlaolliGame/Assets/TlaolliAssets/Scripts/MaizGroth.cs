using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MaizGroth : MonoBehaviour
{
    public SpriteRenderer spriteRenderer; // Asigna el SpriteRenderer del objeto
    public Sprite[] growthStages; // Arrastra aquí los sprites en orden de crecimiento
    public float timeBetweenStages = 3f; // Tiempo entre cada cambio de sprite

    public GameObject corn;
    public List<GameObject> cornGrowthList = new List<GameObject>(); 
    public List<SpriteRenderer> spriteRenderers = new List<SpriteRenderer>();
    private AudioManager audioManager;

    void Start()
    {
        audioManager= FindObjectOfType<AudioManager>();
        
    }

    IEnumerator GrowSequence(SpriteRenderer sr)
    {
        for (int i = 0; i < growthStages.Length; i++)
        {
            sr.sprite = growthStages[i]; 
            yield return new WaitForSeconds(timeBetweenStages); 
        }
    }

public void cornInstantiate(Vector3 plantPosition)
{
    GameObject cornInstantiate = Instantiate(corn, plantPosition, Quaternion.identity);
    cornGrowthList.Add(cornInstantiate);
    audioManager.cornPlantingSound();
    SpriteRenderer sr = cornInstantiate.GetComponent<SpriteRenderer>();
    cornInstantiate.AddComponent<Harvestable>(); // Añadir la funcionalidad de cosecha

    spriteRenderers.Add(sr); 
    StartCoroutine(GrowSequence(sr)); // Iniciar crecimiento
}

public void RemoveFromList(GameObject corn)
{
    cornGrowthList.Remove(corn); // Eliminar de la lista de crecimiento
    spriteRenderers.Remove(corn.GetComponent<SpriteRenderer>()); // Eliminar de la lista de SpriteRenderers
}




}

