using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraController : MonoBehaviour
{
    [SerializeField] private GameObject cosaQueQuierSeguir; 
    [SerializeField] private float tamanoCamara = 10f; // Tamaño de la cámara, ajustable desde el Inspector

    // Start is called before the first frame update
    void Start()
    {
        Camera.main.orthographicSize = tamanoCamara; // Establecer el tamaño de la cámara en el inicio
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = cosaQueQuierSeguir.transform.position + new Vector3(0, 0, -10);
    }
}
