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
    [SerializeField] private float timeBetweenSprint;
    [SerializeField] private Stamina stamina;
    private float sprintCooldownTime;
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
        stamina.InitializeStamina(timeSprint);
    }

    private void Update()
    {
        // Reducir el tiempo del sprint si está corriendo
        if (isSprinting)
        {
            if (sprintCooldownTime > 0)
            {
                float staminaReduction = Time.deltaTime;
                sprintCooldownTime -= staminaReduction;
                stamina.ChangeStamina(-staminaReduction); // Reducir stamina progresivamente
            }
            else
            {
                StopSprint();
                canSprint = false;
                StartCoroutine(RecoverSprint());
            }
        }

        // Recuperar stamina si no se está corriendo
        if (!isSprinting && sprintCooldownTime < timeSprint)
        {
            float staminaRecovery = Time.deltaTime * (timeSprint / timeBetweenSprint);
            sprintCooldownTime = Mathf.Min(sprintCooldownTime + staminaRecovery, timeSprint);
            stamina.ChangeStamina(staminaRecovery); // Recuperar stamina progresivamente
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