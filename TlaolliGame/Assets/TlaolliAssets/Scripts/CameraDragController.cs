using UnityEngine;

public class CameraDragController : MonoBehaviour
{
    public float dragSpeed = 0.1f;

    private Vector3 lastPosition;
    private bool isDragging = false;

    public Vector2 minPosition = new Vector2(-20, -10); // ajusta según tu mapa
    public Vector2 maxPosition = new Vector2(20, 10);    // ajusta según tu mapa
    
    private AnimationsMain animationsMain;

    void Start()
    {
        animationsMain= FindObjectOfType<AnimationsMain>();
    }

    void Update()
    {
#if UNITY_EDITOR || UNITY_STANDALONE
        HandleMouseDrag();
#elif UNITY_ANDROID || UNITY_IOS
        HandleTouchDrag();
#endif

        ClampPosition();
    }

    void HandleMouseDrag()
    {
        if (Input.GetMouseButtonDown(0))
        {
            lastPosition = Input.mousePosition;
            isDragging = true;
        }
        else if (Input.GetMouseButton(0) && isDragging && animationsMain.isInventoryOpen==false)
        {
            Vector3 delta = Input.mousePosition - lastPosition;
            Vector3 move = new Vector3(-delta.x, 0, -delta.y) * dragSpeed * Time.deltaTime;
            transform.Translate(move);
            lastPosition = Input.mousePosition;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
        }
    }

    void HandleTouchDrag()
    {
        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Moved)
            {
                Vector2 delta = touch.deltaPosition;
                Vector3 move = new Vector3(-delta.x, 0, -delta.y) * dragSpeed * Time.deltaTime;
                transform.Translate(move);
            }
        }
    }

    void ClampPosition()
    {
        Vector3 clamped = transform.position;
        clamped.x = Mathf.Clamp(clamped.x, minPosition.x, maxPosition.x);
        clamped.y = Mathf.Clamp(clamped.y, minPosition.y, maxPosition.y);
        transform.position = clamped;
    }
}
