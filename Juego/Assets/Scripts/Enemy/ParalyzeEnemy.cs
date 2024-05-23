using UnityEngine;
using UnityEngine.InputSystem;

public class ParalyzeEnemy : MonoBehaviour
{
    private Entradas entradas;

    public float paralysisDuration = 3.0f; // Duración de la parálisis aplicada al enemigo

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
    }

    private void Paralisis(InputAction.CallbackContext callbackContext)
    {
        Debug.Log("Space pressed");
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.right, 5.0f);
        if (hit.collider != null)
        {
            EnemyAI enemyAI = hit.collider.GetComponent<EnemyAI>();
            if (enemyAI != null)
            {
                enemyAI.Paralyze(paralysisDuration);
            }
        }
    }
}
