using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int PuntosTotales{get{return puntosTotales;} set{puntosTotales = value;}}
    private int puntosTotales = 0;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void sumarPuntos(int puntos){
        puntosTotales += puntos;
        Debug.Log("Puntos totales: " + puntosTotales);
    }
}
