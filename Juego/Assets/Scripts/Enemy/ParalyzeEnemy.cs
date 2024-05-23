using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;

public class ParalyzeEnemy : MonoBehaviour
{
    private Entradas entradas;
    public float paralysisDuration = 3.0f; // Duraci�n de la par�lisis aplicada al enemigo
    public int maxUses = 3; // N�mero m�ximo de usos
    private int currentUses = 0; // Contador de usos actuales
    public TMP_Text counterText; // Texto UI para mostrar el contador

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
        entradas.Acciones.Par�lisis.performed += contexto => Paralisis(contexto);
        UpdateCounterText();
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

    // Esta funci�n debe ser llamada cuando se reinicia el nivel
    public void ResetUses()
    {
        currentUses = 0;
        UpdateCounterText();
    }
}
