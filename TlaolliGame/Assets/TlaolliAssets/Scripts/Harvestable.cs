using UnityEngine;

public class Harvestable : MonoBehaviour
{
    private MaizGroth maizGrothScript; 
    private AudioManager audioManager;
    private GameController gameController;
    

    private void Start()
    {
        
        maizGrothScript = FindObjectOfType<MaizGroth>();
        audioManager= FindObjectOfType<AudioManager>();
        gameController= FindObjectOfType<GameController>();
    }

    public void OnMouseDown()
    {
       
        if (gameObject.CompareTag("Semilla"))
        {
            maizGrothScript.cornSembradoActual--;
            maizGrothScript.semillas1+=2;
            gameController.boxCorns.SetActive(true);
            audioManager.cornHarvest();
            GameController.cornCosechado++; // O puedes llamar GameManager.Instance.IncrementarCosechado() si lo haces a través de una instancia

            // Actualizar el texto de semillas cosechadas
            GameController.instance.UpdateCosechadoText();  // Actualiza el texto en la UI
            maizGrothScript.RemoveFromList(gameObject);
            GameController.instance.actualExperience++;
            GameController.instance.experienceSum();
            

            Destroy(gameObject); 
        }

    }
}
