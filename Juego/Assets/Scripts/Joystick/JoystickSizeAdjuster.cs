using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public RectTransform joystickTransform;
    public Canvas canvas;

    // Start is called before the first frame update
    void Start()
    {
        // Obtiene las dimensiones del canvas
        RectTransform canvasRect = canvas.GetComponent<RectTransform>();

        // Calcula el tamaño relativo del joystick
        float canvasWidth = canvasRect.rect.width;
        float canvasHeight = canvasRect.rect.height;

        // Define el nuevo tamaño del joystick (por ejemplo, un tercio del tamaño del canvas)
        float newWidth = canvasWidth / 3;
        float newHeight = canvasHeight;

        // Asigna el nuevo tamaño al joystick
        joystickTransform.sizeDelta = new Vector2(newWidth, newHeight);   
    }
}
