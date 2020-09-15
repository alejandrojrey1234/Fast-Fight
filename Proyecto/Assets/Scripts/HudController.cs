using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HudController : MonoBehaviour
{

    public Text rondasCanvas;

    public Text jugador1;
    public Text jugador2;

    public Text RondaJ1;
    public Text RondaJ2;

    public Text texto;

    public Text Timer;

    public static HudController instance;
    public static HudController GetInstance()
    {
        return instance;
    }
    void Awake()
    {
        instance = this;    
    }


    void Start()
    {
        jugador1.text = "Bandido Negro";
        jugador2.text = "Bandido Marron";
        
    }

 
}
