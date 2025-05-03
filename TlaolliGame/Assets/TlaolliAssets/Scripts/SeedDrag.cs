using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SeedDrag : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private CanvasGroup canvasGroup;

    [Header("Lógica del juego")]
    public AnimationsMain animationsMain;
    public GameController gameController;

    [Header("Prefab de la semilla fantasma")]
    public GameObject seedGhostPrefab; // Asignar en Inspector
    private GameObject seedGhostInstance;

    private RectTransform canvasRect;
    private Camera uiCamera;

    void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        Canvas canvas = GetComponentInParent<Canvas>();
        canvasRect = canvas.GetComponent<RectTransform>();
        uiCamera = canvas.renderMode == RenderMode.ScreenSpaceOverlay ? null : canvas.worldCamera;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = false;

        // Instanciar semilla fantasma
        seedGhostInstance = Instantiate(seedGhostPrefab, canvasRect);
        seedGhostInstance.transform.SetParent(canvasRect, false);
        seedGhostInstance.transform.SetAsLastSibling();
        seedGhostInstance.transform.localScale = Vector3.one;

        // Efecto ghost visual
        CanvasGroup ghostGroup = seedGhostInstance.GetComponent<CanvasGroup>();
        if (ghostGroup != null)
        {
            ghostGroup.alpha = 0.6f;
            ghostGroup.blocksRaycasts = false;
        }

        // Posicionar correctamente en el canvas
        RectTransform ghostRect = seedGhostInstance.GetComponent<RectTransform>();
        Vector2 localPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvasRect,
            eventData.position,
            uiCamera,
            out localPoint);
        ghostRect.localPosition = localPoint;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (seedGhostInstance != null)
        {
            RectTransform ghostRect = seedGhostInstance.GetComponent<RectTransform>();
            Vector2 localPoint;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                canvasRect,
                eventData.position,
                uiCamera,
                out localPoint);
            ghostRect.localPosition = localPoint;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = true;

        if (seedGhostInstance != null)
        {
            // Convertir posición del mouse a mundo
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(eventData.position);
            worldPos.z = 0;

            RaycastHit2D hit = Physics2D.Raycast(worldPos, Vector2.zero);
            if (hit.collider != null && hit.collider.CompareTag("PlantingZone"))
            {
                Debug.Log("🌱 Semilla plantada.");
                hit.collider.GetComponent<PlantingZone>().PlantSeed(worldPos);
                animationsMain.zoneGreen.Play("GreenColorZone");
            }

            Destroy(seedGhostInstance);
        }
    }
}
