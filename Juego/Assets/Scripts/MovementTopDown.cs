using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MovementTopDown : MonoBehaviour
{
    public Joystick joystick;
    private Rigidbody2D rb2D;
    private Animator animator;

    [Header("Sprint")]
    [SerializeField] private float movementSpeedStandar;
    [SerializeField] private float movementSprint;
    [SerializeField] private float timeSprint;
    private float sprintCooldownTime;
    [SerializeField] private float timeBetweenSprint;
    private bool canSprint = true;
    private bool isSprinting = false;
    private float movementSpeed;
    private SprintMovement sprintMovement;

    private void Awake()
    {
        sprintMovement = new SprintMovement();
        sprintMovement.Player.StartSprint.performed += OnStartSprint;
        sprintMovement.Player.StopSprint.performed += OnStopSprint;
    }

    private void OnEnable()
    {
        sprintMovement.Enable();
    }

    private void OnDisable()
    {
        sprintMovement.Disable();
    }

    private void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        movementSpeed = movementSpeedStandar;
        sprintCooldownTime = timeSprint;
    }

    private void Update()
    {
        // Reducir el tiempo del sprint si está corriendo
        if (isSprinting)
        {
            if (sprintCooldownTime > 0)
            {
                sprintCooldownTime -= Time.deltaTime;
            }
            else
            {
                StopSprint();
                canSprint = false;
                StartCoroutine(RecoverSprint());
            }
        }

        // Actualizar la velocidad de movimiento según el estado del sprint
        if (!isSprinting && sprintCooldownTime < timeSprint)
        {
            sprintCooldownTime = Mathf.Min(sprintCooldownTime + Time.deltaTime, timeSprint);
        }
    }

    private void FixedUpdate()
    {
        Vector2 direction = joystick.Direction;
        rb2D.MovePosition(rb2D.position + direction * movementSpeed * Time.fixedDeltaTime);

        // Inicializar valores para el Animator
        float front = 0f, back = 0f, right = 0f, left = 0f;

        // Asignar los parámetros para el Animator según la posición del joystick
        if (direction != Vector2.zero)
        {
            if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
            {
                if (direction.x > 0) // Moviéndose a la derecha
                {
                    animator.SetFloat("right", direction.x);
                    right = direction.x;
                }
                else // Moviéndose a la izquierda
                {
                    animator.SetFloat("left", -direction.x);
                    left = -direction.x;
                }
                animator.SetFloat("front", 0);
                animator.SetFloat("back", 0);
            }
            else
            {
                if (direction.y > 0) // Moviéndose enfrente
                {
                    animator.SetFloat("front", direction.y);
                    front = direction.y;
                }
                else // Moviéndose hacia atrás
                {
                    animator.SetFloat("back", -direction.y);
                    back = -direction.y;
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

        // Asignar los valores al Animator
        animator.SetFloat("front", front);
        animator.SetFloat("back", back);
        animator.SetFloat("right", right);
        animator.SetFloat("left", left);
    }

    private void OnStartSprint(InputAction.CallbackContext context)
    {
        if (canSprint)
        {
            StartSprint();
        }
    }

    private void OnStopSprint(InputAction.CallbackContext context)
    {
        StopSprint();
    }

    private void StartSprint()
    {
        movementSpeed = movementSprint;
        isSprinting = true;
    }

    private void StopSprint()
    {
        movementSpeed = movementSpeedStandar;
        isSprinting = false;
    }

    private IEnumerator RecoverSprint()
    {
        yield return new WaitForSeconds(timeBetweenSprint);
        canSprint = true;
        sprintCooldownTime = timeSprint;
    }
}