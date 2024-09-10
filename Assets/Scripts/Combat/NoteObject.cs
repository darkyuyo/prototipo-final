using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using System.Security.Cryptography.X509Certificates;
using Unity.VisualScripting;
using UnityEngine;

public class NoteObject : MonoBehaviour
{
    public bool canBePressed1;
    public KeyCode keyToPress;
    private bool obtained=false;
    private NoteObject thisNote;
    public GameObject hitEffect,goodEffect,perfectEffect,missEffect;
    private float scorePerNote=1F,scorePerGoodNote=1.25F,scorePerPerfectNote=1.50F;
    private Renderer objectRenderer;
    [SerializeField] UnityEngine.Vector3 EffectPosition;

    void Start()
    {  
        objectRenderer = GetComponent<Renderer>();
        thisNote=this;
        thisNote.SetObjectVisibility(false);
    }

    public void SetObjectVisibility(bool isVisible)
    {
        objectRenderer.enabled = isVisible;
    }

    void Update()
    {
        if(Input.GetKeyDown(keyToPress))
        {
            if(canBePressed1)
            {
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
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D other){
        if(other.tag=="Button 1"){
            canBePressed1=true;
        }
        if(other.tag=="Barras 1"){
            thisNote.SetObjectVisibility(true);
        }
    }
    private void OnTriggerExit2D(Collider2D other){
        if(other.tag=="Button 1"){
            canBePressed1=false;
            if(!obtained){
                EffectPosition.x=transform.position.x;
                EffectPosition.y=transform.position.y+0.5f;
                EffectPosition.z=transform.position.z;
                GameManager.instance.NoteMissed();
                Instantiate(missEffect,EffectPosition,missEffect.transform.rotation);
            }
            obtained=false;
        }
        if(other.tag=="Barras 1"){
            thisNote.SetObjectVisibility(false);
        }
    }
    
}