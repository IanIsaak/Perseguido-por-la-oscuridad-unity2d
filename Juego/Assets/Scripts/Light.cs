using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Light : MonoBehaviour
{
    public FieldOfView fov;

    void Update()
    {
        if (fov != null)
        {
            fov.SetOrigin(transform.position);
        }
    }
}
