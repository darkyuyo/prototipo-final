using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{

    [SerializeField] GameObject _hpBar;
    [SerializeField] Text levelText;
    [SerializeField] Image _sprite;
    [SerializeField] Image _icon;
    [SerializeField] int index;
    [SerializeField] bool player;
    [SerializeField] GameObject Escudo;
    [SerializeField] Color _spriteColor,_iconColor,_levelColor;
    [SerializeField] Vector3 posInicial;
    [SerializeField] Quaternion rotInicial;
    public float rotationSpeed = 45f;
    private float currentAngle = 0f;
    private int direction = 1;

    void Start()
    {
        _spriteColor=_sprite.color;
        _iconColor=_icon.color;
        _levelColor=levelText.color;
        posInicial=_sprite.transform.position;
        rotInicial=_sprite.transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        if(player){
            Set();
        }
        else{
            SetAI();
        }
    }
    public void SetHP(float hp,GameObject _hpBar){
        _hpBar.transform.localScale=new Vector3(hp,1,1);
    }  
    public void Set(){
        _sprite.sprite=GameManager.instance.playerParty.getMonstruo(index).Stats.getSprite;
        _icon.sprite=GameManager.instance.playerParty.getMonstruo(index).Stats.getIcon;
        levelText.text="Nvl."+GameManager.instance.playerParty.getMonstruo(index).getLevel;
        SetHP(GameManager.instance.playerParty.getMonstruo(index).percentageVida, _hpBar);
        if(GameManager.instance.playerParty.getMonstruo(index).percentageVida<=0){
            _sprite.color=Color.red;
            _icon.color=Color.red;
            levelText.color=Color.red;
        }
        else{
            _sprite.color=_spriteColor;
            _icon.color=_iconColor;
            levelText.color=_levelColor;
        }
    }
    public void SetAI(){
        _sprite.sprite=GameManager.instance.IAParty.getMonstruo(index).Stats.getSprite;
        _icon.sprite=GameManager.instance.IAParty.getMonstruo(index).Stats.getIcon;
        levelText.text="Lv."+GameManager.instance.IAParty.getMonstruo(index).getLevel;
        SetHP(GameManager.instance.IAParty.getMonstruo(index).percentageVida, _hpBar);
        if(GameManager.instance.IAParty.getMonstruo(index).percentageVida<=0){
            _sprite.color=Color.red;
            _icon.color=Color.red;
            levelText.color=Color.red;
        }
        else{
            _sprite.color=_spriteColor;
            _icon.color=_iconColor;
            levelText.color=_levelColor;
        }
    }
    public void SetEscudo(bool flag){
        Escudo.SetActive(flag);
    }
    public Vector3 getIconPosition(){
        //_iconPosition=_icon.transform.position;
        return this.transform.position;
    }
    public void moverIcono(Vector3 x){
        this.transform.position=x;
    }
    public void rotarSprite(float rotationSpeed){
        currentAngle += rotationSpeed * Time.deltaTime * direction;
        if (currentAngle > 30f )
        {
            direction = -1;
        }
        else if(currentAngle <= -30f){
            direction = 1;
        }
        _sprite.transform.rotation = Quaternion.Euler(0f, 0f, currentAngle);

    }
    public void caidaSprite(float caida){
        _sprite.transform.position+=new Vector3(0,caida,0);   
    }
    public void voltearSprite(){
        PositionInicial();
        _sprite.transform.Rotate(0,180,0);
    }
    public void PositionInicial(){
        _sprite.transform.position=posInicial;
        _sprite.transform.rotation=rotInicial;
        currentAngle = 0f;
    }
}
