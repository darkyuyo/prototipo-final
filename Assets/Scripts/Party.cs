using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

//[System.Serializable]
public class Party : MonoBehaviour
{
    public List<Monstruo> Monsters=new List<Monstruo>();

    // Start is called before the first frame update
    void Start()
    {
        foreach (var monstruo in Monsters){
            monstruo.Init();
        }
        //Debug.Log("El nombre de mi monstruo es: "+Monsters[0].Stats.Name+" y su nivel es: "+Monsters[0].Level+ " y su vida actual es: "+Monsters[0].VidaActual);
    }
    public void Init(){
        foreach (var monstruo in Monsters){
            monstruo.Init();
        }
    }
    public Monstruo getMonstruo(int index){
        return Monsters[index];
    }
    public List<Monstruo> getMonstruos{
        get{
            return Monsters;
        }
    }
    public void setMonstruo(int index, Monstruo monstruo){
        Monsters[index]=monstruo;
    }
    public void ClearMonstruos(){
        Monsters.Clear();
    }
    public void SetMonsters(List<Monstruo> monstruos){
        Monsters=monstruos;
    }
    public void AddMonstruo(Monstruo monstruo){
        Monsters.Add(monstruo);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
