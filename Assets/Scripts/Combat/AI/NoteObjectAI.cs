using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class NoteObjectAI : MonoBehaviour
{
    public bool canBePressed2, canNormal, canGood, canPerfect;
    private bool obtained=false;
    private NoteObjectAI thisNote;
    public GameObject hitEffect,goodEffect,perfectEffect,missEffect;
    private float scorePerNote=1F,scorePerGoodNote=1.25F,scorePerPerfectNote=1.50F;
    private Renderer objectRenderer;
    public int numeroAleatorio,percentNormal=80,percentGood=40,percentPerfect=15,percentMiss=100;
    [SerializeField] UnityEngine.Vector3 EffectPosition;

    void Start()
    {  
        objectRenderer = GetComponent<Renderer>();
        thisNote=this;
        thisNote.SetObjectVisibility(false);
        numeroAleatorio = Random.Range(0,100);
    }

    public void SetObjectVisibility(bool isVisible)
    {
        objectRenderer.enabled = isVisible;
    }

    void Update()
    {
        if(canBePressed2)
        {
            if(GameManager.instance.monstruo2Activo==GameManager.instance.IAParty.getMonstruo(1)){
                if(percentPerfect>=numeroAleatorio && numeroAleatorio>0 && canPerfect){
                    obtained=true;
                    gameObject.SetActive(false);
                    GameManager.instance.HitMagicAI(GameManager.instance.monstruo2Activo, GameManager.instance.monstruo1Tank, scorePerPerfectNote);
                    EffectPosition.x=transform.position.x;
                    EffectPosition.y=transform.position.y+0.5f;
                    EffectPosition.z=transform.position.z;
                    Instantiate(perfectEffect,EffectPosition,perfectEffect.transform.rotation);
                    //Debug.Log("mi numero es: "+numeroAleatorio+" y el porcentaje de perfect es: "+percentPerfect);
                }
                else if(percentGood>=numeroAleatorio && numeroAleatorio>percentPerfect && canGood){
                    obtained=true;
                    gameObject.SetActive(false);
                    GameManager.instance.HitMagicAI(GameManager.instance.monstruo2Activo, GameManager.instance.monstruo1Tank, scorePerGoodNote);
                    EffectPosition.x=transform.position.x;
                    EffectPosition.y=transform.position.y+0.5f;
                    EffectPosition.z=transform.position.z;
                    Instantiate(goodEffect,EffectPosition,goodEffect.transform.rotation);
                    //Debug.Log("mi numero es: "+numeroAleatorio+" y el porcentaje de good es: "+percentGood);
                }
                else if(percentNormal>=numeroAleatorio && numeroAleatorio>percentGood && canNormal){
                    obtained=true;
                    gameObject.SetActive(false);
                    GameManager.instance.HitMagicAI(GameManager.instance.monstruo2Activo, GameManager.instance.monstruo1Tank, scorePerNote);
                    EffectPosition.x=transform.position.x;
                    EffectPosition.y=transform.position.y+0.5f;
                    EffectPosition.z=transform.position.z;
                    Instantiate(hitEffect,EffectPosition,hitEffect.transform.rotation);
                    //Debug.Log("mi numero es: "+numeroAleatorio+" y el porcentaje de normal es: "+percentNormal);
                }
            }
            else{
                if(percentPerfect>=numeroAleatorio && numeroAleatorio>0 && canPerfect){
                    obtained=true;
                    gameObject.SetActive(false);
                    GameManager.instance.HitFisicAI(GameManager.instance.monstruo2Activo, GameManager.instance.monstruo1Tank, scorePerPerfectNote);
                    EffectPosition.x=transform.position.x;
                    EffectPosition.y=transform.position.y+0.5f;
                    EffectPosition.z=transform.position.z;
                    Instantiate(perfectEffect,EffectPosition,perfectEffect.transform.rotation);
                    //Debug.Log("mi numero es: "+numeroAleatorio+" y el porcentaje de perfect es: "+percentPerfect);
                }
                else if(percentGood>=numeroAleatorio && numeroAleatorio>percentPerfect && canGood){
                    obtained=true;
                    gameObject.SetActive(false);
                    GameManager.instance.HitFisicAI(GameManager.instance.monstruo2Activo, GameManager.instance.monstruo1Tank, scorePerGoodNote);
                    EffectPosition.x=transform.position.x;
                    EffectPosition.y=transform.position.y+0.5f;
                    EffectPosition.z=transform.position.z;
                    Instantiate(goodEffect,EffectPosition,goodEffect.transform.rotation);
                    //Debug.Log("mi numero es: "+numeroAleatorio+" y el porcentaje de good es: "+percentGood);
                }
                else if(percentNormal>=numeroAleatorio&& numeroAleatorio>percentGood && canNormal){
                    obtained=true;
                    gameObject.SetActive(false);
                    GameManager.instance.HitFisicAI(GameManager.instance.monstruo2Activo, GameManager.instance.monstruo1Tank, scorePerNote);
                    EffectPosition.x=transform.position.x;
                    EffectPosition.y=transform.position.y+0.5f;
                    EffectPosition.z=transform.position.z;
                    Instantiate(hitEffect,EffectPosition,hitEffect.transform.rotation);
                    //Debug.Log("mi numero es: "+numeroAleatorio+" y el porcentaje de normal es: "+percentNormal);
                }
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D other){
        if(other.tag=="Button 2"){
            canBePressed2=true;
        }
        if(other.tag=="Barras 2"){
            thisNote.SetObjectVisibility(true);
        }
        if(other.tag=="Normal"){
            canNormal=true;
            //Debug.Log("Normal");
        }
        if(other.tag=="Good"){
            canGood=true;
            //Debug.Log("Good");
        }
        if(other.tag=="Perfect"){
            canPerfect=true;
            //Debug.Log("Perfect");
        }
    }
    private void OnTriggerExit2D(Collider2D other){
        if(other.tag=="Button 2"){
            canBePressed2=false;
            if(!obtained){
                GameManager.instance.NoteMissedAI();
                EffectPosition.x=transform.position.x;
                EffectPosition.y=transform.position.y+0.5f;
                EffectPosition.z=transform.position.z;
                Instantiate(missEffect,EffectPosition,missEffect.transform.rotation);
            }
            obtained=false;
        }
        if(other.tag=="Barras 2"){
            thisNote.SetObjectVisibility(false);
        }
    }
}