using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coin : MonoBehaviour
{
    public int valor =1;
    public GameManager gameManager;
    public AudioClip sonidoDiamante;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player"){

            Debug.Log("El jugador ha recogido un diamante");
            gameManager.sumarPuntos(valor);
            Destroy(this.gameObject);
            AudioManager.Instance.ReproducirSonido(sonidoDiamante);

        }
    }
}
