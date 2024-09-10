using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using System;

public enum GameState{FreeRoam, Battle,Dialog,Menu}
public class GameController : MonoBehaviour
{
    GameState state;
    public static GameController Instance { get; private set; }

    [SerializeField]Party IAParty,MalezaParty;
    [SerializeField] List<Monstruo> MonstersIA=new List<Monstruo>();
    [SerializeField] PersonajeMovimiento player;
    [SerializeField] GameManager battleSystem, battleSystem2,battleSystemMaleza;
    [SerializeField] GameManager battleSystemTest;
    [SerializeField] MenuPausa MenuPausa;
    [SerializeField] Camera worldCamera,GrayCamera;
    [SerializeField] int valor;
    [SerializeField] Stats Carlitos,Devuelvanlo,Escolder,Jepasar,Palgun,Pihongo,Polution,SadBunny,Sipo,UCI;
    [SerializeField] GameObject Menu,NPCborrado, NPCnuevo,NPCborrado2,NPCnuevo2,Audifonos;
    [SerializeField] int contMalvado=0,contLilsMalvados=0;
    //[SerializeField] Monstruo Monstruo1,Monstruo2,Monstruo3;
    public event Action OnEncountered;

    private void Awake()
    {
        Instance = this;
    }
    public void SetValor(int valor){
        this.valor=valor;
    }
    public void SetIAParty(Party party){
        IAParty=party;
    }
    void Start()
    {
        //player.OnEncountered+=StartBattle;
        DialogManagement.Instance.OnEncountered+=StartBattle;
        OnEncountered+=StartBattle;
        battleSystem.BatallaAcabada+=EndBattle;
        battleSystem2.BatallaAcabada+=EndBattle;
        battleSystemMaleza.BatallaAcabada+=EndBattle;
        battleSystemTest.BatallaAcabada+=EndBattle;
        DialogManagement.Instance.OnShowDialog+=()=>state=GameState.Dialog;
        DialogManagement.Instance.OnCloseDialog+=()=>state=GameState.FreeRoam;

    }
    void StartBattle()
    {
        //int valor=Random.Range(1,4);
        state=GameState.Battle;
        var playerParty=player.GetComponent<Party>();
        player.gameObject.SetActive(false);
        player.BaseMusic.gameObject.SetActive(false);
        switch(valor){
            case 1:
                battleSystem.gameObject.SetActive(true);
                battleSystem.StartGameManager();
                battleSystem.StartBatalla(playerParty,IAParty);
                battleSystem.panelInicio.Inicio();
                break;
            case 2:
                battleSystem2.gameObject.SetActive(true);
                battleSystem2.StartGameManager();
                battleSystem2.StartBatalla(playerParty,IAParty);
                battleSystem2.panelInicio.Inicio();
                break;
            case 3:
                battleSystemMaleza.gameObject.SetActive(true);
                battleSystemMaleza.StartGameManager();
                battleSystemMaleza.StartBatalla(playerParty,IAParty);
                battleSystemMaleza.panelInicio.Inicio();
                break;
            case 4:
                battleSystemTest.gameObject.SetActive(true);
                battleSystemTest.StartGameManager();
                battleSystemTest.StartBatalla(playerParty,IAParty);
                battleSystemTest.panelInicio.Inicio();
                break;
        }
        worldCamera.gameObject.SetActive(false);
    }
    void EndBattle(bool won){
        state=GameState.FreeRoam;
        battleSystem.gameObject.SetActive(false);
        battleSystem2.gameObject.SetActive(false);
        battleSystemMaleza.gameObject.SetActive(false);
        battleSystemTest.gameObject.SetActive(false);
        foreach (var monstruo in IAParty.Monsters)
        {
            monstruo.Quitarboosts();
        }
        foreach (var monstruo in player.GetComponent<Party>().Monsters)
        {
            monstruo.Quitarboosts();
        }
        worldCamera.gameObject.SetActive(true);
        player.gameObject.SetActive(true);
        player.BaseMusic.gameObject.SetActive(true);
        player.BaseMusic.Play();
        player.Interact();
    }
    public void RandomBattle(){
        MalezaParty.ClearMonstruos();
        MonstersIA.Clear();
        for (int i = 0; i < 3; i++)
        {
            MonstersIA.Add(new Monstruo());
            int random1 = UnityEngine.Random.Range(1, 10);
            int random2 = UnityEngine.Random.Range(1, 3);
            switch (random1)
            {
                case 1:
                    MonstersIA[i].setStats(Carlitos);
                    MonstersIA[i].setLevel(random2);
                    break;
                case 2:
                    MonstersIA[i].setStats(Devuelvanlo);
                    MonstersIA[i].setLevel(random2);
                    break;
                case 3:
                    MonstersIA[i].setStats(Escolder);
                    MonstersIA[i].setLevel(random2);
                    break;
                case 4:
                    MonstersIA[i].setStats(Jepasar);
                    MonstersIA[i].setLevel(random2);
                    break;
                case 5:
                    MonstersIA[i].setStats(Palgun);
                    MonstersIA[i].setLevel(random2);
                    break;
                case 6:
                    MonstersIA[i].setStats(Pihongo);
                    MonstersIA[i].setLevel(random2);
                    break;
                case 7:
                    MonstersIA[i].setStats(Polution);
                    MonstersIA[i].setLevel(random2);
                    break;
                case 8:
                    MonstersIA[i].setStats(SadBunny);
                    MonstersIA[i].setLevel(random2);
                    break;
                case 9:
                    MonstersIA[i].setStats(Sipo);
                    MonstersIA[i].setLevel(random2);
                    break;
                case 10:
                    MonstersIA[i].setStats(UCI);
                    MonstersIA[i].setLevel(random2);
                    break;
            }
        }
        MalezaParty.SetMonsters(MonstersIA);
        MalezaParty.Init();
        SetIAParty(MalezaParty);
        int random3 = UnityEngine.Random.Range(1, 3);
        SetValor(random3);
        OnEncountered();
    }
    public void OpenMenu(){
        state=GameState.Menu;
        Menu.gameObject.SetActive(true);
        MenuPausa.Iniciar();
        
    }
    public void CloseMenu(){
        state=GameState.FreeRoam;
        Menu.gameObject.SetActive(false);
        MenuPausa.CerrarMenus();

    }

