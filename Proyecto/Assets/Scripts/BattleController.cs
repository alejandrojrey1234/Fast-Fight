using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BattleController : MonoBehaviour
{
    WaitForSeconds seg;

    public GameObject objJugador1;
    public GameObject objJugador2;

    Vector3 orPosJ1;
    Vector3 orPosJ2;

    public bool countdown;
    private int maxTurnTime = 10;
    public int currentTime;
    float internalTimer;

    public GameObject ganadorxd;
    public Text ganador;

    public int rondasp1;
    public int rondasp2;

    public HudController hud;

    public int limiteGanadas = 2;
    int currentTurn = 1;

    public PlayerController p1Muerto;
    public Player2Controller p2Muerto;



    private void Start()
    {
        seg = new WaitForSeconds(1);

        p1Muerto.enabled = false;
        p2Muerto.enabled = false;

        Player2Controller.muerto = true;
        PlayerController.muerto = true;

        orPosJ1 = objJugador1.transform.position;
        orPosJ2 = objJugador2.transform.position;

        hud = HudController.GetInstance();


        StartCoroutine("StartGame");
    }


    void Update()
    {
        if (countdown)
        {
            HandleTurnTimer();
        }



    }
    void HandleTurnTimer()
    {
        hud.Timer.text = currentTime.ToString();

        internalTimer += Time.deltaTime;

        if (internalTimer > 1)
        {
            currentTime--;
            internalTimer = 0;
        }
        if (currentTime <= 0 || Player2Controller.muerto == true || PlayerController.muerto == true)
        {
            EndTurnFunction(true);
            countdown = false;
        }
    }
    public void EndTurnFunction(bool timeOut = false)
    {
        countdown = false;
        hud.Timer.text = maxTurnTime.ToString();

        if (timeOut)
        {
            hud.texto.gameObject.SetActive(true);
            hud.texto.text = "Tiempo Fuera!";
        }
        else
        {
            hud.texto.gameObject.SetActive(true);
            hud.texto.text = "K.O.";
        }
        StartCoroutine("EndTurn");

    }

    IEnumerator EndTurn()
    {
        p1Muerto.enabled = false;
        p2Muerto.enabled = false;

        Player2Controller.muerto = true;
        PlayerController.muerto = true;

        yield return seg;
        yield return seg;
        yield return seg;

        QuienGano();

        yield return seg;
        yield return seg;
        yield return seg;

        currentTurn++;

        bool matchOver = isMatchOver();

        if (!matchOver)
        {
            StartCoroutine("initTurn");
        }
        else
        {
            rondasp1 = 0;
            rondasp2 = 0;
        }
    }

    bool isMatchOver()
    {
        bool retVal = false;
        if(rondasp2 >= limiteGanadas || rondasp1 >= limiteGanadas)
        {
            retVal = true;
        }
        return retVal;

    }

    IEnumerator StartGame()
    {
        yield return initTurn();
    }
    IEnumerator initTurn()
    {
        hud.rondasCanvas.text = "Ronda " + currentTurn;
        hud.texto.gameObject.SetActive(false);

        currentTime = maxTurnTime;
        countdown = false;

        yield return initPlayer();

        yield return OLA();
    }
    IEnumerator initPlayer()
    {
        p1Muerto.vidaActual = 100;
        p2Muerto.vidaActual = 100;

        p1Muerto.healthBar.SetMaxHealth(100);
        p2Muerto.healthBar.SetMaxHealth(100);

        p1Muerto.animator.SetBool("Muerto?", false);
        p2Muerto.animator.SetBool("Muerto?", false);

        objJugador1.transform.position = orPosJ1;
        objJugador2.transform.position = orPosJ2; 

        yield return null;
    }
    IEnumerator OLA()
    {
        hud.texto.gameObject.SetActive(true);
        hud.texto.text = "Ronda " + currentTurn;

        yield return seg;
        yield return seg;

        hud.texto.text = "3";
        yield return seg;
        hud.texto.text = "2";
        yield return seg;
        hud.texto.text = "1";
        yield return seg;
        hud.texto.text = "PELEEN!";

        p1Muerto.enabled = true;
        p2Muerto.enabled = true;
        
        Player2Controller.muerto = false;
        PlayerController.muerto = false;

        yield return seg;
        hud.texto.gameObject.SetActive(false);
        countdown = true;
    }


    void QuienGano()
    {
        if(p1Muerto.vidaActual != p2Muerto.vidaActual)
        {
            if(p1Muerto.vidaActual < p2Muerto.vidaActual)
            {
                rondasp2++;
                hud.RondaJ2.text = rondasp2.ToString();
                hud.texto.text = "Gano el Jugador 2";
            }
            else
            {
                rondasp1++;
                hud.RondaJ1.text = rondasp1.ToString();
                hud.texto.text = "Gano el Jugador 1";
            }
        }
        else
        {
            hud.texto.text = "Es un empate";
        }
    }
    public static BattleController instance;
    public static BattleController GetInstance()
    {
        return instance;
    }
    private void Awake()
    {
        instance = this;
    }
}





