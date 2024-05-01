using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementTopDown : MonoBehaviour
{
    public Joystick joystick;
    [SerializeField] private float movementSpeed;
    private Rigidbody2D rb2D;
    private Animator animator;

    private void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        Vector2 direction = joystick.Direction;
        rb2D.MovePosition(rb2D.position + direction * movementSpeed * Time.fixedDeltaTime);

        // Se asignan los parametros para el animator segun la poscion del joystick
        if (direction != Vector2.zero)
        {
            if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
            {
                if (direction.x > 0) // moviendose a la derecha
                {
                    animator.SetFloat("right", direction.x);
                }
                else // moviendose a la izquiera
                {
                    animator.SetFloat("left", -direction.x);
                }
                animator.SetFloat("front", 0);
                animator.SetFloat("back", 0);
            }
            else
            {
                if (direction.y > 0) // moviendose enfrente
                {
                    animator.SetFloat("front", direction.y);
                }
                else // moviendose hacia atras
                {
                    animator.SetFloat("back", -direction.y);
                }
                animator.SetFloat("right", 0);
                animator.SetFloat("left", 0);
            }
        }
        else
        {
            // Se le asigna el valor 0 si no hay movimiento
            animator.SetFloat("front", 0);
            animator.SetFloat("back", 0);
            animator.SetFloat("right", 0);
            animator.SetFloat("left", 0);
        }
    }
}
