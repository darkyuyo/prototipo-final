using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelP2 : MonoBehaviour{
    [SerializeField] List<Text> nombre_habilidad;
    [SerializeField] List<Text> descripcion_habilidad;
    [SerializeField] List<Text> valor_habilidad;
    [SerializeField] List<Text> tipo_habilidad;
    [SerializeField] List<Text> tipo_poder_habilidad;


    void Start()
    {
    }
    void Update()
    {
        Actualizar_panel();
    }
    public void Actualizar_panel(){
        for(int cont=0; cont<nombre_habilidad.Count;cont++){
            if(cont<GameManager.instance.monstruo2Activo._abilities.Count){
                nombre_habilidad[cont].text=GameManager.instance.monstruo2Activo._abilities[cont]._ability.getName;
                descripcion_habilidad[cont].text=GameManager.instance.monstruo2Activo._abilities[cont]._ability.getDescription;
                valor_habilidad[cont].text=GameManager.instance.monstruo2Activo._abilities[cont]._ability.getValor.ToString();
                tipo_habilidad[cont].text=GameManager.instance.monstruo2Activo._abilities[cont]._ability.getTipo.ToString();
                tipo_poder_habilidad[cont].text=GameManager.instance.monstruo2Activo._abilities[cont]._ability.getPoder.ToString();
                if(GameManager.instance.ObtenerMultiplierAI()>=GameManager.instance.monstruo2Activo._abilities[cont]._ability.getValor){
                    tipo_habilidad[cont].color=Color.white;
                    nombre_habilidad[cont].color=Color.white;
                    descripcion_habilidad[cont].color=Color.white;
                    valor_habilidad[cont].color=Color.white;
                    tipo_habilidad[cont].color=Color.white;
                    tipo_poder_habilidad[cont].color=Color.white;
                }
                else{
                    tipo_habilidad[cont].color=Color.red;
                    nombre_habilidad[cont].color=Color.red;
                    descripcion_habilidad[cont].color=Color.red;
                    valor_habilidad[cont].color=Color.red;
                    tipo_habilidad[cont].color=Color.red;
                    tipo_poder_habilidad[cont].color=Color.red;
                }
            }
            else{
                nombre_habilidad[cont].text="-";
                descripcion_habilidad[cont].text="";
                valor_habilidad[cont].text="";
                tipo_habilidad[cont].text="";
                tipo_poder_habilidad[cont].text="";
            }
        }
    }
}
