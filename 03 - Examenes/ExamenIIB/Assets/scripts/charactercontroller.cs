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
    public GameObject prefabArbol; 
    public GameManager gameManager; 

    private Rigidbody2D rb;
    private BoxCollider2D bc;
    private bool mirandoDerecha = true;
    private int saltosRestantes;
    private Animator anim;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        bc = GetComponent<BoxCollider2D>();
        saltosRestantes = saltosMaximos;
        anim = GetComponent<Animator>();
        // Asignar manualmente si no está asignado en el Inspector
        if (gameManager == null)
        {
           gameManager = Object.FindFirstObjectByType<GameManager>();
        }

        if (prefabArbol == null)
        {
            prefabArbol = Resources.Load<GameObject>("Arbol");
        }

    }

    void Update()
    {
        ProcesarMovimiento();
        ProcesarSalto();
        PlantarArbol(); 
        ComprobarCambioTamaño(); 
    }
    void ComprobarCambioTamaño()
{
    if (Input.GetKeyDown(KeyCode.Z))
    {
        if (transform.localScale == Vector3.one)
        {
            // Escala el personaje al doble de su tamaño
            transform.localScale = new Vector3(0.5f, 0.5f, 1);
        }
        else
        {
            // Restaura el tamaño original
            transform.localScale = Vector3.one;
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

    void PlantarArbol()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            if (gameManager == null)
            {
                Debug.LogError("El GameManager no está asignado.");
                return;
            }

            if (prefabArbol == null)
            {
                Debug.LogError("El Prefab Arbol no está asignado.");
                return;
            }

            if (gameManager.obtenerGemas() >= 2)
            {
                Vector3 posicionArbol = new Vector3(transform.position.x + 5.0f, transform.position.y + 0.5f, transform.position.z + 1); 
                // Mueve el árbol hacia adelante en el eje Z (al ajustar el valor Z)
                GameObject arbolInstanciado = Instantiate(prefabArbol, posicionArbol, Quaternion.identity);
                
                // Opcionalmente, ajusta el orden de renderizado del Sprite Renderer para asegurarte de que esté en frente
                SpriteRenderer arbolSpriteRenderer = arbolInstanciado.GetComponent<SpriteRenderer>();
                if (arbolSpriteRenderer != null)
                {
                    arbolSpriteRenderer.sortingOrder = 1; // Aumenta el valor si es necesario
                }

                gameManager.reiniciarGemas(); 
            }
        }
    }

}
