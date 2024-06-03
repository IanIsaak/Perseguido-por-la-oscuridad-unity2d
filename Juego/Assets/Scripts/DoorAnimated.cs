using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorAnimated : MonoBehaviour
{
    private Animator animator;
    private DoorSound doorSound;

    private void Start()
    {
        doorSound = GetComponentInChildren<DoorSound>();
    }

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    public void OpenDoor()
    {
        animator.SetBool("Open", true);
    }
    public void CloseDoor()
    {
        animator.SetBool("Open", false);
    }
    private void DoorSound()
    {
        doorSound.StartAudio();
    }
}
