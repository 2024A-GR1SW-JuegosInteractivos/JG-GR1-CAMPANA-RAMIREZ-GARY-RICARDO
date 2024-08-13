using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class charactercontroller : MonoBehaviour
{
    public float velocidad;
    public float fuerzaSalto;
    public float velocidadDeslizamiento = 10f;
    public float duracionDeslizamiento = 0.5f;
    public float distanciaTeletransporte = 3f; 
    public LayerMask capaSuelo;
    public int saltosMaximos;
    public AudioClip sonidoSalto;

    private Rigidbody2D rb;
    private BoxCollider2D bc;
    private bool mirandoDerecha = true;
    private int saltosRestantes;
    private Animator anim;
    private bool estaDeslizando = false;
    private Vector3 escalaInicial;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        bc = GetComponent<BoxCollider2D>();
        saltosRestantes = saltosMaximos;
        anim = GetComponent<Animator>();
        escalaInicial = transform.localScale; 
    }

    void Update()
    {
        ProcesarMovimiento();
        ProcesarSalto();
        ComprobarCambioTamaño();
        ProcesarDeslizamiento();
        ProcesarTeletransporte(); 
    }

    void ComprobarCambioTamaño()
        {
            if (Input.GetKeyDown(KeyCode.Z))
            {
                if (transform.localScale == new Vector3(2, 2, 1))
                {
                    transform.localScale = new Vector3(0.5f, 0.5f, 1);
                }
                else
                {
                    transform.localScale = new Vector3(2, 2, 1);
                }
            }
        }
    bool EstaEnSuelo()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(bc.bounds.center, new Vector2(bc.bounds.size.x, bc.bounds.size.y), 0f, Vector2.down, 0.2f, capaSuelo);
        return raycastHit.collider != null;
    }

    void ProcesarSalto()
    {
        if (Input.GetKeyDown(KeyCode.Space) && EstaEnSuelo())
        {
            rb.AddForce(Vector2.up * fuerzaSalto, ForceMode2D.Impulse);
            AudioManager.Instance.ReproducirSonido(sonidoSalto);
        }
    }

    void ProcesarMovimiento()
    {
        if (estaDeslizando) return;

        float inputMovimiento = Input.GetAxis("Horizontal");
        if (inputMovimiento != 0)
        {
            anim.SetBool("isRunning", true);
        }
        else
        {
            anim.SetBool("isRunning", false);
        }
        rb.velocity = new Vector2(inputMovimiento * velocidad, rb.velocity.y);
        GestionarOrientacion(inputMovimiento);
    }

    void GestionarOrientacion(float inputMovimiento)
    {
        if ((mirandoDerecha == true && inputMovimiento < 0) || (mirandoDerecha == false && inputMovimiento > 0))
        {
            mirandoDerecha = !mirandoDerecha;
            transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
        }
    }

    void ProcesarDeslizamiento()
    {
        if (Input.GetKeyDown(KeyCode.C) && !estaDeslizando && EstaEnSuelo())
        {
            StartCoroutine(Deslizar());
        }
    }

    IEnumerator Deslizar()
    {
        estaDeslizando = true;
        float direccionDeslizamiento = mirandoDerecha ? 1 : -1;
        rb.velocity = new Vector2(direccionDeslizamiento * velocidadDeslizamiento, rb.velocity.y);

        yield return new WaitForSeconds(duracionDeslizamiento);

        rb.velocity = new Vector2(0, rb.velocity.y);
        estaDeslizando = false;
    }

    void ProcesarTeletransporte()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            float direccionTeletransporte = mirandoDerecha ? 1 : -1;

            Vector3 nuevaPosicion = transform.position + new Vector3(direccionTeletransporte * distanciaTeletransporte, 0, 0);

            transform.position = nuevaPosicion;
        }
    }
}