    // Update is called once per frame
    void Update()
    {
        if(state==GameState.FreeRoam)
        {
            player.HandleUpdate();
        }
        else if(state==GameState.Battle)
        {
            //Battle
        }
        else if(state==GameState.Dialog)
        {
            DialogManagement.Instance.HandleUpdate();
            player. _direccionMovimiento.x=0f;
            player. _direccionMovimiento.y=0f;
        }
        else if(state==GameState.Menu)
        {
            player. _direccionMovimiento.x=0f;
            player. _direccionMovimiento.y=0f;
            if(Input.GetKeyDown(KeyCode.X))
            {
                CloseMenu();
            }
        }
    }
    public void ChangeCamera(){
        worldCamera.gameObject.SetActive(!worldCamera.gameObject.activeSelf);
        GrayCamera.gameObject.SetActive(!GrayCamera.gameObject.activeSelf);
    }
    public IEnumerator Wait(GameObject GO)
    {
        ChangeCamera();
        GO.SetActive(false);
        if(contMalvado==2){
            NPCnuevo.SetActive(true);
            contMalvado=0;
        }
        else if(contLilsMalvados==1){
            NPCnuevo2.SetActive(true);
            Audifonos.SetActive(true);
            contLilsMalvados=0;
        }
        yield return new WaitForSeconds(0.5f);
        ChangeCamera();
    }
    public void LlamarWait(GameObject GO){
        StartCoroutine(Wait(GO));
    }
    public void sumarContMalvado(){
        contMalvado++;
    }
    public void sumarContLilsMalvados(){
        contLilsMalvados++;
    }
}
