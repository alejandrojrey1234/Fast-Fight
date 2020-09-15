using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2Attack : MonoBehaviour
{
    
    public Animator animator;

    public Transform PosicionDeAtaque;
    public float rangoDeAtaque = 0.5f;
    public LayerMask Jugador1;

    public float velocidadDeAtaque = 2f;
    float proximoAtaque = 0f;

    void Update()
    {
        if (Time.time >= proximoAtaque)
        {
            if (Player2Controller.muerto == false)
            {
                if (Input.GetKeyDown(KeyCode.Keypad1))
                {
                    Ataque();
                    proximoAtaque = Time.time + 1f / velocidadDeAtaque;
                }
            }
        }

    }

    void Ataque()
    {
        animator.SetTrigger("Atacar");

        Collider2D[] enemigosGolpeados = Physics2D.OverlapCircleAll(PosicionDeAtaque.position, rangoDeAtaque, Jugador1);

        foreach (Collider2D enemigo in enemigosGolpeados)
        {
            enemigo.GetComponent<PlayerController>().recibirDaño(25);
        }

        void OnDrawGizmosSelected()
        {
            if (PosicionDeAtaque == null)
                return;

            Gizmos.DrawWireSphere(PosicionDeAtaque.position, rangoDeAtaque);
        }
    }
}
