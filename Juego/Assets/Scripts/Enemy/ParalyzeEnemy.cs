using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;

public class ParalyzeEnemy : MonoBehaviour
{
    private Entradas entradas;
    public float paralysisDuration = 3.0f; // Duración de la parálisis aplicada al enemigo
    public int maxUses = 3; // Número máximo de usos
    private int currentUses = 0; // Contador de usos actuales
    public TMP_Text counterText; // Texto UI para mostrar el contador
    public Animator batteryAnimator; // Referencia al componente Animator

    private void Awake()
    {
        entradas = new Entradas();
    }

    private void OnEnable()
    {
        entradas.Enable();
    }

    private void OnDisable()
    {
        entradas.Disable();
    }

    private void Start()
    {
        entradas.Acciones.Parálisis.performed += contexto => Paralisis(contexto);
        UpdateCounterText();
        UpdateBatteryAnimation();
    }

    private void Paralisis(InputAction.CallbackContext callbackContext)
    {
        if (currentUses < maxUses)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.right, 5.0f);
            if (hit.collider != null)
            {
                EnemyAI enemyAI = hit.collider.GetComponent<EnemyAI>();
                if (enemyAI != null)
                {
                    enemyAI.Paralyze(paralysisDuration);
                    currentUses++;
                    UpdateCounterText();
                    UpdateBatteryAnimation();
                }
            }
        }
        else
        {
            Debug.Log("No more uses left. Restart the level to reset.");
        }
    }

    private void UpdateCounterText()
    {
        if (counterText != null)
        {
            counterText.text = "Uses left: " + (maxUses - currentUses);
        }
    }
    private void UpdateBatteryAnimation()
    {
        // Se asigna el animator
        if (batteryAnimator != null)
        {
            // Se calcula el estado actual en base a los usos restantes
            int batteryState = maxUses - currentUses;
            switch (batteryState)
            {
                case 3:
                    batteryAnimator.Play("Full");
                    break;
                case 2:
                    batteryAnimator.Play("3-2");
                    break;
                case 1:
                    batteryAnimator.Play("2-1");
                    break;
                case 0:
                    batteryAnimator.Play("1-0");
                    break;
                default:
                    Debug.LogWarning("Invalid battery state.");
                    break;
            }
        }
    }

    // Esta función debe ser llamada cuando se reinicia el nivel
    public void ResetUses()
    {
        currentUses = 0;
        UpdateCounterText();
        UpdateBatteryAnimation();
    }
}
