using UnityEngine;

public class GridManager : MonoBehaviour
{
    [Header("Grilla")]
    public int width = 20;
    public int height = 20;
    public float cellSize = 1f;
    private bool[,] gridOccupied;

    [Header("Prefabs")]
    public GameObject buildingPrefab;
    public GameObject ghostPrefab;

    public GameObject currentGhost;

    public bool isInBuildMode = false;

    public GameObject aldeano;
[SerializeField] private Vector3 originPosition = new Vector3(-4.82f, -5.33f, 0f);
[Header("Restricciones")]
public LayerMask blockedLayers;





    void Awake()
    {
        gridOccupied = new bool[width, height];
    }

  

void Update()
{
    if (isInBuildMode)
    {
        Vector3 mouseWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorld.z = 0;
        Vector2Int gridPos = WorldToGrid(mouseWorld);
        Vector3 snappedPos = GridToWorld(gridPos);

        currentGhost.transform.position = snappedPos;

        // Verificar si la celda está libre en la lógica de la grilla
        bool isCellFree = IsCellFree(gridPos);

        // Verificar si hay algo físico (tilemap, objeto, otro edificio)
        bool isBlocked = IsPhysicallyBlocked(snappedPos);

        // Solo es válido si está libre y no bloqueado físicamente
        bool isValid = isCellFree && !isBlocked;

        SetGhostColor(isValid);

        if (Input.GetMouseButtonDown(0) && isValid&& GameController.wood>=10)
        {
            GameObject refugee =Instantiate(buildingPrefab, snappedPos, Quaternion.identity);
            GameController.wood-=10;
            GameController.instance.UpdateWoodText();
            OccupyCell(gridPos);
            Destroy(currentGhost);
            isInBuildMode = false;
            AldeanoMovement movimiento = aldeano.GetComponent<AldeanoMovement>();
            movimiento.SetTarget(refugee.transform);
            
        }
    }
}


    private bool IsPhysicallyBlocked(Vector3 worldPosition)
{
    Vector2 center = worldPosition;
    Vector2 size = Vector2.one * cellSize * 0.9f; // levemente más pequeño para evitar bordes

    Collider2D hit = Physics2D.OverlapBox(center, size, 0f, blockedLayers);
    return hit != null;
}


   
  public void ActivateBuildMode()
{
    if (isInBuildMode)
    {
        // Si ya estaba en modo construcción, cancela
        isInBuildMode = false;
        if (currentGhost != null)
        {
            Destroy(currentGhost);
        }
    }
    else
    {
        // Si no estaba en modo construcción, activa y crea ghost
        currentGhost = Instantiate(ghostPrefab);
        isInBuildMode = true;
    }
}


    // 🔄 Conversión de coordenadas
public Vector2Int WorldToGrid(Vector3 worldPos)
{
    Vector3 localPos = worldPos - originPosition;
    int x = Mathf.FloorToInt(localPos.x / cellSize);
    int y = Mathf.FloorToInt(localPos.y / cellSize);
    return new Vector2Int(x, y);
}


    public Vector3 GridToWorld(Vector2Int gridPos)
    {
        return originPosition + new Vector3(gridPos.x * cellSize + cellSize / 2f, gridPos.y * cellSize + cellSize / 2f, 0);
    }

    // 🧱 Lógica de ocupación
    public bool IsCellFree(Vector2Int gridPos)
    {
        if (gridPos.x < 0 || gridPos.x >= width || gridPos.y < 0 || gridPos.y >= height)
            return false;
        return !gridOccupied[gridPos.x, gridPos.y];
    }

    public void OccupyCell(Vector2Int gridPos)
    {
        if (gridPos.x >= 0 && gridPos.x < width && gridPos.y >= 0 && gridPos.y < height)
            gridOccupied[gridPos.x, gridPos.y] = true;
    }

    private void SetGhostColor(bool isValid)
    {
        Color color = isValid ? new Color(0, 1, 0, 0.5f) : new Color(1, 0, 0, 0.5f);
        SpriteRenderer[] renderers = currentGhost.GetComponentsInChildren<SpriteRenderer>();
        foreach (var renderer in renderers)
        {
            renderer.color = color;
        }
    }

 void OnDrawGizmos()
    {
        Gizmos.color = Color.gray;

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Vector3 cellCenter = originPosition + new Vector3(x * cellSize + cellSize / 2f, y * cellSize + cellSize / 2f, 0);
                Gizmos.DrawWireCube(cellCenter, Vector3.one * cellSize);
            }
        }
    }

}