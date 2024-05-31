using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class MovementTopDown : MonoBehaviour
{
    public FieldOfView fov;
    public Joystick joystick;
    private Rigidbody2D rb2D;
    private Animator animator;
    private WalkSound walkSound;

    [Header("Sprint")]
    [SerializeField] private float movementSpeedStandar = 5f;
    [SerializeField] private float movementSprint = 8f;
    [SerializeField] private float timeSprint = 2f;
    [SerializeField] private float timeBetweenSprint = 5f;
    [SerializeField] private Stamina stamina;
    private float sprintCooldownTime;
    private bool canSprint = true;
    private bool isSprinting = false;
    private float movementSpeed;
    private SprintMovement sprintMovement;
    public bool gameOver = false;

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
        walkSound = GetComponentInChildren<WalkSound>();

        if (stamina != null)
        {
            stamina.InitializeStamina(timeSprint);
        }

        sprintCooldownTime = timeSprint;
    }

    private void Update()
    {
        // Actualizar el origen del FOV para que siga al personaje
        if (fov != null)
        {
            fov.SetOrigin(transform.position);
        }

        // Reducir el tiempo del sprint si está corriendo
        if (isSprinting)
        {
            if (sprintCooldownTime > 0)
            {
                float staminaReduction = Time.deltaTime;
                sprintCooldownTime -= staminaReduction;

                if (stamina != null)
                {
                    stamina.ChangeStamina(-staminaReduction); // Reducir stamina progresivamente
                }
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

            if (stamina != null)
            {
                stamina.ChangeStamina(staminaRecovery); // Recuperar stamina progresivamente
            }
        }
    }

    private void FixedUpdate()
    {
        Vector2 direction = Vector2.zero;

        // Verificar si el joystick está habilitado
        if (!gameOver)
        {
            direction = joystick.Direction;
        }

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
                    right = direction.x;
                }
                else // Moviéndose a la izquierda
                {
                    left = -direction.x;
                }
            }
            else
            {
                if (direction.y > 0) // Moviéndose enfrente
                {
                    front = direction.y;
                }
                else // Moviéndose hacia atrás
                {
                    back = -direction.y;
                }
            }
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

    private void Walk()
    {
        walkSound.StartAudio();
    }
}