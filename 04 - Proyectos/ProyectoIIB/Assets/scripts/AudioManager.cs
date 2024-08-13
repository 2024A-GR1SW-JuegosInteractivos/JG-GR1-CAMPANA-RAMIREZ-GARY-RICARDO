using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance{get; private set;}
    private AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void ReproducirSonido(AudioClip clip){
        audioSource.clip = clip;
        audioSource.Play();
    }

    private void Awake(){
        if(Instance == null){
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }else{
            Debug.Log("Cuidado!, hay dos instancias de AudioManager en la escena. Eliminando la instancia recién creada.");
            Destroy(this.gameObject);
        }
    }
}
