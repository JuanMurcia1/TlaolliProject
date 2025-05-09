using UnityEngine;
using UnityEngine.EventSystems;

public class HoverRevealer : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject requerimientoChoza; // El objeto que se activa al pasar el mouse

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (requerimientoChoza != null)
            requerimientoChoza.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (requerimientoChoza != null)
            requerimientoChoza.SetActive(false);
    }
}
