using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class randomizarcables : MonoBehaviour
{
    // Start is called before the first frame update
    private void Awake()
    {
        for(int i=0; i<transform.childCount;i++)
        {
            GameObject cableActual = transform.GetChild(i).gameObject;
            GameObject otroCable = transform.GetChild(Random.Range(0, transform.childCount)).gameObject;

            Vector2 nuevaPoscableActual = otroCable.transform.position;
            Vector2 nuevaPosOrtroCable = cableActual.transform.position;

            cableActual.transform.position = nuevaPoscableActual;
            otroCable.transform.position = nuevaPosOrtroCable;
        }
    }
}
