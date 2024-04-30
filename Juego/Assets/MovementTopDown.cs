using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementTopDown : MonoBehaviour
{
    [SerializeField] private float movementSpeed;
    [SerializeField] private Vector2 direction;
    private Rigidbody2D rb2D;

    private void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        direction = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
    }
    private void FixedUpdate()
    {
        rb2D.MovePosition(rb2D.position + direction * movementSpeed * Time.fixedDeltaTime);
    }
}
