using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class cable : MonoBehaviour
{

    public SpriteRenderer finalCable;
    private Vector2 posicionOriginal;
    private Vector2 tamañoOriginal;
    private TareaCable tareaCables;

    // Start is called before the first frame update
    void Start()
    {
        posicionOriginal = transform.position;
        tamañoOriginal = finalCable.size;
        tareaCables = transform.root.gameObject.GetComponent<TareaCable>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonUp(0))
        {
            Reiniciar();
        }
    }

    private void OnMouseDrag()
    {
        ActualizarPosicion();
        ComprobarConexion();
        ActualizarRotacion();
        ActualizarTamaño();
    }

    private void ActualizarPosicion()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(mousePosition.x, mousePosition.y);
;    }

    private void ActualizarRotacion()
    {
        Vector2 posicionActual = transform.position;
        Vector2 puntoOrigen = transform.parent.position;

        Vector2 direccion = posicionActual - puntoOrigen;

        float angulo = Vector2.SignedAngle(Vector2.right * transform.lossyScale, direccion);

        transform.rotation = Quaternion.Euler(0, 0, angulo);


    }

    private void ActualizarTamaño()
    {
        Vector2 posicionActual = transform.position;
        Vector2 puntoOrigen = transform.parent.position;

        float distancia = Vector2.Distance(posicionActual, puntoOrigen);

        finalCable.size = new Vector2(distancia, finalCable.size.y);
    }

    private void Reiniciar()
    {
        transform.position = posicionOriginal;
        transform.rotation = Quaternion.identity;
        finalCable.size = new Vector2(tamañoOriginal.x, tamañoOriginal.y);
    }

    private void ComprobarConexion()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 0.2f);

        foreach (Collider2D col in colliders)
        {
            if (col.gameObject != gameObject)
            {
                transform.position = col.transform.position;

                cable otroCable = col.gameObject.GetComponent<cable>();

                if (finalCable.color == otroCable.finalCable.color)
                {
                    Conectar();
                    otroCable.Conectar();

                    tareaCables.conexionesActuales++;
                    tareaCables.ComprobarVictoria();
                }
            }
        }
    }

    public void Conectar()
    {
        Destroy(this);
    }
}
