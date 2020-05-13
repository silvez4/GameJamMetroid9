using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movimento")]
    private Rigidbody2D rb;
    public float vel = 10f;
    public bool canMove = true;

    [Header("Ground Check")]
    public Transform groundCheck;
    public float footOffest = 0.4f;
    public float groundDistance = 0.1f;
    public LayerMask groundLayer;
    public bool onGround = false;

    [Header("Pulo")]
    public float forcaPulo = 15f;
    public float contPulo = 1f;
    public float totalPulos = 1f;

    [Header("Parede")]
    public LayerMask wallLayer;
    public bool onWall=false;
    public float wallFallSpeed = -1;
    public float forcaPuloParede = 10f;
    public Vector3 wallOffset;
    public float wallRadius;
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
        checkFisica();
    }
    private void Move(Vector2 dir)
    {
        if(!canMove)
            return;
        rb.velocity = (new Vector2(dir.x * vel, rb.velocity.y));
    }
    private void Pulo(Vector2 dir)
    {
        rb.velocity = new Vector2(rb.velocity.x, 0);
        rb.velocity += dir * forcaPulo;
        contPulo--;
    }
       void checkFisica()
    {
        onGround = false;
        onWall = false;

        RaycastHit2D leftFoot = Raycast(groundCheck.position + new Vector3(-footOffest, 0), Vector2.down, groundDistance, groundLayer);
        RaycastHit2D rightFoot = Raycast(groundCheck.position + new Vector3(footOffest, 0), Vector2.down, groundDistance, groundLayer);
    
        if(leftFoot || rightFoot)
        {
            contPulo = totalPulos;
            onGround = true;
        }

        bool rightWall = Physics2D.OverlapCircle(transform.position + new Vector3(wallOffset.x, 0), wallRadius, wallLayer);
        bool leftWall = Physics2D.OverlapCircle(transform.position + new Vector3(-wallOffset.x, 0), wallRadius, wallLayer);

        if(rightWall || leftWall)
        {
            onWall = true;
        }

        if (onWall)
        {
            if(rb.velocity.y < wallFallSpeed)
            {
                rb.velocity = new Vector2(rb.velocity.x, wallFallSpeed);
            }
        }

        //anim.SetBool("OnGround", onGround);
        //anim.SetBool("OnWall", onWall);

    }
        public RaycastHit2D Raycast(Vector2 origin, Vector2 rayDirection, float length, LayerMask mask, bool drawRay = true)
    {

        //Send out the desired raycasr and record the result
        RaycastHit2D hit = Physics2D.Raycast(origin, rayDirection, length, mask);

        //If we want to show debug raycasts in the scene...

        if (drawRay)
        {
            Color color = hit ? Color.red : Color.green;
            //...and draw the ray in the scene view
            Debug.DrawRay(origin, rayDirection * length, color);
        }
        //...determine the color based on if the raycast hit...

        //Return the results of the raycast
        return hit;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawWireSphere(transform.position + new Vector3(wallOffset.x, 0), wallRadius);
        Gizmos.DrawWireSphere(transform.position + new Vector3(-wallOffset.x, 0), wallRadius);
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        // if (other.gameObject.tag == "Ground")
        // {
        //     onGround = true;
        //     contPulo = 2;
        // }
        if (other.gameObject.tag == "Wall")
        {
            onWall = true;
        }
    }
    private void OnCollisionExit2D(Collision2D other) {
        //  if (other.gameObject.tag == "Ground")
        // {
        //     onGround = false;
        //     contPulo = 2;
        // }
        if (other.gameObject.tag == "Wall")
        {
            onWall = false;
        }   
    }
}