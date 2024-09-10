using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PowerObject : MonoBehaviour
{
    public bool canBePressed1;
    private bool obtained=false;
    private PowerObject thisPower;
    public GameObject hitEffect,goodEffect,perfectEffect,missEffect;
    private float scorePerNote=1F,scorePerGoodNote=1.25F,scorePerPerfectNote=1.50F;
    private Renderer objectRenderer;
    [SerializeField] UnityEngine.Vector3 EffectPosition;
    void Start()
    {
        objectRenderer = GetComponent<Renderer>();
        thisPower=this;
        thisPower.SetObjectVisibility(false);
    }
    public void SetObjectVisibility(bool isVisible)
    {
        objectRenderer.enabled = isVisible;
    }
    void Update()
    {
        if(canBePressed1)
        {
            if(Input.GetKeyDown(KeyCode.Q) && GameManager.instance.monstruo1Activo._abilities.Count>=1){if(GameManager.instance.monstruo1Activo._abilities[0]._ability.getValor<=GameManager.instance.currentMultiplier){
                if(GameManager.instance.monstruo1Activo==GameManager.instance.playerParty.getMonstruo(1)){
                    obtained=true;
                    gameObject.SetActive(false);
                    if(transform.position.x>=-4.44 && transform.position.x<=-4.35){
                        GameManager.instance.HitMagic(GameManager.instance.monstruo1Activo, GameManager.instance.monstruo2Tank, scorePerPerfectNote);
                        EffectPosition.x=transform.position.x;
                        EffectPosition.y=transform.position.y+0.5f;
                        EffectPosition.z=transform.position.z;
                        Instantiate(perfectEffect,EffectPosition,perfectEffect.transform.rotation);
                    }
                    else if(transform.position.x>-4.54 && transform.position.x<-4.25){
                        GameManager.instance.HitMagic(GameManager.instance.monstruo1Activo, GameManager.instance.monstruo2Tank, scorePerGoodNote);
                        EffectPosition.x=transform.position.x;
                        EffectPosition.y=transform.position.y+0.5f;
                        EffectPosition.z=transform.position.z;
                        Instantiate(goodEffect,EffectPosition,goodEffect.transform.rotation);
                    }
                    else{
                        GameManager.instance.HitMagic(GameManager.instance.monstruo1Activo, GameManager.instance.monstruo2Tank, scorePerNote);
                        EffectPosition.x=transform.position.x;
                        EffectPosition.y=transform.position.y+0.5f;
                        EffectPosition.z=transform.position.z;
                        Instantiate(hitEffect,EffectPosition,hitEffect.transform.rotation);
                    }
                }
                else{
                    obtained=true;
                    gameObject.SetActive(false);
                    if(transform.position.x>=-4.44 && transform.position.x<=-4.35){
                        GameManager.instance.HitFisic(GameManager.instance.monstruo1Activo, GameManager.instance.monstruo2Tank, scorePerPerfectNote);
                        EffectPosition.x=transform.position.x;
                        EffectPosition.y=transform.position.y+0.5f;
                        EffectPosition.z=transform.position.z;
                        Instantiate(perfectEffect,EffectPosition,perfectEffect.transform.rotation);
                    }
                    else if(transform.position.x>-4.54 && transform.position.x<-4.25){
                        GameManager.instance.HitFisic(GameManager.instance.monstruo1Activo, GameManager.instance.monstruo2Tank, scorePerGoodNote);
                        EffectPosition.x=transform.position.x;
                        EffectPosition.y=transform.position.y+0.5f;
                        EffectPosition.z=transform.position.z;
                        Instantiate(goodEffect,EffectPosition,goodEffect.transform.rotation);
                    }
                    else{
                        GameManager.instance.HitFisic(GameManager.instance.monstruo1Activo, GameManager.instance.monstruo2Tank, scorePerNote);
                        EffectPosition.x=transform.position.x;
                        EffectPosition.y=transform.position.y+0.5f;
                        EffectPosition.z=transform.position.z;
                        Instantiate(hitEffect,EffectPosition,hitEffect.transform.rotation);
                    }
                }
                if(GameManager.instance.monstruo2Tank!=null){
                    GameManager.instance.monstruo1Activo._abilities[0]._ability.Activate(true);
                    GameManager.instance.Habilidad1(GameManager.instance.monstruo1Activo.Stats.Name,GameManager.instance.monstruo1Activo._abilities[0]._ability.getName);
                    GameManager.instance.RotarActivo();                    
                }
            }
            }
            if(Input.GetKeyDown(KeyCode.W) && GameManager.instance.monstruo1Activo._abilities.Count>=2){if(GameManager.instance.monstruo1Activo._abilities[1]._ability.getValor<=GameManager.instance.currentMultiplier){
                if(GameManager.instance.monstruo1Activo==GameManager.instance.playerParty.getMonstruo(1)){
                    obtained=true;
                    gameObject.SetActive(false);
                    if(transform.position.x>=-4.44 && transform.position.x<=-4.35){
                        GameManager.instance.HitMagic(GameManager.instance.monstruo1Activo, GameManager.instance.monstruo2Tank, scorePerPerfectNote);
                        EffectPosition.x=transform.position.x;
                        EffectPosition.y=transform.position.y+0.5f;
                        EffectPosition.z=transform.position.z;
                        Instantiate(perfectEffect,EffectPosition,perfectEffect.transform.rotation);
                    }
                    else if(transform.position.x>-4.54 && transform.position.x<-4.25){
                        GameManager.instance.HitMagic(GameManager.instance.monstruo1Activo, GameManager.instance.monstruo2Tank, scorePerGoodNote);
                        EffectPosition.x=transform.position.x;
                        EffectPosition.y=transform.position.y+0.5f;
                        EffectPosition.z=transform.position.z;
                        Instantiate(goodEffect,EffectPosition,goodEffect.transform.rotation);
                    }
                    else{
                        GameManager.instance.HitMagic(GameManager.instance.monstruo1Activo, GameManager.instance.monstruo2Tank, scorePerNote);
                        EffectPosition.x=transform.position.x;
                        EffectPosition.y=transform.position.y+0.5f;
                        EffectPosition.z=transform.position.z;
                        Instantiate(hitEffect,EffectPosition,hitEffect.transform.rotation);
                    }
                }
                else{
                    obtained=true;
                    gameObject.SetActive(false);
                    if(transform.position.x>=-4.44 && transform.position.x<=-4.35){
                        GameManager.instance.HitFisic(GameManager.instance.monstruo1Activo, GameManager.instance.monstruo2Tank, scorePerPerfectNote);
                        EffectPosition.x=transform.position.x;
                        EffectPosition.y=transform.position.y+0.5f;
                        EffectPosition.z=transform.position.z;
                        Instantiate(perfectEffect,EffectPosition,perfectEffect.transform.rotation);
                    }
                    else if(transform.position.x>-4.54 && transform.position.x<-4.25){
                        GameManager.instance.HitFisic(GameManager.instance.monstruo1Activo, GameManager.instance.monstruo2Tank, scorePerGoodNote);
                        EffectPosition.x=transform.position.x;
                        EffectPosition.y=transform.position.y+0.5f;
                        EffectPosition.z=transform.position.z;
                        Instantiate(goodEffect,EffectPosition,goodEffect.transform.rotation);
                    }
                    else{
                        GameManager.instance.HitFisic(GameManager.instance.monstruo1Activo, GameManager.instance.monstruo2Tank, scorePerNote);
                        EffectPosition.x=transform.position.x;
                        EffectPosition.y=transform.position.y+0.5f;
                        EffectPosition.z=transform.position.z;
                        Instantiate(hitEffect,EffectPosition,hitEffect.transform.rotation);
                    }
                }
                if(GameManager.instance.monstruo2Tank!=null){
                    GameManager.instance.monstruo1Activo._abilities[1]._ability.Activate(true);
                    GameManager.instance.Habilidad1(GameManager.instance.monstruo1Activo.Stats.Name,GameManager.instance.monstruo1Activo._abilities[1]._ability.getName);
                    GameManager.instance.RotarActivo();                    
                }
            }
            }
            if(Input.GetKeyDown(KeyCode.E) &&  GameManager.instance.monstruo1Activo._abilities.Count>=3){if(GameManager.instance.monstruo1Activo._abilities[2]._ability.getValor<=GameManager.instance.currentMultiplier){
                if(GameManager.instance.monstruo1Activo==GameManager.instance.playerParty.getMonstruo(1)){
                    obtained=true;
                    gameObject.SetActive(false);
                    if(transform.position.x>=-4.44 && transform.position.x<=-4.35){
                        GameManager.instance.HitMagic(GameManager.instance.monstruo1Activo, GameManager.instance.monstruo2Tank, scorePerPerfectNote);
                        EffectPosition.x=transform.position.x;
                        EffectPosition.y=transform.position.y+0.5f;
                        EffectPosition.z=transform.position.z;
                        Instantiate(perfectEffect,EffectPosition,perfectEffect.transform.rotation);
                    }
                    else if(transform.position.x>-4.54 && transform.position.x<-4.25){
                        GameManager.instance.HitMagic(GameManager.instance.monstruo1Activo, GameManager.instance.monstruo2Tank, scorePerGoodNote);
                        EffectPosition.x=transform.position.x;
                        EffectPosition.y=transform.position.y+0.5f;
                        EffectPosition.z=transform.position.z;
                        Instantiate(goodEffect,EffectPosition,goodEffect.transform.rotation);
                    }
                    else{
                        GameManager.instance.HitMagic(GameManager.instance.monstruo1Activo, GameManager.instance.monstruo2Tank, scorePerNote);
                        EffectPosition.x=transform.position.x;
                        EffectPosition.y=transform.position.y+0.5f;
                        EffectPosition.z=transform.position.z;
                        Instantiate(hitEffect,EffectPosition,hitEffect.transform.rotation);
                    }
                }
                else{
                    obtained=true;
                    gameObject.SetActive(false);
                    if(transform.position.x>=-4.44 && transform.position.x<=-4.35){
                        GameManager.instance.HitFisic(GameManager.instance.monstruo1Activo, GameManager.instance.monstruo2Tank, scorePerPerfectNote);
                        EffectPosition.x=transform.position.x;
                        EffectPosition.y=transform.position.y+0.5f;
                        EffectPosition.z=transform.position.z;
                        Instantiate(perfectEffect,EffectPosition,perfectEffect.transform.rotation);
                    }
                    else if(transform.position.x>-4.54 && transform.position.x<-4.25){
                        GameManager.instance.HitFisic(GameManager.instance.monstruo1Activo, GameManager.instance.monstruo2Tank, scorePerGoodNote);
                        EffectPosition.x=transform.position.x;
                        EffectPosition.y=transform.position.y+0.5f;
                        EffectPosition.z=transform.position.z;
                        Instantiate(goodEffect,EffectPosition,goodEffect.transform.rotation);
                    }
                    else{
                        GameManager.instance.HitFisic(GameManager.instance.monstruo1Activo, GameManager.instance.monstruo2Tank, scorePerNote);
                        EffectPosition.x=transform.position.x;
                        EffectPosition.y=transform.position.y+0.5f;
                        EffectPosition.z=transform.position.z;
                        Instantiate(hitEffect,EffectPosition,hitEffect.transform.rotation);
                    }
                }
                if(GameManager.instance.monstruo2Tank!=null){
                    GameManager.instance.monstruo1Activo._abilities[2]._ability.Activate(true);
                    GameManager.instance.Habilidad1(GameManager.instance.monstruo1Activo.Stats.Name,GameManager.instance.monstruo1Activo._abilities[2]._ability.getName);
                    GameManager.instance.RotarActivo();                    
                }
            }
            }
            if(Input.GetKeyDown(KeyCode.R) &&  GameManager.instance.monstruo1Activo._abilities.Count>=4){if(GameManager.instance.monstruo1Activo._abilities[3]._ability.getValor<=GameManager.instance.currentMultiplier){
                if(GameManager.instance.monstruo1Activo==GameManager.instance.playerParty.getMonstruo(1)){
                    obtained=true;
                    gameObject.SetActive(false);
                    if(transform.position.x>=-4.44 && transform.position.x<=-4.35){
                        GameManager.instance.HitMagic(GameManager.instance.monstruo1Activo, GameManager.instance.monstruo2Tank, scorePerPerfectNote);
                        EffectPosition.x=transform.position.x;
                        EffectPosition.y=transform.position.y+0.5f;
                        EffectPosition.z=transform.position.z;
                        Instantiate(perfectEffect,EffectPosition,perfectEffect.transform.rotation);
                    }
                    else if(transform.position.x>-4.54 && transform.position.x<-4.25){
                        GameManager.instance.HitMagic(GameManager.instance.monstruo1Activo, GameManager.instance.monstruo2Tank, scorePerGoodNote);
                        EffectPosition.x=transform.position.x;
                        EffectPosition.y=transform.position.y+0.5f;
                        EffectPosition.z=transform.position.z;
                        Instantiate(goodEffect,EffectPosition,goodEffect.transform.rotation);
                    }
                    else{
                        GameManager.instance.HitMagic(GameManager.instance.monstruo1Activo, GameManager.instance.monstruo2Tank, scorePerNote);
                        EffectPosition.x=transform.position.x;
                        EffectPosition.y=transform.position.y+0.5f;
                        EffectPosition.z=transform.position.z;
                        Instantiate(hitEffect,EffectPosition,hitEffect.transform.rotation);
                    }
                }
                else{
                    obtained=true;
                    gameObject.SetActive(false);
                    if(transform.position.x>=-4.44 && transform.position.x<=-4.35){
                        GameManager.instance.HitFisic(GameManager.instance.monstruo1Activo, GameManager.instance.monstruo2Tank, scorePerPerfectNote);
                        EffectPosition.x=transform.position.x;
                        EffectPosition.y=transform.position.y+0.5f;
                        EffectPosition.z=transform.position.z;
                        Instantiate(perfectEffect,EffectPosition,perfectEffect.transform.rotation);
                    }
                    else if(transform.position.x>-4.54 && transform.position.x<-4.25){
                        GameManager.instance.HitFisic(GameManager.instance.monstruo1Activo, GameManager.instance.monstruo2Tank, scorePerGoodNote);
                        EffectPosition.x=transform.position.x;
                        EffectPosition.y=transform.position.y+0.5f;
                        EffectPosition.z=transform.position.z;
                        Instantiate(goodEffect,EffectPosition,goodEffect.transform.rotation);
                    }
                    else{
                        GameManager.instance.HitFisic(GameManager.instance.monstruo1Activo, GameManager.instance.monstruo2Tank, scorePerNote);
                        EffectPosition.x=transform.position.x;
                        EffectPosition.y=transform.position.y+0.5f;
                        EffectPosition.z=transform.position.z;
                        Instantiate(hitEffect,EffectPosition,hitEffect.transform.rotation);
                    }
                }
                if(GameManager.instance.monstruo2Tank!=null){
                    GameManager.instance.monstruo1Activo._abilities[3]._ability.Activate(true);
                    GameManager.instance.Habilidad1(GameManager.instance.monstruo1Activo.Stats.Name,GameManager.instance.monstruo1Activo._abilities[3]._ability.getName);
                    GameManager.instance.RotarActivo();                    
                }
            }
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D other){
        if(other.tag=="Button 1"){
            canBePressed1=true;
        }
        if(other.tag=="Barras 1"){
            thisPower.SetObjectVisibility(true);
        }
    }
    private void OnTriggerExit2D(Collider2D other){
        if(other.tag=="Button 1"){
            canBePressed1=false;
            if(!obtained){
                GameManager.instance.NoteMissed();
                EffectPosition.x=transform.position.x;
                EffectPosition.y=transform.position.y+0.5f;
                EffectPosition.z=transform.position.z;
                Instantiate(missEffect,EffectPosition,missEffect.transform.rotation);
            }
            obtained=false;
        }
        if(other.tag=="Barras 1"){
            thisPower.SetObjectVisibility(false);
        }
    }
}