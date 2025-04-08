using UnityEngine;
using UnityEngine.EventSystems;

public class SeedDrag : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    private Vector3 startPosition;
    public AnimationsMain animationsMain;
    public GameController gameController;



    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        startPosition = rectTransform.position; // Guarda la posición original
    }


    public void OnBeginDrag(PointerEventData eventData)
    {
        // canvasGroup.alpha = 0.6f; // Hace la semilla semi-transparente al arrastrar
        canvasGroup.blocksRaycasts = false; // Permite que `PlantingZone` la detecte
        transform.SetAsLastSibling();
        
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.position = Input.mousePosition; // Sigue el mouse
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;

        // Convertir la posición de la UI a coordenadas del mundo
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        worldPosition.z = 0; // Mantener la posición en 2D

        // Buscar si la semilla se soltó en `PlantingZone`
        RaycastHit2D hit = Physics2D.Raycast(worldPosition, Vector2.zero);
        if (hit.collider != null && hit.collider.CompareTag("PlantingZone"))
        {
            Debug.Log("🌱 Semilla plantada en la zona.");
            hit.collider.GetComponent<PlantingZone>().PlantSeed(worldPosition);
            rectTransform.position = startPosition;
            animationsMain.zoneGreen.Play("GreenColorZone");
            
            


        }
        else
        {
            
            rectTransform.position= startPosition; // Vuelve a la posición original
        }
    }
}
