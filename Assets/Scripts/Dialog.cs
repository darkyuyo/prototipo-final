using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialog
{
    [SerializeField] List<string> dialogLines;
    [SerializeField] List<string> namesLines;
    [SerializeField] List<string> dialogLines2;
    [SerializeField] List<string> namesLines2;
    [SerializeField] bool fight;
    [SerializeField] bool heal;
    [SerializeField] bool moreDialog;
    [SerializeField] int cont=0;
    [SerializeField] int battlenumber;
    [SerializeField] GameObject NPC;
    [SerializeField] bool Malvado,LilsMalvados;

    public List<string> Lines{
        get{
            return dialogLines;
        }
    }
    public List<string> Lines2{
        get{
            return dialogLines2;
        }
    }
    public string getLineaNombre(int numero){
        return namesLines[numero];
    }
    public string getLineaNombre2(int numero){
        return namesLines2[numero];
    }
    public bool Fight{
        get{
            return fight;
        }
    }
    public bool Heal{
        get{
            return heal;
        }
    }
    public int BattleNumber{
        get{
            return battlenumber;
        }
    }
    public void sumarCont(){
        cont++;
    }
    public bool ContPar(){
        return cont%2==0;
    }
    public bool MoreDialog(){
        return moreDialog;   
    }
    public void desaparecerNPC(){
        if(Malvado){
            GameController.Instance.sumarContMalvado();
        }
        else if(LilsMalvados){
            GameController.Instance.sumarContLilsMalvados();
        }
        GameController.Instance.LlamarWait(NPC);
    }
}