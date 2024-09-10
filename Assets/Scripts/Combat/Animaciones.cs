using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class Animaciones : MonoBehaviour
{
    [SerializeField]HUD hud1player,hud2player,hud3player,hud1ia,hud2ia,hud3ia, aux, aux2;
    [SerializeField] bool isStart, isCaida, isCaidaIA, miss, missIA;
    [SerializeField] float rotationSpeed=100f;
    [SerializeField] float caida=-0.05f;
    [SerializeField] int contador=0, contador_maximo=100, contadorIA=0, contador_maximoIA=100;
    // Start is called before the first frame update
    void Start()
    {
        isStart=false;
        isCaida=false;
        aux=null;
        aux2=null;
    }

    // Update is called once per frame
    public void HandleUpdate()
    {
        if(isCaida==false){
            if(GameManager.instance.playerParty.getMonstruo(0)==GameManager.instance.monstruo1Activo){
                hud1player.rotarSprite(rotationSpeed);
                hud2player.PositionInicial();
                hud3player.PositionInicial();
            }
            else if(GameManager.instance.playerParty.getMonstruo(1)==GameManager.instance.monstruo1Activo){
                hud2player.rotarSprite(rotationSpeed);
                hud1player.PositionInicial();
                hud3player.PositionInicial();
            }
            else if(GameManager.instance.playerParty.getMonstruo(2)==GameManager.instance.monstruo1Activo){
                hud3player.rotarSprite(rotationSpeed);
                hud1player.PositionInicial();
                hud2player.PositionInicial();
            }
        }
        if(isCaidaIA==false){
            if(GameManager.instance.IAParty.getMonstruo(0)==GameManager.instance.monstruo2Activo){
                hud1ia.rotarSprite(rotationSpeed);
                hud2ia.PositionInicial();
                hud3ia.PositionInicial();
            }
            else if(GameManager.instance.IAParty.getMonstruo(1)==GameManager.instance.monstruo2Activo){
                hud2ia.rotarSprite(rotationSpeed);
                hud1ia.PositionInicial();
                hud3ia.PositionInicial();
            }
            else if(GameManager.instance.IAParty.getMonstruo(2)==GameManager.instance.monstruo2Activo){
                hud3ia.rotarSprite(rotationSpeed);
                hud1ia.PositionInicial();
                hud2ia.PositionInicial();
            }
        }
        if(miss){
            isCaida=true;
            miss=false;
            aux=hud1player;
            if(GameManager.instance.playerParty.getMonstruo(0)==GameManager.instance.monstruo1Activo){
                aux=hud1player;
            }
            else if(GameManager.instance.playerParty.getMonstruo(1)==GameManager.instance.monstruo1Activo){
                aux=hud2player;
            }
            else if(GameManager.instance.playerParty.getMonstruo(2)==GameManager.instance.monstruo1Activo){
                aux=hud3player;
            }
            aux.voltearSprite();
        }
        if(isCaida){
            if(contador<contador_maximo){
                aux.caidaSprite(caida);
                contador++;
            }
            else{
                isCaida=false;
                contador=0;
                aux.PositionInicial();
            }
        }
        if(missIA){
            isCaidaIA=true;
            missIA=false;
            aux2=hud1ia;
            if(GameManager.instance.IAParty.getMonstruo(0)==GameManager.instance.monstruo2Activo){
                aux2=hud1ia;
            }
            else if(GameManager.instance.IAParty.getMonstruo(1)==GameManager.instance.monstruo2Activo){
                aux2=hud2ia;
            }
            else if(GameManager.instance.IAParty.getMonstruo(2)==GameManager.instance.monstruo2Activo){
                aux2=hud3ia;
            }
            aux2.voltearSprite();
        }
        if(isCaidaIA){
            if(contadorIA<contador_maximoIA){
                aux2.caidaSprite(caida);
                contadorIA++;
            }
            else{
                isCaidaIA=false;
                contadorIA=0;
                aux2.PositionInicial();
            }
        }
    }
    void Update()
    {
        if(isStart){
            HandleUpdate();
        }
    }
    public void StartAnim(){
        isStart=true;
        isCaida=false;
        isCaidaIA=false;
        contador=200;
        contadorIA=200;
    }
    public void EndAnim(){
        isStart=false;
        isCaida=false;
        isCaidaIA=false;
        hud1player.PositionInicial();
        hud2player.PositionInicial();
        hud3player.PositionInicial();
        hud1ia.PositionInicial();
        hud2ia.PositionInicial();
        hud3ia.PositionInicial();
    }
    public void Miss(){
        miss=true;
    }
    public void MissIA(){
        missIA=true;
    }
}
