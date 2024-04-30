using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementTopDown : MonoBehaviour
{
    public Joystick joystick;
    [SerializeField] private float movementSpeed;
    [SerializeField] private Vector2 direction;
    private Rigidbody2D rb2D;

    private void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        direction = joystick.Direction;
    }
    private void FixedUpdate()
    {
        rb2D.MovePosition(rb2D.position + direction * movementSpeed * Time.fixedDeltaTime);
    }
}
