using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class Player2Controller : MonoBehaviour
{
    int rondasGanadas = 0;
    private Vector3 personaje2;
    private Vector3 personaje1;
    private Rigidbody2D rb2d;
    public Animator animator;   
    public int vidaActual;
    float speed = 25.0f;
    public static bool muerto = false;
    private static bool correr = false;
    private BoxCollider2D boxcollider;
    [SerializeField] private LayerMask pisolayermas;
    [SerializeField] private LayerMask jugadorlayermas;

    public HealthBarr healthBar;

    void Start()
    {      
        healthBar.SetMaxHealth(vidaActual);

        boxcollider = transform.GetComponent<BoxCollider2D>();
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }
    private void Update()
    {
        if ((IsGrounded() || TocaPersonaje()) && Input.GetKeyDown(KeyCode.UpArrow))
        {
            float jumplvelocity = 150f;
            rb2d.velocity = Vector2.up * jumplvelocity;
        }
    }
    void FixedUpdate()
    {
        personaje2 = GameObject.Find("Personaje 2").transform.position;
        personaje1 = GameObject.Find("Personaje 1").transform.position;

        if (IsGrounded())
        {
            float moveHorizontal = Input.GetAxis("Horizontal2");
            rb2d.velocity = new Vector2(moveHorizontal * speed, rb2d.velocity.y);
            if (moveHorizontal > 0)
            {
                GetComponent<SpriteRenderer>().flipX = true;
                GetComponent<Animator>().SetBool("Correr", true);
                correr = true;
            }
            else if (moveHorizontal < 0)
            {
                GetComponent<SpriteRenderer>().flipX = false;
                GetComponent<Animator>().SetBool("Correr", true);
                correr = true;
            }
            else
            {
                GetComponent<Animator>().SetBool("Correr", false);
                correr = false;
            }

        }

        if (correr == false && personaje2.x > personaje1.x)
        {
            GetComponent<SpriteRenderer>().flipX = false;
        }
        else if (correr == false && personaje2.x < personaje1.x)
        {
            GetComponent<SpriteRenderer>().flipX = true;
        }


    }

    private bool IsGrounded()
    {
        RaycastHit2D raycast2d = Physics2D.BoxCast(boxcollider.bounds.center, boxcollider.bounds.size, 0f, Vector2.down, 1f, pisolayermas);
        return raycast2d.collider != null;
    }

    private bool TocaPersonaje()
    {
        RaycastHit2D raycast2d = Physics2D.BoxCast(boxcollider.bounds.center, boxcollider.bounds.size, 0f, Vector2.down, 1f, jugadorlayermas);
        return raycast2d.collider != null;
    }

    public void recibirDaño(int daño)
    {
        
        vidaActual -= daño;
        animator.SetTrigger("Herido");
        if(vidaActual <= 0)
        {
            Muerte();
        }
        healthBar.SetHealth(vidaActual);
    }

    public void Muerte()
    {
        animator.SetBool("Muerto?", true);
        this.enabled = false;
         
         muerto = true;
    }
    
}
