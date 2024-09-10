using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{
    public AudioSource theMusic;
    public PanelInicio panelInicio;
    public Party playerParty, IAParty;
    public bool startPlaying,start=false;
    [SerializeField] Animaciones animaciones;
    public BeatScroller theBS;
    public BeatScrollerAI theBSAI;
    public Timer theTimer;
    public static GameManager instance;
    public HUD hud1Player,hud2Player,hud3Player,hud1AI,hud2AI,hud3AI;
    [SerializeField] Vector3 posicion1Player,posicion2Player,posicion3Player,posicion1AI,posicion2AI,posicion3AI;
    public int currentMultiplier=1,multiplierTracker,currentMultiplierAI=1,multiplierTrackerAI;
    public int[] multiplierThresholds;    
    public  TextMeshProUGUI multiText,multiTextAI;
    [SerializeField] Text hab1, hab2;
    public Monstruo monstruo1Tank,monstruo2Tank,monstruo1Activo,monstruo2Activo;
    [SerializeField] AudioSource musicMiss,musicMissIA;
    // Start is called before the first frame update
    public event Action<bool> BatallaAcabada;
    public void StartGameManager()
    {
        instance=this;
    }

    public void StartBatalla(Party PartyPlayer,Party PartyIA){
        this.playerParty=PartyPlayer;
        this.IAParty=PartyIA;
        currentMultiplierAI=1;
        multiplierTrackerAI=0;
        currentMultiplier=1;
        multiplierTracker=0;
        CurarIA();
        Init();
    }
    public void IniciarNoteHolders(){
        theBS.ScrollerPositionStart();
        theBSAI.ScrollerPositionStart();
    }

    public int ObtenerMultiplier(){
        return currentMultiplier;
    }
    public int ObtenerMultiplierAI(){
        return currentMultiplierAI;
    }
    public void Update()
    {
    }
    public void StartGame(){
        this.theBS.hasStarted=true;
        this.theBSAI.hasStarted=true;
        this.theTimer.hasStarted=true;
        this.animaciones.StartAnim();
        this.StartCoroutine(EmpezarMusica());
    }
    public IEnumerator EmpezarMusica()
    {
        yield return new WaitForSeconds(1);
        theMusic.Play();
        theTimer.InitTimer();
    }
    public void Habilidad1(string nombre_lil,string nombre_habilidad)
    {
        StartCoroutine(habilidad1(nombre_lil,nombre_habilidad));
    }
    public IEnumerator habilidad1(string nombre_lil,string nombre_habilidad)
    {
        hab1.gameObject.SetActive(true);
        hab1.text=nombre_lil+" uso "+nombre_habilidad;
        yield return new WaitForSeconds(2);
        hab1.gameObject.SetActive(false);
    }
    public void Habilidad2(string nombre_lil,string nombre_habilidad)
    {
        StartCoroutine(habilidad2(nombre_lil,nombre_habilidad));
    }
    public IEnumerator habilidad2(string nombre_lil,string nombre_habilidad)
    {
        hab2.gameObject.SetActive(true);
        hab2.text=nombre_lil+" uso "+nombre_habilidad;
        yield return new WaitForSeconds(2);
        hab2.gameObject.SetActive(false);
    }
    public void EndGame(){
        theBS.hasStarted=false;
        theBSAI.hasStarted=false;
        theTimer.hasStarted=false;
        animaciones.EndAnim();
        theMusic.Stop();
    }
    public void NoteHit(){
        if(currentMultiplier-1<multiplierThresholds.Length)
        {
            multiplierTracker++;
            if(multiplierThresholds[currentMultiplier-1]<=multiplierTracker)
            {
                multiplierTracker=0;
                currentMultiplier++;
            }
        }
        multiText.text="Multiplicador: x"+currentMultiplier;
    }
    public void NoteHitAI(){
        if(currentMultiplierAI-1<multiplierThresholds.Length)
        {
            multiplierTrackerAI++;
            if(multiplierThresholds[currentMultiplierAI-1]<=multiplierTrackerAI)
            {
                multiplierTrackerAI=0;
                currentMultiplierAI++;
            }
        }
        multiTextAI.text="Multiplicador: x"+currentMultiplierAI;
    }

    public void HitFisic(Monstruo emisor,Monstruo receptor, float tipoNota){
        float damage=BasicFisic(emisor,receptor)*tipoNota*currentMultiplier;
        if(receptor.restarVida(damage)){
            CambiarIA();
        }
        NoteHit();
    }
    public void HitFisicAI(Monstruo emisor,Monstruo receptor, float tipoNota){
        float damage=BasicFisic(emisor,receptor)*tipoNota*currentMultiplier;
        if(receptor.restarVida(damage)){
            Cambiar();
        }
        NoteHitAI();
    }
    public void HitMagicAI(Monstruo emisor,Monstruo receptor, float tipoNota){
        float damage=BasicMagic(emisor,receptor)*tipoNota*currentMultiplier;
        if(receptor.restarVida(damage)){
            Cambiar();
        }
        NoteHitAI();
    }
    public void HitMagic(Monstruo emisor,Monstruo receptor, float tipoNota){
        float damage=BasicMagic(emisor,receptor)*tipoNota*currentMultiplier;
        if(receptor.restarVida(damage)){
            CambiarIA();
        }
        NoteHit();
    }
    public void NoteMissed(){
        currentMultiplier=1;
        multiplierTracker=0;
        multiText.text="Multiplicador: x"+currentMultiplier;
        animaciones.Miss();
        musicMiss.Play();
    }
    public void NoteMissedAI(){
        currentMultiplierAI=1;
        multiplierTrackerAI=0;
        multiTextAI.text="Multiplicador: x"+currentMultiplierAI;
        animaciones.MissIA();
        musicMissIA.Play();
    }
    public int BasicFisic(Monstruo emisor, Monstruo receptor){
        int efectividad=1,poder=5;
        int damage=efectividad*Mathf.FloorToInt((((0.2f * emisor.getLevel + 1) * emisor.Attack * poder) / (25 * receptor.Defense)) + 2);
        return(damage);
    }
    public int BasicMagic(Monstruo emisor, Monstruo receptor){
        int efectividad=1,poder=5;
        int damage=efectividad*Mathf.FloorToInt((((0.2f * emisor.getLevel + 1) * emisor.MagicAttack * poder) / (25 * receptor.MagicDefense)) + 2);
        return(damage);
    }
    public void CambiarIA(){
        hud1AI.SetEscudo(false);
        hud2AI.SetEscudo(false);
        hud3AI.SetEscudo(false);
        if(monstruo2Activo.isAlive==false){
            CambiarActivoIA();
        }
        if(IAParty.getMonstruo(0).isAlive){
            monstruo2Tank=IAParty.getMonstruo(0);
            hud1AI.SetEscudo(true);
        }
        else if(IAParty.getMonstruo(1).isAlive){
            monstruo2Tank=IAParty.getMonstruo(1);
            hud2AI.SetEscudo(true);
        }
        else if(IAParty.getMonstruo(2).isAlive){
            monstruo2Tank=IAParty.getMonstruo(2);
            hud3AI.SetEscudo(true);
        }
        else{
            int suma_xp=ExperienciaObtenida();
            int cont=1;
            foreach (var monstruo in playerParty.getMonstruos)
            {
                monstruo.Exp+=suma_xp;
                while(monstruo.CheckForLevelUp()){
                    panelInicio.MostrarMensaje(monstruo.Stats.Name,cont);
                }
                cont++;
            }
            EndGame();
            panelInicio.GanasteBatalla(suma_xp);
            Debug.Log("pasa acaba de ganar la batalla");
        }
    }
    public void CurarIA(){
            foreach (var monstruo in IAParty.getMonstruos)
            { 
                monstruo.sumarVida(monstruo.VidaMax);
            }
    }
    public int ExperienciaObtenida(){
        int suma_xp=0;
        foreach (var monstruo in IAParty.getMonstruos)
        {
            suma_xp+=monstruo.expGain();
        }
        return suma_xp;
    }


    public void Cambiar(){
        hud1Player.SetEscudo(false);
        hud2Player.SetEscudo(false);
        hud3Player.SetEscudo(false);
        if(monstruo1Activo.isAlive==false){
            CambiarActivo();
        }
        if(playerParty.getMonstruo(0).isAlive){
            monstruo1Tank=playerParty.getMonstruo(0);
            hud1Player.SetEscudo(true);
        }
        else if(playerParty.getMonstruo(1).isAlive){
            monstruo1Tank=playerParty.getMonstruo(1);
            hud2Player.SetEscudo(true);
        }
        else if(playerParty.getMonstruo(2).isAlive){
            monstruo1Tank=playerParty.getMonstruo(2);
            hud3Player.SetEscudo(true);
        }
        else{
            CurarIA();
            EndGame();
            panelInicio.PerdisteBatalla();
        }
    }
    public void CambiarActivoIA(){
        if(IAParty.getMonstruo(0).isAlive){
            PositionAI(1);
            monstruo2Activo=IAParty.getMonstruo(0);
        }
        else if(IAParty.getMonstruo(1).isAlive){
            PositionAI(2);
            monstruo2Activo=IAParty.getMonstruo(1);
        }
        else if(IAParty.getMonstruo(2).isAlive){
            PositionAI(3);
            monstruo2Activo=IAParty.getMonstruo(2);
        }
    }
    public void CambiarActivo(){
        if(playerParty.getMonstruo(0).isAlive){
            PositionPlayer(1);
            monstruo1Activo=playerParty.getMonstruo(0);
        }
        else if(playerParty.getMonstruo(1).isAlive){
            PositionPlayer(2);
            monstruo1Activo=playerParty.getMonstruo(1);
        }
        else if(playerParty.getMonstruo(2).isAlive){
            PositionPlayer(3);
            monstruo1Activo=playerParty.getMonstruo(2);
        }
    }
    public void RotarActivo(){
        if(monstruo1Activo==playerParty.getMonstruo(0)){
            if(playerParty.getMonstruo(1).isAlive){
                monstruo1Activo=playerParty.getMonstruo(1);
                PositionPlayer(2);
            }
            else if(playerParty.getMonstruo(2).isAlive){
                monstruo1Activo=playerParty.getMonstruo(2);
                PositionPlayer(3);
            }
            else{
                monstruo1Activo=playerParty.getMonstruo(0);
                PositionPlayer(1);
            }
        }
        else if(monstruo1Activo==playerParty.getMonstruo(1)){
            if(playerParty.getMonstruo(2).isAlive){
                monstruo1Activo=playerParty.getMonstruo(2);
                PositionPlayer(3);
            }
            else if(playerParty.getMonstruo(0).isAlive){
                monstruo1Activo=playerParty.getMonstruo(0);
                PositionPlayer(1);
            }
            else{
                monstruo1Activo=playerParty.getMonstruo(1);
                PositionPlayer(2);
            }
        }
        else if(monstruo1Activo==playerParty.getMonstruo(2)){
            if(playerParty.getMonstruo(0).isAlive){
                monstruo1Activo=playerParty.getMonstruo(0);
                PositionPlayer(1);
            }
            else if(playerParty.getMonstruo(1).isAlive){
                monstruo1Activo=playerParty.getMonstruo(1);
                PositionPlayer(2);
            }
            else{
                monstruo1Activo=playerParty.getMonstruo(2);
                PositionPlayer(3);
            }
        }
    }
    public void RotarActivoIA(){
        if(monstruo2Activo==IAParty.getMonstruo(0)){
            if(IAParty.getMonstruo(1).isAlive){
                monstruo2Activo=IAParty.getMonstruo(1);
                PositionAI(2);
            }
            else if(IAParty.getMonstruo(2).isAlive){
                monstruo2Activo=IAParty.getMonstruo(2);
                PositionAI(3);
            }
            else{
                monstruo2Activo=IAParty.getMonstruo(0);
                PositionAI(1);
            }
        }
        else if(monstruo2Activo==IAParty.getMonstruo(1)){
            if(IAParty.getMonstruo(2).isAlive){
                monstruo2Activo=IAParty.getMonstruo(2);
                PositionAI(3);
            }
            else if(IAParty.getMonstruo(0).isAlive){
                monstruo2Activo=IAParty.getMonstruo(0);
                PositionAI(1);
            }
            else{
                monstruo2Activo=IAParty.getMonstruo(1);
                PositionAI(2);
            }
        }
        else if(monstruo2Activo==IAParty.getMonstruo(2)){
            if(IAParty.getMonstruo(0).isAlive){
                monstruo2Activo=IAParty.getMonstruo(0);
                PositionAI(1);
            }
            else if(IAParty.getMonstruo(1).isAlive){
                monstruo2Activo=IAParty.getMonstruo(1);
                PositionAI(2);
            }
            else{
                monstruo2Activo=IAParty.getMonstruo(2);
                PositionAI(3);
            }
        }
    }
    public void Init(){
        getPosition();
        multiText.text="Multiplicador: x1";
        multiTextAI.text="Multiplicador: x1";
        /*monstruo1Tank=playerParty.getMonstruo(0);
        monstruo2Tank=IAParty.getMonstruo(0);
        monstruo1Activo=playerParty.getMonstruo(0);
        monstruo2Activo=IAParty.getMonstruo(0);
        PositionPlayer(1);
        PositionAI(1);*/
        PositionInitial();
        hab1.gameObject.SetActive(false);
        hab2.gameObject.SetActive(false);
    }
    public void getPosition(){
        posicion1Player.x=-5.4f;
        posicion1Player.y=2.5f;
        posicion1Player.z=89.7f;
        posicion2Player.x=-5.4f;
        posicion2Player.y=0.95f;
        posicion2Player.z=89.7f;
        posicion3Player.x=-5.4f;
        posicion3Player.y=-0.6f;
        posicion3Player.z=89.7f;
        posicion1AI.x=10.8f;
        posicion1AI.y=2.5f;
        posicion1AI.z=89.7f;
        posicion2AI.x=10.8f;
        posicion2AI.y=0.95f;
        posicion2AI.z=89.7f;
        posicion3AI.x=10.8f;
        posicion3AI.y=-0.6f;
        posicion3AI.z=89.7f;
    }
    public void PositionInitial(){
        CambiarActivo();
        Cambiar();
        if(playerParty.getMonstruo(0).isAlive){
            PositionPlayer(1);
        }
        else if(playerParty.getMonstruo(1).isAlive){
            PositionPlayer(2);
        }
        else if(playerParty.getMonstruo(2).isAlive){
            PositionPlayer(3);
        }
        CambiarActivoIA();
        CambiarIA();
        if(IAParty.getMonstruo(0).isAlive){
            PositionAI(1);
        }
        else if(IAParty.getMonstruo(1).isAlive){
            PositionAI(2);
        }
        else if(IAParty.getMonstruo(2).isAlive){
            PositionAI(3);
        }
    }

    public void PositionPlayer(int index){
        switch(index){
            case 1:
                hud1Player.moverIcono(posicion1Player);
                if(playerParty.getMonstruo(1).isAlive){
                    hud2Player.moverIcono(posicion2Player);
                    hud3Player.moverIcono(posicion3Player);
                }
                else{
                    hud2Player.moverIcono(posicion3Player);
                    hud3Player.moverIcono(posicion2Player);
                }
            break;
            case 2:
                hud2Player.moverIcono(posicion1Player);
                if(playerParty.getMonstruo(2).isAlive){
                    hud3Player.moverIcono(posicion2Player);
                    hud1Player.moverIcono(posicion3Player);
                }
                else{
                    hud1Player.moverIcono(posicion2Player);
                    hud3Player.moverIcono(posicion3Player);
                }
            break;
            case 3:
                hud3Player.moverIcono(posicion1Player);
                if(playerParty.getMonstruo(0).isAlive){
                    hud1Player.moverIcono(posicion2Player);
                    hud2Player.moverIcono(posicion3Player);
                }
                else{
                    hud2Player.moverIcono(posicion2Player);
                    hud1Player.moverIcono(posicion3Player);
                }
            break;
        }
    }
        public void PositionAI(int index){
        switch(index){
            case 1:
                hud1AI.moverIcono(posicion1AI);
                if(IAParty.getMonstruo(1).isAlive){
                    hud2AI.moverIcono(posicion2AI);
                    hud3AI.moverIcono(posicion3AI);
                }
                else{
                    hud2AI.moverIcono(posicion2AI);
                    hud3AI.moverIcono(posicion3AI);
                }
            break;
            case 2:
                hud2AI.moverIcono(posicion1AI);
                if(IAParty.getMonstruo(2).isAlive){
                    hud3AI.moverIcono(posicion2AI);
                    hud1AI.moverIcono(posicion3AI);
                }
                else{
                    hud1AI.moverIcono(posicion2AI);
                    hud3AI.moverIcono(posicion3AI);
                }
            break;
            case 3:
                hud3AI.moverIcono(posicion1AI);
                if(IAParty.getMonstruo(0).isAlive){
                    hud1AI.moverIcono(posicion2AI);
                    hud2AI.moverIcono(posicion3AI);
                }
                else{
                    hud2AI.moverIcono(posicion2AI);
                    hud1AI.moverIcono(posicion3AI);
                }
            break;
        }
    }
    public void Batalla(bool playerWin){
        BatallaAcabada(playerWin);
    }
}