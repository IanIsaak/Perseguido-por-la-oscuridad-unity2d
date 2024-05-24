using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;
using System;

public class ParalyzeEnemy : MonoBehaviour
{
    public Image ElectricityOverlay;
    private float r;
    private float g;
    private float b;
    private float a;

    private Entradas entradas;
    public float paralysisDuration = 3.0f; // Duración de la parálisis aplicada al enemigo
    public int maxUses = 3; // Número máximo de usos
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
        entradas.Acciones.Parálisis.performed += contexto => Paralisis(contexto);
        UpdateCounterText();

        r = ElectricityOverlay.color.r;
        g = ElectricityOverlay.color.g;
        b = ElectricityOverlay.color.b;
        a = ElectricityOverlay.color.a;
    }

    private void FixedUpdate()
    {
        a -= 0.01f;
        a = Math.Clamp(a, 0, 1f);
        ChangeColor();
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

                    //Overlay Logic
                    a = 0.8f;
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

    // Esta función debe ser llamada cuando se reinicia el nivel
    public void ResetUses()
    {
        currentUses = 0;
        UpdateCounterText();
    }

    private void ChangeColor()
    {
        Color c = new Color(r, g, b, a);
        ElectricityOverlay.color = c;
    }
}
