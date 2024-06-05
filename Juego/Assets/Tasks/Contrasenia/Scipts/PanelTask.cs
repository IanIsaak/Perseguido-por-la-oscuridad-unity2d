using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PanelTask : MonoBehaviour
{
    public TextMeshProUGUI display;
    public TextMeshProUGUI papel;
    private VictoryDoorTrigger victoryDoorTrigger; // Referencia al VictoryDoorTrigger

    // Start is called before the first frame update
    void Start()
    {
        // Buscar VictoryDoorTrigger en la escena
        victoryDoorTrigger = FindObjectOfType<VictoryDoorTrigger>();

        if (victoryDoorTrigger == null)
        {
            Debug.LogError("No se encontró VictoryDoorTrigger en la escena.");
        }

        GeneratePassword();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void AddNumber(string number)
    {
        if (display.text.Length >= 5)
        {
            return;
        }

        display.text += number;
    }

    public void EraseDisplay()
    {
        display.text = "";
    }

    private void GeneratePassword()
    {
        papel.text = "";
        for (int i = 0; i < 5; i++)
        {
            int randNumber = UnityEngine.Random.Range(1, 9);
            papel.text += randNumber;
        }
    }

    public void CheckPassword()
    {
        if (display.text.Equals(papel.text))
        {
            display.text = ("Granted");
            if (victoryDoorTrigger != null)
            {
                victoryDoorTrigger.OpenVictoryDoor(); // Llama al método para abrir la puerta
            }
            Destroy(gameObject, 1.0f);
        }
        else
        {
            display.text = ("Denied");
        }
    }
}
