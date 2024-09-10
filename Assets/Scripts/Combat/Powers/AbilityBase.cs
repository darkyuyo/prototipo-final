using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static Stats;
using static Monstruo;

[CreateAssetMenu(fileName = "Ability", menuName = "Ability", order = 0)]
public class AbilityBase : ScriptableObject
{
    [SerializeField] string name;
    [SerializeField] string Description;
    [SerializeField] float ActiveTime;
    [SerializeField] int  valor;
    [SerializeField] Stats.Tipo tipo_ataque;
    [SerializeField] Categoria categoria_ataque;
    [SerializeField] float poder;
    [SerializeField] Objetivo target;
    public Monstruo emisor;
    [SerializeField] Monstruo.Stat statboost1;
    [SerializeField] Monstruo.Stat statboost2;
    

    [SerializeField] Dano tipo_dano;
    public enum Categoria{
        Dano,
        Estado,
        Cura,
        Robo_stats,
        Robo_vida,
        Revivir,
        Criticos,
        Proteger,
        Boost,
        Ignora_armadura,
        Ataque_repetitivo
    }
    public void Start(){
        }
    public enum Dano{
        Fisico,
        Magico,
        None
    }

    public Stats.Tipo getTipo{
        get{return tipo_ataque;}
    }
    public string getName{
        get{return name;}
    }
    public string getDescription{
        get{return Description;}
    }
    public float getActiveTime{
        get{return ActiveTime;}
    }
    public int getValor{
        get{return valor;}
    }
    public Categoria getCategoria{
        get{return categoria_ataque;}
    }
    public float getPoder{
        get{return poder;}
    }
    public void Activate(bool isPlayer){
        switch(this.categoria_ataque){
            case Categoria.Dano:
                switch(this.tipo_dano){
                    case Dano.Fisico:
                        if(isPlayer){
                            GameManager.instance.monstruo1Activo.DamageFisic(GameManager.instance.monstruo2Tank, (int)this.poder,this.tipo_ataque); 
                        }
                        else{
                            GameManager.instance.monstruo2Activo.DamageFisic(GameManager.instance.monstruo1Tank, (int)this.poder,this.tipo_ataque);
                        }
                        break;
                    case Dano.Magico:
                        if(isPlayer){
                            GameManager.instance.monstruo1Activo.DamageMagic(GameManager.instance.monstruo2Tank, (int)this.poder, this.tipo_ataque);
                        }
                        else{
                            GameManager.instance.monstruo2Activo.DamageMagic(GameManager.instance.monstruo1Tank, (int)this.poder, this.tipo_ataque);
                        }
                        break;
                }
                break;
            case Categoria.Estado:
                break;
            case Categoria.Cura:
                switch(this.target){
                    case Objetivo.Aliado:
                        if(isPlayer){
                            GameManager.instance.monstruo1Activo.sumarVidaPercentage(this.poder);
                        }
                        else{
                            GameManager.instance.monstruo2Activo.sumarVidaPercentage(this.poder);
                        }
                    break;
                }
                break;
            case Categoria.Robo_stats:
                break;
            case Categoria.Robo_vida:
                int robo;
                switch(this.tipo_dano){
                    case Dano.Fisico:
                        if(isPlayer){
                            robo=GameManager.instance.monstruo1Activo.DamageFisic(GameManager.instance.monstruo2Tank, (int)this.poder,this.tipo_ataque);
                            GameManager.instance.monstruo1Activo.sumarVida(robo);
                        }
                        else{
                            robo=GameManager.instance.monstruo2Activo.DamageFisic(GameManager.instance.monstruo1Tank, (int)this.poder,this.tipo_ataque);
                            GameManager.instance.monstruo2Activo.sumarVida(robo);
                        }
                        break;
                    case Dano.Magico:
                    if(isPlayer){
                            robo=GameManager.instance.monstruo1Activo.DamageMagic(GameManager.instance.monstruo2Tank, (int)this.poder, this.tipo_ataque);
                            GameManager.instance.monstruo1Activo.sumarVida(robo);
                        }
                        else{
                            robo=GameManager.instance.monstruo2Activo.DamageMagic(GameManager.instance.monstruo1Tank, (int)this.poder, this.tipo_ataque);
                            GameManager.instance.monstruo2Activo.sumarVida(robo);
                        }
                        break;
                }
                break;
            case Categoria.Revivir:
                break;
            case Categoria.Criticos:
                break;
            case Categoria.Proteger:
                break;
            case Categoria.Boost:
                if(isPlayer){
                    GameManager.instance.monstruo1Activo.AplicarCambios(this.statboost1,this.poder);
                    GameManager.instance.monstruo1Activo.AplicarCambios(this.statboost2,this.poder);
                }
                else{
                    GameManager.instance.monstruo2Activo.AplicarCambios(this.statboost1,this.poder);
                    GameManager.instance.monstruo2Activo.AplicarCambios(this.statboost2,this.poder);
                }
                break;
            case Categoria.Ignora_armadura:
                break;
            case Categoria.Ataque_repetitivo:
                switch(this.tipo_dano){
                    case Dano.Fisico:
                        if(isPlayer){
                                GameManager.instance.monstruo1Activo.DamageFisic(GameManager.instance.monstruo2Tank, (int)this.poder,this.tipo_ataque);
                                //Debug.Log("golpeo");
                                GameManager.instance.monstruo1Activo.DamageFisic(GameManager.instance.monstruo2Tank, (int)this.poder,this.tipo_ataque);
                                //Debug.Log("golpeo");
                                for (int i = 0; i < 3; i++)
                                {
                                    int random=Random.Range(0,100);
                                    if(random<50){
                                        GameManager.instance.monstruo1Activo.DamageFisic(GameManager.instance.monstruo2Tank, (int)this.poder,this.tipo_ataque);
                                        //Debug.Log("golpeo");
                                    }
                                }
                            }
                            else{
                                GameManager.instance.monstruo2Activo.DamageFisic(GameManager.instance.monstruo1Tank, (int)this.poder,this.tipo_ataque);
                                //Debug.Log("golpeo");
                                GameManager.instance.monstruo2Activo.DamageFisic(GameManager.instance.monstruo1Tank, (int)this.poder,this.tipo_ataque);
                                //Debug.Log("golpeo");
                                for (int i = 0; i < 3; i++)
                                {
                                    int random=Random.Range(0,100);
                                    if(random<50){
                                        GameManager.instance.monstruo2Activo.DamageFisic(GameManager.instance.monstruo1Tank, (int)this.poder,this.tipo_ataque);
                                        //Debug.Log("golpeo");
                                    }
                                }
                            }
                        break;
                    case Dano.Magico:
                        if(isPlayer){
                            GameManager.instance.monstruo1Activo.DamageMagic(GameManager.instance.monstruo2Tank, (int)this.poder, this.tipo_ataque);
                            //Debug.Log("golpeo");
                            GameManager.instance.monstruo1Activo.DamageMagic(GameManager.instance.monstruo2Tank, (int)this.poder, this.tipo_ataque);
                            //Debug.Log("golpeo");
                            for (int i = 0; i < 3; i++)
                            {
                                int random=Random.Range(0,100);
                                if(random<50){
                                    GameManager.instance.monstruo1Activo.DamageMagic(GameManager.instance.monstruo2Tank, (int)this.poder, this.tipo_ataque);
                                    //Debug.Log("golpeo");
                                }
                            }
                        }
                        else{
                            GameManager.instance.monstruo2Activo.DamageMagic(GameManager.instance.monstruo1Tank, (int)this.poder, this.tipo_ataque);
                            //Debug.Log("golpeo");
                            GameManager.instance.monstruo2Activo.DamageMagic(GameManager.instance.monstruo1Tank, (int)this.poder, this.tipo_ataque);
                            Debug.Log("golpeo");
                            for (int i = 0; i < 3; i++)
                            {
                                int random=Random.Range(0,100);
                                if(random<50){
                                    GameManager.instance.monstruo2Activo.DamageMagic(GameManager.instance.monstruo1Tank, (int)this.poder, this.tipo_ataque);
                                    //Debug.Log("golpeo");
                                }
                            }
                        }
                    break;
                }
                break;
        }
    }

    /*public enum Stat{
        None,
        Ataque,
        Defensa,
        AtaqueMagico,
        DefensaMagica,}*/


    public enum Objetivo{
        Enemigo,
        Aliado,
        Todos,
        UnoMismo
    }
}