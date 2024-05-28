using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class FieldOfView : MonoBehaviour
{
    public float fov = 360f;
    public int numAristas = 360;
    public float anguloInicial = 0;
    public float distanciaVision = 8f;
    public LayerMask layerMask;

    private Mesh mesh;
    private Vector3 origen;

    private void Start()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
    }

    private void LateUpdate()
    {
        UpdateMesh();
    }

    private void UpdateMesh()
    {
        float anguloActual = anguloInicial;
        float incrementoAngulo = fov / numAristas;

        Vector3[] vertices = new Vector3[numAristas + 1];
        int[] triangulos = new int[numAristas * 3];

        vertices[0] = origen;

        int indiceVertices = 1;
        int indiceTriangulos = 0;

        for (int i = 0; i < numAristas; i++)
        {
            Vector3 verticeActual;
            RaycastHit2D raycastHit2D = Physics2D.Raycast(origen, GetVectorFromAngle(anguloActual), distanciaVision, layerMask);

            if (raycastHit2D.collider == null)
            {
                verticeActual = origen + GetVectorFromAngle(anguloActual) * distanciaVision;
            }
            else
            {
                verticeActual = raycastHit2D.point;
            }

            vertices[indiceVertices] = verticeActual;

            if (i > 0)
            {
                triangulos[indiceTriangulos + 0] = 0;
                triangulos[indiceTriangulos + 1] = indiceVertices - 1;
                triangulos[indiceTriangulos + 2] = indiceVertices;

                indiceTriangulos += 3;
            }

            indiceVertices++;
            anguloActual -= incrementoAngulo;
        }

        triangulos[indiceTriangulos + 0] = 0;
        triangulos[indiceTriangulos + 1] = indiceVertices - 1;
        triangulos[indiceTriangulos + 2] = 1;

        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = triangulos;
        mesh.RecalculateNormals();
    }

    Vector3 GetVectorFromAngle(float angle)
    {
        float angleRad = angle * (Mathf.PI / 180f);
        return new Vector3(Mathf.Cos(angleRad), Mathf.Sin(angleRad));
    }

    public void SetOrigin(Vector3 newOrigin)
    {
        origen = newOrigin;
    }

    public void SetAngleInicial(float nuevoAngulo)
    {
        anguloInicial = nuevoAngulo;
    }
}