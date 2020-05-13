using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    public float vel = 10f;
    public float forcaPulo = 5f;
    public float contPulo = 2f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        Vector2 dir = new Vector2(x, y);
        Move(dir);
        if (Input.GetButtonDown("Jump") && contPulo > 0)
        {
            Pulo(Vector2.up);
        }
    }
    private void Move(Vector2 dir)
    {
        rb.velocity = (new Vector2(dir.x * vel, rb.velocity.y));
    }
    private void Pulo(Vector2 dir)
    {
        rb.velocity = new Vector2(rb.velocity.x, 0);
        rb.velocity += dir * forcaPulo;
        contPulo--;
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Ground")
        {
            contPulo = 2;
        }
    }
}