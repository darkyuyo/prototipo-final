using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Stats;

[System.Serializable]
public class Monstruo
{
    [SerializeField] Stats _stats;
    [SerializeField] int _level;
    public Stats Stats{
        get{return _stats;}
    }
    public int Level{
        get{return _level;}
    }
    public List<Ability> _abilities{get;set;}
    public void Init()
    {
        _abilities=new List<Ability>();
        foreach (var move in _stats.getMovimientos_aprendibles){
            if(move.getNivel<=_level){
                _abilities.Add(new Ability(move.getHabilidad));
            }
            if(_abilities.Count>=4){
                break;
            }
        }
        Exp=Stats.getExpForLevel(_level);
        CalculateStats();
        VidaActual=VidaMax;
        //Debug.Log("Se ha creado un monstruo de nivel "+_level+" de nombre  "+_stats.Name+" con "+_abilities[0]._ability.getName);
        StatsBoostDictionary=new Dictionary<Stat, int>(){
            {Stat.Ataque,0},
            {Stat.Defensa,0},
            {Stat.AtaqueMagico,0},
            {Stat.DefensaMagica,0}
        };
    }
    public void Quitarboosts(){
        StatsBoostDictionary[Stat.Defensa]=0;
        StatsBoostDictionary[Stat.Ataque]=0;
        StatsBoostDictionary[Stat.AtaqueMagico]=0;
        StatsBoostDictionary[Stat.DefensaMagica]=0;
    }
    public enum Stat{
        None,
        Ataque,
        Defensa,
        AtaqueMagico,
        DefensaMagica,}
    public bool CheckForLevelUp(){
        if(Exp>=Stats.getExpForLevel(_level+1)){
            _level++;
            Init();
            return true;
        }
        else{
            return false;
        }
    }
    public Dictionary<Stat,int> StatsDictionary{get;private set;}
    public Dictionary<Stat,int> StatsBoostDictionary{get;private set;}
    public void CalculateStats(){
        StatsDictionary=new Dictionary<Stat, int>();
        StatsDictionary.Add(Stat.Ataque,Mathf.FloorToInt((_stats.getAttack*_level*2) / 100f)+5);
        StatsDictionary.Add(Stat.Defensa,Mathf.FloorToInt((_stats.getDefense*_level*2) / 100f)+5);
        StatsDictionary.Add(Stat.AtaqueMagico,Mathf.FloorToInt((_stats.getMagicAttack*_level*2) / 100)+5);
        StatsDictionary.Add(Stat.DefensaMagica,Mathf.FloorToInt((_stats.getMagicDefense*_level*2) / 100f)+5);
    }
    public int Exp {get;set;}
    public int VidaActual{get;set;}
    public int _vidaactual;
    int getStat(Stat stat){

        int statValue=StatsDictionary[stat];
        int boost=StatsBoostDictionary[stat];
        var boostValues=new float[]{1f,1.5f,2f,2.5f,3f,3.5f,4f};
        if (boost>=0){
            statValue=Mathf.FloorToInt(statValue*boostValues[boost]);
        }
        else{
            statValue=Mathf.FloorToInt(statValue/(boostValues[-boost]));
        }
        return statValue;
    }
    public int Attack{
        get{    return getStat(Stat.Ataque);}
    }
    public int MagicAttack{
        get{    return getStat(Stat.AtaqueMagico);}
    }
    public int Defense{
        get{    return getStat(Stat.Defensa);}
    }
    public int MagicDefense{
        get{    return getStat(Stat.DefensaMagica);}
    }
    public int VidaMax{
        get{return Mathf.FloorToInt(_stats.getVidaMax*_level*6/100f)+50;}
    }
    public bool isAlive{
        get=>this.VidaActual>0;
    }
    public int getLevel{
        get{return _level;}
    }
    public bool restarVida(float damage){
        VidaActual-=Mathf.FloorToInt(damage);
        if(VidaActual<=0){
            VidaActual=0;
            return true;
        }
        else{
            return false;
        }
    }
    public void sumarVida(int cura){
        VidaActual+=cura;
        if(VidaActual>VidaMax){
            VidaActual=VidaMax;
        }
    }
    public void AplicarCambios(Stat stat,float boost){
        if(stat==Stat.None){
            return;
            }
        else{
            //Debug.Log("Se ha aplicado un cambio de "+boost+" a "+stat);
            //Debug.Log("El valor original de "+stat+" es "+statsDictionary[stat]);
            StatsBoostDictionary[stat]=Mathf.Clamp(StatsBoostDictionary[stat]+Mathf.FloorToInt(boost),-6,6);
            //Debug.Log("El valor nuevo de "+stat+" es "+Defense);
        }
    }
    public class StatsBoost{
        public Stat stat;
        public int boost;
    }
    public void sumarVidaPercentage(float percentage){
        VidaActual+=Mathf.FloorToInt(VidaMax*percentage);
        if(VidaActual>VidaMax){
            VidaActual=VidaMax;
        }
    }
    public void Update(){
        _vidaactual=VidaActual;
    }
    public int DamageFisic(Monstruo receptor,int poder,Stats.Tipo tipo_ataque){
        float efectividad=Stats.TypeChart.getEffectiveness(tipo_ataque,receptor.Stats.getTipo1)*Stats.TypeChart.getEffectiveness(tipo_ataque,receptor.Stats.getTipo2);
        //Debug.Log("efectividad "+efectividad);
        int damage=Mathf.FloorToInt(efectividad*(((0.2f * this.getLevel + 1) * this.Attack * poder) / (25 * receptor.Defense)) + 2);
        receptor.restarVida(damage);
        return damage;
    }
    public int DamageMagic(Monstruo receptor,int poder, Stats.Tipo tipo_ataque){
        float efectividad=Stats.TypeChart.getEffectiveness(tipo_ataque,receptor.Stats.getTipo1)*Stats.TypeChart.getEffectiveness(tipo_ataque,receptor.Stats.getTipo2);
        //Debug.Log("efectividad "+efectividad);
        int damage=Mathf.FloorToInt(efectividad*(((0.2f * this.getLevel + 1) * this.MagicAttack * poder) / (25 * receptor.MagicDefense)) + 2);
        receptor.restarVida(damage);
        return damage;
    }
    public float percentageVida{
        get{return (float)VidaActual/(float)VidaMax;}
    }
    public int expGain(){
        return _level*_stats.GetexpYield;
    }
    public void setStats(Stats stats){
        _stats=stats;
    }
    public void setLevel(int level){
        _level=level;
    }
}
