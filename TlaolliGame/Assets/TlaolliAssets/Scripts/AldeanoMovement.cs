using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AldeanoMovement : MonoBehaviour
{
    public Transform[] waypoints;
    private int currentWaypointIndex = 0;
    private float distanceMin = 0.05f;
    private float speed = 0.5f;

    [SerializeField] private SpriteRenderer spriteRenderer;

    // Variables para ir hacia la choza
    private Transform targetHut;
    private bool goingToHut = false;

    void Update()
    {
        if (goingToHut && targetHut != null)
        {
            MoveToTarget(targetHut);
        }
        else
        {
            movementEnemy();
        }
    }

    // Método para recibir el destino de la choza
    public void SetTarget(Transform hut)
    {
        targetHut = hut;
        goingToHut = true;
    }

    private void movementEnemy()
    {
        if (waypoints.Length == 0) return;

        Transform targetWaypoint = waypoints[currentWaypointIndex];
        MoveToTarget(targetWaypoint);

        if (Vector2.Distance(transform.position, targetWaypoint.position) < distanceMin)
        {
            currentWaypointIndex++;
            if (currentWaypointIndex >= waypoints.Length) currentWaypointIndex = 0;
        }
    }

    // Método compartido para moverse hacia un objetivo (choza o waypoint)
    private void MoveToTarget(Transform target)
    {
        Vector2 direction = target.position - transform.position;

        // Voltear sprite según dirección en X
        spriteRenderer.flipX = direction.x < 0;

        // Movimiento
        transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);

        // Si llega a la choza
        if (goingToHut && Vector2.Distance(transform.position, target.position) < distanceMin)
        {
            goingToHut = false;
            Destroy(gameObject); // 👈 Aquí desaparece el aldeano
        }
    }
}
