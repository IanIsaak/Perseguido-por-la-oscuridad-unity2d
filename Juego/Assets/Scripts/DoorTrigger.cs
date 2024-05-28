using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    [SerializeField] private GameObject doorGameObject;
    private DoorAnimated door;

    private void Awake()
    {
        door = doorGameObject.GetComponent<DoorAnimated>();
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.GetComponent<MovementTopDown>() != null)
        {
            door.OpenDoor();
        }
    }
    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.GetComponent<MovementTopDown>() != null)
        {
            door.CloseDoor();
        }
    }
}
