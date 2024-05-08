using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Diagnostics;

public class RandomMovement : MonoBehaviour
{
    private Vector3 startingPosition;
    private Vector3 roamPosition;
    private Rigidbody2D rb; // Se añade un Rigidbody2D para controlar el movimiento físico del enemigo
    private Animator animator;

    public float moveSpeed = 2f; // Velocidad de movimiento del enemigo
    public float roamRadius = 10f; // Radio en el que el enemigo puede deambular

    private void Start()
    {
        startingPosition = transform.position;
        rb = GetComponent<Rigidbody2D>(); // Se obtiene el Rigidbody2D del enemigo
        roamPosition = GetRoamingPosition();
        animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        // Mover al enemigo hacia la posición de deambulación
        Vector2 direction = (roamPosition - transform.position).normalized;
        rb.velocity = direction * moveSpeed;

        // Si el enemigo llega a la posición de deambulación, obtener una nueva posición
        if (Vector3.Distance(transform.position, roamPosition) < 0.5f)
        {
            roamPosition = GetRoamingPosition();
        }


        if (direction != Vector2.zero)
        {
            if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
            {
                if (direction.x > 0) // moviendose a la derecha
                {
                    animator.SetFloat("right", direction.x);
                    animator.SetFloat("left", 0);
                }
                else // moviendose a la izquiera
                {
                    animator.SetFloat("left", -direction.x);
                    animator.SetFloat("right", 0);
                }
                animator.SetFloat("up", 0);
                animator.SetFloat("down", 0);
            }
            else
            {
                if (direction.y > 0) // moviendose enfrente
                {
                    animator.SetFloat("up", direction.y);
                    animator.SetFloat("down", 0);
                }
                else // moviendose hacia atras
                {
                    animator.SetFloat("down", -direction.y);
                    animator.SetFloat("up", 0);
                }
                animator.SetFloat("right", 0);
                animator.SetFloat("left", 0);
            }
        }
        else
        {
            // Se le asigna el valor 0 si no hay movimiento
            animator.SetFloat("up", 0);
            animator.SetFloat("down", 0);
            animator.SetFloat("right", 0);
            animator.SetFloat("left", 0);
        }
    }

    private Vector3 GetRoamingPosition()
    {
        // Obtener una posición aleatoria dentro del radio de deambulación
        Vector3 randomDirection = Random.insideUnitCircle * roamRadius;
        return startingPosition + new Vector3(randomDirection.x, randomDirection.y, 0);
    }
}
