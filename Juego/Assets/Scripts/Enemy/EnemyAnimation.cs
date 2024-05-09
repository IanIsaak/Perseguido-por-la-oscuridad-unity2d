using UnityEngine;

public class DirectionalAnimation : MonoBehaviour
{
    private Rigidbody2D rb; // Rigidbody2D para controlar el movimiento
    private Animator animator;

    private void Start()
    {
        rb = GetComponentInParent<Rigidbody2D>(); // Obtener el Rigidbody2D
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        // Obtener la dirección del movimiento del Rigidbody2D
        Vector2 direction = rb.velocity.normalized;

        if (direction != Vector2.zero)
        {
            if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
            {
                if (direction.x > 0) // moviéndose a la derecha
                {
                    animator.SetFloat("right", direction.x);
                    animator.SetFloat("left", 0);
                }
                else // moviéndose a la izquierda
                {
                    animator.SetFloat("left", -direction.x);
                    animator.SetFloat("right", 0);
                }
                animator.SetFloat("up", 0);
                animator.SetFloat("down", 0);
            }
            else
            {
                if (direction.y > 0) // moviéndose hacia adelante
                {
                    animator.SetFloat("up", direction.y);
                    animator.SetFloat("down", 0);
                }
                else // moviéndose hacia atrás
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
            // Si no hay movimiento, se asigna el valor 0 a todas las animaciones
            animator.SetFloat("up", 0);
            animator.SetFloat("down", 0);
            animator.SetFloat("right", 0);
            animator.SetFloat("left", 0);
        }
    }
}
