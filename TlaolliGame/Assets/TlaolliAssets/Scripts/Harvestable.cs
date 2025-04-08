using UnityEngine;

public class Harvestable : MonoBehaviour
{
    private MaizGroth maizGrothScript; // Referencia al script MaizGroth
    private AudioManager audioManager;
    private StorageBarSprite storageBarSprite;
    private GameController gameController;
    

    private void Start()
    {
        // Obtener referencia al script MaizGroth
        maizGrothScript = FindObjectOfType<MaizGroth>();
        storageBarSprite = FindObjectOfType<StorageBarSprite>();
        audioManager= FindObjectOfType<AudioManager>();
        gameController= FindObjectOfType<GameController>();
    }

    public void OnMouseDown()
    {
        // Verificar si el objeto tiene el tag "Semilla"
        if (gameObject.CompareTag("Semilla"))
        {
            gameController.boxCorns.SetActive(true);

            audioManager.cornHarvest();
            // Aquí puedes definir lo que pasa cuando el maíz es cosechado
            Debug.Log("¡Maíz cosechado!");

            // Llamar al GameManager para incrementar el contador
            GameController.cornCosechado++; // O puedes llamar GameManager.Instance.IncrementarCosechado() si lo haces a través de una instancia

            // Actualizar el texto de semillas cosechadas
            GameController.instance.UpdateCosechadoText();  // Actualiza el texto en la UI

            // Eliminar de las listas y destruir el objeto
            maizGrothScript.RemoveFromList(gameObject);
            Destroy(gameObject); // Elimina el maíz del juego

           // storageBarSprite.AddStorage(1f);
           // Debug.Log(storageBarSprite.currentStorage);

           
        }
        else
        {
           
            Debug.Log("Este objeto no es cosechable.");
        }

    }
}
