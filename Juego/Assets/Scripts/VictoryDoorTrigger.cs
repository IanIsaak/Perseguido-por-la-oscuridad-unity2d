using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictoryDoorTrigger : MonoBehaviour
{
    public GameObject doorGameObject; // Referencia al objeto de la puerta
    private DoorAnimated door;

    // Start is called before the first frame update
    void Start()
    {
        door = doorGameObject.GetComponent<DoorAnimated>();
    }

    public void OpenVictoryDoor()
    {
        if (door != null)
        {
            door.OpenDoor();
        }
    }
}
