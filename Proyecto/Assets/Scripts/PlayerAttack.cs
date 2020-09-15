using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public Animator animator;

    public Transform PosicionDeAtaque;
    public float rangoDeAtaque = 0.5f;
    public LayerMask Jugador2;

    public float velocidadDeAtaque = 2f;
    float proximoAtaque = 0f;


    void Update()
    {
        if (Time.time >= proximoAtaque)
        {
            if (PlayerController.muerto == false)
            {
                if (Input.GetKeyDown(KeyCode.Space))
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

        Collider2D[] enemigosGolpeados = Physics2D.OverlapCircleAll(PosicionDeAtaque.position, rangoDeAtaque, Jugador2);

        foreach (Collider2D enemigo in enemigosGolpeados)
        {
            enemigo.GetComponent<Player2Controller>().recibirDaño(25);

        }

        void OnDrawGizmosSelected()
        {
            if (PosicionDeAtaque == null)
                return;

            Gizmos.DrawWireSphere(PosicionDeAtaque.position, rangoDeAtaque);
        }
    }


}
