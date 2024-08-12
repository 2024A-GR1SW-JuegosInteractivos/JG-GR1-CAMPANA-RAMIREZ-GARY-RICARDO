using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int PuntosTotales { get { return puntosTotales; } set { puntosTotales = value; } }
    private int puntosTotales = 0;

    private int gemasRecogidas = 0; // Contador de gemas

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void sumarPuntos(int puntos)
    {
        puntosTotales += puntos;
        Debug.Log("Puntos totales: " + puntosTotales);
    }

    public void sumarGema()
    {
        gemasRecogidas += 1;
        Debug.Log("Gemas recogidas: " + gemasRecogidas);
    }

    public int obtenerGemas()
    {
        return gemasRecogidas;
    }

    public void reiniciarGemas()
    {
        gemasRecogidas = 0;
    }
}
