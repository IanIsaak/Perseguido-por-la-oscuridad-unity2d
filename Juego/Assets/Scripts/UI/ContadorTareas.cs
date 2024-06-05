using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ContadorTareas : MonoBehaviour
{
    [SerializeField] GameObject exitAlert;

    private float tareas = 0;
    private TextMeshProUGUI textTareas;
    private VictoryDoorTrigger victoryDoorTrigger; // Referencia al VictoryDoorTrigger

    void Start()
    {
        textTareas = GetComponent<TextMeshProUGUI>();

        // Buscar VictoryDoorTrigger en la escena
        victoryDoorTrigger = FindObjectOfType<VictoryDoorTrigger>();

        if (victoryDoorTrigger == null)
        {
            Debug.LogError("No se encontr� VictoryDoorTrigger en la escena.");
        }
    }

    private void Update()
    {
        textTareas.text = tareas.ToString() + "/3 Tareas";

    }

    public void SumarPuntos()
    {
        tareas++;
        if (victoryDoorTrigger != null && tareas == 3)
        {
            victoryDoorTrigger.OpenVictoryDoor(); // Llama al m�todo para abrir la puerta
            exitAlert.SetActive(true);
        }
    }
}
