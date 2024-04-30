using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float distance = 5f;

    void LateUpdate()
    {
        if (target != null)
        {
            // Obtiene la posición actual del jugador
            Vector3 playerPos = target.position;

            // Calcula la nueva posición de la cámara
            Vector3 newPosition = playerPos - transform.forward * distance;

            // Actualiza la posición de la cámara
            transform.position = newPosition;

            // Mantiene la rotación de la cámara para que siempre esté mirando al jugador
            transform.LookAt(playerPos);
        }
    }

}
