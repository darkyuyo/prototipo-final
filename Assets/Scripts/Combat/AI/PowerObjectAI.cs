using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerObjectAI : MonoBehaviour
{
    public bool canBePressed2,canNormal, canGood, canPerfect;
    private bool obtained=false;
    private PowerObjectAI thisPower;
    public GameObject hitEffect,goodEffect,perfectEffect,missEffect;
    private float scorePerNote=1F,scorePerGoodNote=1.25F,scorePerPerfectNote=1.50F;
    private Renderer objectRenderer;
    public int numeroAleatorio,percentNormal=80,percentGood=30,percentPerfect=10,percentMiss=100;
    [SerializeField] UnityEngine.Vector3 EffectPosition;
    void Start()
    {
        objectRenderer = GetComponent<Renderer>();
        thisPower=this;
        thisPower.SetObjectVisibility(false);
        numeroAleatorio = Random.Range(0,100);
    }
    public void SetObjectVisibility(bool isVisible)
    {
        objectRenderer.enabled = isVisible;
    }
    void Update(){
        int mejor_ability=0;
        int cont=0;
        if(canBePressed2){
            foreach (var ability in GameManager.instance.monstruo2Activo._abilities)
            {
                if(ability._ability.getValor<=GameManager.instance.currentMultiplierAI){
                    mejor_ability=cont;
                    break;
                }
                cont++;
            }
            cont=0;
            if(GameManager.instance.monstruo2Tank.percentageVida<=0.3){
                ////Debug.Log("Priorizar cura");
                foreach (var ability in GameManager.instance.monstruo2Activo._abilities)
                {
                    if(ability._ability.getCategoria==AbilityBase.Categoria.Boost && ability._ability.getValor<=GameManager.instance.currentMultiplierAI){
                        mejor_ability=cont;
                        break;
                    }
                    cont++;
                }
                cont=0;
            }
            else if(GameManager.instance.monstruo2Tank.percentageVida>=0.7){
                ////Debug.Log("Priorizar daño y boost");
                foreach (var ability in GameManager.instance.monstruo2Activo._abilities)
                {
                    if(ability._ability.getCategoria==AbilityBase.Categoria.Dano || ability._ability.getCategoria==AbilityBase.Categoria.Ataque_repetitivo && ability._ability.getValor<=GameManager.instance.currentMultiplierAI){
                        mejor_ability=cont;
                        break;
                    }
                    cont++;
                }
            cont=0;
            }
            else if(GameManager.instance.monstruo1Tank.percentageVida<=0.5){
                ////Debug.Log("Priorizar daño y boost");
                foreach (var ability in GameManager.instance.monstruo2Activo._abilities)
                {
                    if(ability._ability.getCategoria==AbilityBase.Categoria.Dano || ability._ability.getCategoria==AbilityBase.Categoria.Ataque_repetitivo && ability._ability.getValor<=GameManager.instance.currentMultiplierAI){
                        mejor_ability=cont;
                        break;
                    }
                    cont++;
                }
            }
            if(GameManager.instance.monstruo2Activo==GameManager.instance.IAParty.getMonstruo(1)){
                if(percentPerfect>=numeroAleatorio && numeroAleatorio>0 && canPerfect){
                    obtained=true;
                    gameObject.SetActive(false);
                    GameManager.instance.HitMagicAI(GameManager.instance.monstruo2Activo, GameManager.instance.monstruo1Tank, scorePerPerfectNote);
                    EffectPosition.x=transform.position.x;
                    EffectPosition.y=transform.position.y+0.5f;
                    EffectPosition.z=transform.position.z;
                    Instantiate(perfectEffect,EffectPosition,perfectEffect.transform.rotation);
                    if(GameManager.instance.monstruo1Tank!=null){
                        //Debug.Log("mejor ability es: "+mejor_ability+", y se llama: "+GameManager.instance.monstruo2Activo._abilities[mejor_ability]._ability.getName);
                        GameManager.instance.monstruo2Activo._abilities[mejor_ability]._ability.Activate(false);
                        GameManager.instance.Habilidad2(GameManager.instance.monstruo2Activo.Stats.Name,GameManager.instance.monstruo2Activo._abilities[mejor_ability]._ability.getName);
                        GameManager.instance.RotarActivoIA();                    
                    }  
                }
                else if(percentGood>=numeroAleatorio && numeroAleatorio>percentPerfect && canGood){
                    obtained=true;
                    gameObject.SetActive(false);
                    GameManager.instance.HitMagicAI(GameManager.instance.monstruo2Activo, GameManager.instance.monstruo1Tank, scorePerGoodNote);
                    EffectPosition.x=transform.position.x;
                    EffectPosition.y=transform.position.y+0.5f;
                    EffectPosition.z=transform.position.z;
                    Instantiate(goodEffect,EffectPosition,goodEffect.transform.rotation);
                    if(GameManager.instance.monstruo1Tank!=null){
                        ////Debug.Log("mejor ability es: "+mejor_ability+", y se llama: "+GameManager.instance.monstruo2Activo._abilities[mejor_ability]._ability.getName);
                        GameManager.instance.monstruo2Activo._abilities[mejor_ability]._ability.Activate(false);
                        GameManager.instance.Habilidad2(GameManager.instance.monstruo2Activo.Stats.Name,GameManager.instance.monstruo2Activo._abilities[mejor_ability]._ability.getName);
                        GameManager.instance.RotarActivoIA();                    
                    }  
                }
                else if(percentNormal>=numeroAleatorio && numeroAleatorio>percentGood && canNormal){
                    obtained=true;
                    gameObject.SetActive(false);
                    GameManager.instance.HitMagicAI(GameManager.instance.monstruo2Activo, GameManager.instance.monstruo1Tank, scorePerNote);
                    EffectPosition.x=transform.position.x;
                    EffectPosition.y=transform.position.y+0.5f;
                    EffectPosition.z=transform.position.z;
                    Instantiate(hitEffect,EffectPosition,hitEffect.transform.rotation);
                    if(GameManager.instance.monstruo1Tank!=null){
                        //Debug.Log("mejor ability es: "+mejor_ability+", y se llama: "+GameManager.instance.monstruo2Activo._abilities[mejor_ability]._ability.getName);
                        GameManager.instance.monstruo2Activo._abilities[mejor_ability]._ability.Activate(false);
                        GameManager.instance.Habilidad2(GameManager.instance.monstruo2Activo.Stats.Name,GameManager.instance.monstruo2Activo._abilities[mejor_ability]._ability.getName);
                        GameManager.instance.RotarActivoIA();                    
                    }  
                }
            }
            else{
                if(percentPerfect>=numeroAleatorio && numeroAleatorio>0 && canPerfect){
                    obtained=true;
                    gameObject.SetActive(false);
                    GameManager.instance.HitFisicAI(GameManager.instance.monstruo2Activo, GameManager.instance.monstruo1Tank, scorePerPerfectNote);
                    Instantiate(perfectEffect,EffectPosition,perfectEffect.transform.rotation);
                    if(GameManager.instance.monstruo1Tank!=null){
                        //Debug.Log("mejor ability es: "+mejor_ability+", y se llama: "+GameManager.instance.monstruo2Activo._abilities[mejor_ability]._ability.getName);
                        GameManager.instance.monstruo2Activo._abilities[mejor_ability]._ability.Activate(false);
                        GameManager.instance.Habilidad2(GameManager.instance.monstruo2Activo.Stats.Name,GameManager.instance.monstruo2Activo._abilities[mejor_ability]._ability.getName);
                        GameManager.instance.RotarActivoIA();                    
                    }  
                }
                else if(percentGood>=numeroAleatorio && numeroAleatorio>percentPerfect && canGood){
                    obtained=true;
                    gameObject.SetActive(false);
                    GameManager.instance.HitFisicAI(GameManager.instance.monstruo2Activo, GameManager.instance.monstruo1Tank, scorePerGoodNote);
                    EffectPosition.x=transform.position.x;
                    EffectPosition.y=transform.position.y+0.5f;
                    EffectPosition.z=transform.position.z;
                    Instantiate(goodEffect,EffectPosition,goodEffect.transform.rotation);
                    if(GameManager.instance.monstruo1Tank!=null){
                        //Debug.Log("mejor ability es: "+mejor_ability+", y se llama: "+GameManager.instance.monstruo2Activo._abilities[mejor_ability]._ability.getName);
                        GameManager.instance.monstruo2Activo._abilities[mejor_ability]._ability.Activate(false);
                        GameManager.instance.Habilidad2(GameManager.instance.monstruo2Activo.Stats.Name,GameManager.instance.monstruo2Activo._abilities[mejor_ability]._ability.getName);
                        GameManager.instance.RotarActivoIA();                    
                    }  
                }
                else if(percentNormal>=numeroAleatorio && numeroAleatorio>percentGood && canNormal){
                    obtained=true;
                    gameObject.SetActive(false);
                    GameManager.instance.HitFisicAI(GameManager.instance.monstruo2Activo, GameManager.instance.monstruo1Tank, scorePerNote);
                    EffectPosition.x=transform.position.x;
                    EffectPosition.y=transform.position.y+0.5f;
                    EffectPosition.z=transform.position.z;
                    Instantiate(hitEffect,EffectPosition,hitEffect.transform.rotation);
                    if(GameManager.instance.monstruo1Tank!=null){
                        //Debug.Log("mejor ability es: "+mejor_ability+", y se llama: "+GameManager.instance.monstruo2Activo._abilities[mejor_ability]._ability.getName);
                        GameManager.instance.monstruo2Activo._abilities[mejor_ability]._ability.Activate(false);
                        GameManager.instance.Habilidad2(GameManager.instance.monstruo2Activo.Stats.Name,GameManager.instance.monstruo2Activo._abilities[mejor_ability]._ability.getName);
                        GameManager.instance.RotarActivoIA();                    
                    }  
                }
            }
            /*if(GameManager.instance.monstruo1Tank!=null){
                //Debug.Log("mejor ability es: "+mejor_ability+", y se llama: "+GameManager.instance.monstruo2Activo._abilities[mejor_ability]._ability.getName);
                GameManager.instance.monstruo2Activo._abilities[mejor_ability]._ability.Activate(false);
                GameManager.instance.Habilidad2(GameManager.instance.monstruo2Activo.Stats.Name,GameManager.instance.monstruo2Activo._abilities[mejor_ability]._ability.getName);
                //GameManager.instance.RotarActivoIA();                    
            }*/
        }
    }
    private void OnTriggerEnter2D(Collider2D other){
        if(other.tag=="Button 2"){
            canBePressed2=true;
        }
        if(other.tag=="Barras 2"){
            thisPower.SetObjectVisibility(true);
        }
        if(other.tag=="Normal"){
            canNormal=true;
            ////Debug.Log("Normal");
        }
        if(other.tag=="Good"){
            canGood=true;
            ////Debug.Log("Good");
        }
        if(other.tag=="Perfect"){
            canPerfect=true;
            ////Debug.Log("Perfect");
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
            thisPower.SetObjectVisibility(false);
        }
    }
}