using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class charactercontroller : MonoBehaviour
{
    public float velocidad;
    public float fuerzaSalto;
    public LayerMask capaSuelo;
    public int saltosMaximos;
    public AudioClip sonidoSalto;

    private Rigidbody2D rb;
    private BoxCollider2D bc;
    private bool mirandoDerecha = true;
    private int saltosRestantes;
    private Animator anim;
    void Start(){
        rb = GetComponent<Rigidbody2D>();
        bc = GetComponent<BoxCollider2D>();
        saltosRestantes = saltosMaximos;
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcesarMovimiento();
        ProcesarSalto();
    }

    bool EstaEnSuelo(){
        
        RaycastHit2D raycastHit = Physics2D.BoxCast(bc.bounds.center, new Vector2(bc.bounds.size.x, bc.bounds.size.y), 0f, Vector2.down, 0.2f, capaSuelo);
        return raycastHit.collider != null;
    }
    void ProcesarSalto(){
        if(Input.GetKeyDown(KeyCode.Space) && EstaEnSuelo()){
            rb.AddForce(Vector2.up*fuerzaSalto, ForceMode2D.Impulse);
            AudioManager.Instance.ReproducirSonido(sonidoSalto);

        }
    }
    void ProcesarMovimiento()
    {
        float inputMovimiento = Input.GetAxis("Horizontal");
        if(inputMovimiento != 0){
            anim.SetBool("isRunning", true);
        }else{
            anim.SetBool("isRunning", false);
        }
        rb.velocity = new Vector2(inputMovimiento * velocidad, rb.velocity.y);
        GestionarOrientacion(inputMovimiento);
    }

    void GestionarOrientacion(float inputMovimiento){
        if ((mirandoDerecha == true && inputMovimiento < 0) || (mirandoDerecha == false && inputMovimiento > 0)){
            mirandoDerecha = !mirandoDerecha;
            transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
        }
    }
}
