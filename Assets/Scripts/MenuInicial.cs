using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class MenuInicial : MonoBehaviour
{
    [SerializeField] GameObject Menuinicial,Menuopciones,Panel;
    [SerializeField] Slider slider;
    //[SerializeField] float volumen;
    // Start is called before the first frame update
    void Start()
    {
        slider.value=PlayerPrefs.GetFloat("Volumen",1);
        AudioListener.volume=slider.value;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Comenzar(){
        UnityEngine.SceneManagement.SceneManager.LoadScene("Mundo");
    }
    public void Jugar(){
        Menuinicial.SetActive(false);
        Panel.SetActive(true);
    }

    public void Salir(){
        Application.Quit();
    }
    public void Opciones(){
        Menuinicial.SetActive(false);
        Menuopciones.SetActive(true);
    }
    public void Volver(){
        Menuinicial.SetActive(true);
        Menuopciones.SetActive(false);
    }
    public void CambiarVolumen(float valor){
        AudioListener.volume=slider.value;
        PlayerPrefs.SetFloat("Volumen",slider.value);
    }
}
