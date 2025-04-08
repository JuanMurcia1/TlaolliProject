using UnityEngine;

public class PlantingZone : MonoBehaviour
{
    public MaizGroth maizGrowth; // Referencia al script MaizGroth

    public void PlantSeed(Vector3 plantPosition)
    {
        if (maizGrowth != null)
        {
            Debug.Log("🌱 Plantando maíz en " + plantPosition);
            maizGrowth.cornInstantiate(plantPosition);
        }
        else
        {
            Debug.LogError("❌ No se asignó MaizGroth en PlantingZone.");
        }
    }
}
