using UnityEngine;

public class CameraDragController : MonoBehaviour
{
    public float dragSpeed = 0.1f;

    private Vector3 lastTouchPosition;
    private bool isTouchDragging = false;

    public Vector2 minPosition = new Vector2(-20, 0); // solo X se usa ahora
    public Vector2 maxPosition = new Vector2(20, 0);

    private AnimationsMain animationsMain;
    private float fixedZ; // guardará la posición Z inicial

    void Start()
    {
        animationsMain = FindObjectOfType<AnimationsMain>();
        fixedZ = transform.position.z; // mantener Z fijo desde el inicio
    }

    void Update()
    {
        if (animationsMain != null && animationsMain.isInventoryOpen) return;

        HandleMouseDrag();
        HandleTouchDrag();

        ClampPosition();
    }

    void HandleMouseDrag()
    {
        if (Input.GetMouseButtonDown(0))
        {
            lastTouchPosition = Input.mousePosition;
            isTouchDragging = true;
        }
        else if (Input.GetMouseButton(0) && isTouchDragging)
        {
            Vector3 delta = Input.mousePosition - lastTouchPosition;
            float moveX = -delta.x * dragSpeed * Time.deltaTime;
            transform.Translate(new Vector3(moveX, 0, 0), Space.World);
            lastTouchPosition = Input.mousePosition;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            isTouchDragging = false;
        }
    }

    void HandleTouchDrag()
    {
        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                lastTouchPosition = touch.position;
                isTouchDragging = true;
            }
            else if (touch.phase == TouchPhase.Moved && isTouchDragging)
            {
                Vector2 delta = touch.position - (Vector2)lastTouchPosition;
                float moveX = -delta.x * dragSpeed * Time.deltaTime;
                transform.Translate(new Vector3(moveX, 0, 0), Space.World);
                lastTouchPosition = touch.position;
            }
            else if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
            {
                isTouchDragging = false;
            }
        }
    }

    void ClampPosition()
    {
        Vector3 pos = transform.position;
        pos.x = Mathf.Clamp(pos.x, minPosition.x, maxPosition.x);
        pos.z = fixedZ; // mantiene Z fijo
        transform.position = pos;
    }
}
