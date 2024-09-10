using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelInicio : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject Panel;
    [SerializeField] Text TextPanel,TextPanel2,TextM1,TextM2,TextM3;
    [SerializeField] bool inicio,timerPerdida,perdiste,ganaste;
    void Start()
    {
        //Debug.Log("Inicio");
    }
    public void Inicio(){
        inicio=false;timerPerdida=true;perdiste=true;ganaste=true;
        Panel.SetActive(true);
        TextPanel.text="Presiona cualquier tecla para comenzar";
        TextPanel2.text="";
        TextM1.gameObject.SetActive(false);
        TextM2.gameObject.SetActive(false);
        TextM3.gameObject.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        
        if(Input.anyKeyDown && !inicio) 
        {
            inicio=true;
            StartCoroutine(DesaparecerPanel());
        }
        if(Input.anyKeyDown && !timerPerdida){
            timerPerdida=true;
            GameManager.instance.Batalla(false);
            GameManager.instance.IniciarNoteHolders();
        }
        if(Input.anyKeyDown && !perdiste){
            perdiste=true;
            GameManager.instance.Batalla(false);
            GameManager.instance.IniciarNoteHolders();
        }
        if(Input.anyKeyDown && !ganaste){
            ganaste=true;
            GameManager.instance.Batalla(true);
            GameManager.instance.IniciarNoteHolders();
        }
    }
    public void DesaparecerPanelInicio(){
        Panel.SetActive(false);
    }
    IEnumerator DesaparecerPanel()
    {
        GameManager.instance.StartGame();
        TextPanel.text="3";
        yield return new WaitForSeconds(1);
        TextPanel.text="2";
        yield return new WaitForSeconds(1);
        TextPanel.text="1";
        yield return new WaitForSeconds(1);
        Panel.SetActive(false);
    }
    public void TimerPerdida(){
        Debug.Log("Perdiste timer");
        Panel.SetActive(true);
        TextPanel.text="Se te ha acabado el tiempo";
        TextPanel2.text="Presiona cualquier tecla para continuar";
        StartCoroutine(Esperar1s(3));
        GameManager.instance.EndGame();
        //timerPerdida=false;
    }

    public void GanasteBatalla(int exp){
        Debug.Log("Ganaste");
        Panel.SetActive(true);
        TextPanel.text="Haz ganado la pelea, obtuviste " + exp + " de experiencia";
        TextPanel2.text="Presiona cualquier tecla para continuar";
        StartCoroutine(Esperar1s(1));
        //ganaste=false;
        //Invoke("GanasteBool",1);
    }


    IEnumerator Esperar1s(int numero)
    {
        Debug.Log("Esperando");
        yield return new WaitForSeconds(1);
        Debug.Log("Listo");
        switch(numero){
            case 1:
                ganaste=false;
                break;
            case 2:
                perdiste=false;
                break;
            case 3:
                timerPerdida=false;
                break;
        }

    }
    public void PerdisteBatalla(){
        Debug.Log("Perdiste");
        Panel.SetActive(true);
        TextPanel.text="Escapas por poco del poder hipnótico del baile";
        TextPanel2.text="Presiona cualquier tecla para continuar";
        StartCoroutine(Esperar1s(2));
        //perdiste=false;
    }

    public void MostrarMensaje(string monstruo, int cont){
        switch(cont){
            case 1:
                TextM1.gameObject.SetActive(true);
                TextM1.text=monstruo+" ha subido de nivel";
                break;
            case 2:
                TextM2.gameObject.SetActive(true);
                TextM2.text=monstruo+" ha subido de nivel";
                break;
            case 3:
                TextM3.gameObject.SetActive(true);
                TextM3.text=monstruo+" ha subido de nivel";
                break;
        }
    }

}
