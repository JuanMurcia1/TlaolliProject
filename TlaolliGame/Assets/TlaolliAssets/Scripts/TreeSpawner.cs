using UnityEngine;
using System.Collections;

public class TreeSpawner : MonoBehaviour
{
    [Header("Prefabs de árboles")]
    public GameObject[] treePrefabs; 

    [Header("Rango del mapa")]
    private Vector2 spawnAreaMin = new Vector2(-6.41f,5.43f); // Ej: (-10, -10)
    private Vector2 spawnAreaMax= new Vector2(-2.96f,-5.31f); // Ej: (10, 10)

    [Header("Tiempos")]
    private float treeLifetime = 10f;     // Tiempo que permanece un árbol
    private float respawnDelay = 15f;      // Tiempo tras destrucción antes de nuevo spawn

    public  Vector3 spawnPosition;

    private GameController gameController;

    void Start()
    {
        gameController= FindObjectOfType<GameController>();
        //StartCoroutine(SpawnTreesLoop());
    }


    public IEnumerator SpawnTreesLoop()
    {
        while (true)
        {
            // Selecciona un prefab aleatorio
            int index = Random.Range(0, treePrefabs.Length);
            GameObject selectedTreePrefab = treePrefabs[index];

            // Genera posición aleatoria
            spawnPosition = new Vector3(
                Random.Range(spawnAreaMin.x, spawnAreaMax.x),
                Random.Range(spawnAreaMin.y, spawnAreaMax.y),
                0f
            );

            // Instancia árbol
            GameObject treeInstance = Instantiate(selectedTreePrefab, spawnPosition, Quaternion.identity);

            // Espera el tiempo de vida del árbol
            yield return new WaitForSeconds(treeLifetime);

            // Destruye el árbol
            Destroy(treeInstance);
            gameController.woodGenerateAlways();


            // Espera antes de volver a instanciar otro
            yield return new WaitForSeconds(respawnDelay);
        }
    }
}
