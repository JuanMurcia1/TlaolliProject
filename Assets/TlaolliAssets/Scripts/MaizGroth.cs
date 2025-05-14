using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MaizGroth : MonoBehaviour
{
    public SpriteRenderer spriteRenderer; // Asigna el SpriteRenderer del objeto
    public Sprite[] growthStages; // Arrastra aquí los sprites en orden de crecimiento
    public float timeBetweenStages = 3f; // Tiempo entre cada cambio de sprite

    public GameObject corn;
    public List<GameObject> cornGrowthList = new List<GameObject>(); 
    public List<SpriteRenderer> spriteRenderers = new List<SpriteRenderer>();
    private AudioManager audioManager;
    public int cornSembradoActual= 0;
    public int semillas1=1;
    public Text semillasCantidad;

    void Start()
    {
        audioManager= FindObjectOfType<AudioManager>();
    }

    void Update()
    {
        semillasCantidad.text=""+ semillas1.ToString();;
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
    if(semillas1>0)
    {
    cornSembradoActual++;
    GameObject cornInstantiate = Instantiate(corn, plantPosition, Quaternion.identity);
    cornGrowthList.Add(cornInstantiate);
    audioManager.cornPlantingSound();
    SpriteRenderer sr = cornInstantiate.GetComponent<SpriteRenderer>();
    cornInstantiate.AddComponent<Harvestable>(); // Añadir la funcionalidad de cosecha

    spriteRenderers.Add(sr); 
    StartCoroutine(GrowSequence(sr));
    semillas1--;
    semillasCantidad.text=""+ semillas1;

    }
    
}

public void EliminarSembrados(int cantidad)
{
    // Aseguramos no eliminar más de los que existen
    int cantidadReal = Mathf.Min(cantidad, cornGrowthList.Count);

    // Usamos una lista temporal para evitar problemas al modificar la lista original
    List<GameObject> maicesAEliminar = cornGrowthList.GetRange(0, cantidadReal);

    foreach (GameObject maiz in maicesAEliminar)
    {
        if (maiz != null)
        {
            Destroy(maiz);
        }
    }

    // Eliminamos solo los que destruimos
    cornGrowthList.RemoveRange(0, cantidadReal);
    cornSembradoActual -= cantidadReal;

    Debug.Log($" Eliminados {cantidadReal} maíces sembrados (porcentaje).");
}



public void RemoveFromList(GameObject corn)
{
    cornGrowthList.Remove(corn); // Eliminar de la lista de crecimiento
    spriteRenderers.Remove(corn.GetComponent<SpriteRenderer>()); // Eliminar de la lista de SpriteRenderers
}




}

