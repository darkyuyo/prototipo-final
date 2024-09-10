using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "Stats", menuName = "Stats", order = 0)]
public class Stats : ScriptableObject
{
    [SerializeField] string idName;
    [SerializeField] Sprite sprite;
    [SerializeField] Sprite icon;
    [SerializeField] int vida_max;
    [SerializeField] int attack;
    [SerializeField] int defense;
    [SerializeField] int magic_attack;
    [SerializeField] int magic_defense;
    [SerializeField] int expYield;
    [SerializeField] GrowType growType;
    public int getExpForLevel(int level){
        if(growType==GrowType.Fast){
            return 4*(level*level*level)/5;
        }
        else if(growType==GrowType.MediumFast)
        {
            return level*level*level;
        }
        else{
            return 0;
        }
    }

    [SerializeField] Tipo tipo1;
    [SerializeField] Tipo tipo2;
    public enum Tipo
    {
        None,
        Normal,
        Fuego,
        Agua,
        Electrico,
        Planta,
        Hielo,
        Pelea,
        Veneno,
        Tierra,
        Volador,
        Psiquico,
        Bicho,
        Piedra,
        Fantasma,
        Dragon,
    }
    public enum GrowType{
        Fast,
        MediumFast,
    }
    public class TypeChart{
        static float[][] chart={
            //                      Nor  Fue  Agua Ele  Pla  Hie  Pele Ven  Tie  Vol  Psi  Bic  Pie  Fan  Dra
            /*Normal*/ new float[]{ 1f,  1f,   1f,  1f, 1f,  1f,   1f,  1f,  1f,  1f,  1f, 1f,  1f, 0f,  1f},
            /*Fuego*/ new float[]{  1f, 0.5f, 0.5f, 1f, 2f,  2f, 0.5f,  1f,  1f,  1f,  1f, 2f, 0.5f, 1f, 0.5f},
            /*Agua*/ new float[]{   1f, 2f, 0.5f, 1f, 0.5f, 1f,  1f,  1f,  2f,  1f,  1f, 1f,  2f, 1f,  0.5f},
            /*Electrico*/ new float[]{ 1f, 1f,  2f, 0.5f, 0.5f, 1f,  1f,  1f,  0f,  2f,  1f, 1f,  1f, 1f,  0.5f},
            /*Planta*/ new float[]{  1f, 0.5f, 2f,  1f, 0.5f, 1f,  1f, 0.5f, 2f, 0.5f, 1f, 0.5f, 2f, 1f,  0.5f},
            /*Hielo*/ new float[]{   1f, 0.5f, 0.5f, 1f, 2f,  0.5f, 1f,  1f,  2f,  2f,  1f, 1f,  1f, 1f,  2f},
            /*Pelea*/ new float[]{  2f,  1f,  1f,  1f, 1f,  2f,  1f, 0.5f, 1f, 0.5f, 0.5f, 0.5f, 2f, 0f,  1f},
            /*Veneno*/ new float[]{ 1f,  1f,  1f,  1f, 2f,  1f,  1f, 0.5f, 0.5f, 1f,  1f, 0.5f, 0.5f, 0.5f, 1f},
            /*Tierra*/ new float[]{ 1f,  2f,  1f,  2f, 0.5f, 1f,  1f,  2f,  1f,  0f,  1f, 0.5f, 2f, 1f,  1f},
            /*Volador*/ new float[]{1f,  1f,  1f,  0.5f, 2f,  1f,  2f,  1f,  1f,  1f,  1f, 2f,  0.5f, 1f,  1f},
            /*Psiquico*/ new float[]{1f,  1f,  1f,  1f, 1f,  1f,  2f,  2f,  1f,  1f, 0.5f, 1f,  1f, 1f,  1f},
            /*Bicho*/ new float[]{   1f, 0.5f, 1f,  1f, 2f,  1f,  0.5f, 0.5f, 1f,  0.5f, 2f, 1f,  1f, 0.5f, 1f},
            /*Piedra*/ new float[]{  1f, 2f,  1f,  1f, 1f,  2f,  0.5f, 1f,  0.5f, 2f,  1f, 2f,  1f, 1f,  1f},
            /*Fantasma*/ new float[]{1f,  1f,  1f,  1f, 1f,  1f,  1f,  1f,  1f,  1f,  2f, 1f,  1f, 2f,  1f},
            /*Dragon*/ new float[]{  1f, 0.5f, 0.5f, 0.5f, 0.5f, 2f,  1f,  1f,  1f,  1f,  1f, 1f,  1f, 1f,  2f},
        };
        public static float getEffectiveness(Tipo atacante, Tipo defensor){
            if(atacante==Tipo.None || defensor==Tipo.None){
                return 1f;
            }
            int row=(int)atacante-1;
            int col=(int)defensor-1;
            return chart[row][col];
        }
    }
    [SerializeField] List<MovimientosAprendibles> movimientos_aprendibles;
    [System.Serializable]
    public class MovimientosAprendibles{
        [SerializeField] AbilityBase habilidadBase;
        [SerializeField] int nivel;
        public AbilityBase getHabilidad{
            get{return habilidadBase;}
        }
        public int getNivel{
            get{return nivel;}
        }
    }
    public string Name{
        get{return idName;}
    }
    public int getVidaMax{
        get{return vida_max;}
    }
    public int getDefense{
        get{return defense;}
    }
    public int getMagicDefense{
        get{return magic_defense;}
    }
    public int getAttack{
        get{return attack;}
    }
    public int getMagicAttack{
        get{return magic_attack;}
    }
    public Sprite getSprite{
        get{return sprite;}
    }
    public Sprite getIcon{
        get{return icon;}
    }
    public Tipo getTipo1{
        get{return tipo1;}
    }
    public Tipo getTipo2{
        get{return tipo2;}
    }
    public List<MovimientosAprendibles> getMovimientos_aprendibles{
        get{return movimientos_aprendibles;}
    }
    public int GetexpYield{
        get{return expYield;}
    }
    public GrowType getGrowType{
        get{return growType;}
    }
        public enum Stat{
        Ataque,
        Defensa,
        AtaqueMagico,
        DefensaMagica,}
}
