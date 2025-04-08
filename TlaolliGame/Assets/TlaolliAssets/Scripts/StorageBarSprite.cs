using UnityEngine;

public class StorageBarSprite : MonoBehaviour
{
    public Transform barFill; // Asigna el objeto hijo del sprite
    public float currentStorage = 0f;
    public float maxStorage = 22f;

    private Vector3 fullScale = new Vector3(0.4f, 0.1f, 1f); // Escala máxima cuando está llena

    void Update()
    {
        float fillPercent = Mathf.Clamp01(currentStorage / maxStorage);
       barFill.localScale = new Vector3(fullScale.x * fillPercent, fullScale.y, fullScale.z);
       

    }

    public void AddStorage(float amount)
    {
        currentStorage += amount;
        currentStorage = Mathf.Clamp(currentStorage, 0, maxStorage);
    }
}
