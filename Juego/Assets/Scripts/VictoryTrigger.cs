using System.Collections;
using UnityEngine;

public class VictoryTrigger : MonoBehaviour
{
    private SceneTransition sceneTransition;

    private void Start()
    {
        // Busca el componente SceneTransition en el objeto padre
        sceneTransition = GetComponentInParent<SceneTransition>();
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.GetComponent<MovementTopDown>() != null)
        {
            sceneTransition.TriggerSceneChange();
        }
    }
}