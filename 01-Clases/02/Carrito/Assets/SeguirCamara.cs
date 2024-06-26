using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeguirCamara : MonoBehaviour
{
    [SerializeField] private GameObject cosaQueQuierSeguir; 
    // Update is called once per frame
    void Update()
    {
        transform.position = cosaQueQuierSeguir.transform.position + new Vector3(0, 0, -10);
    }
}
