using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;

public class Contador : MonoBehaviour
{
    public static int contadortareas = 3;
    public TextMeshProUGUI numero;
    // Start is called before the first frame update
    void Start()
    {
        TareasPendientes(contadortareas);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TareasPendientes(int contador)
    {
        numero.text = (contador.ToString());
    }
}
