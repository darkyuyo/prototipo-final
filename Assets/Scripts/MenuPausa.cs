using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class MenuPausa : MonoBehaviour
{
    [SerializeField] Slider slider;
    [SerializeField] GameObject MenuPrincipal, OpcionesMenu, MenuLils,MenuCambiar,MenuStats;
    [SerializeField] List<GameObject> _hpBar;
    [SerializeField] List<Text> levelText, nombreText, vidaText, nombresreserva;
    [SerializeField] List<Image> _icon;
    [SerializeField] List<Image> _spritesreserva;
    [SerializeField] Party playerParty, reservaParty;
    [SerializeField] int indexparty,indexreserva;
    [SerializeField] Image SpriteStats;
    [SerializeField] Text Nombre, Tipo, Nivel, Vida, Ataque, Defensa, AtaqueMagico, DefensaMagica;
    [SerializeField] List<Text> NombreHabilidad, DescripcionHabilidad, MultiHabilidad,TipoHabilidad,ValorHabilidad;

    void Start()
    {
        slider.value=PlayerPrefs.GetFloat("Volumen",1);
        AudioListener.volume=slider.value;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Iniciar(){
        MenuPrincipal.SetActive(true);
    }

    public void Lils(){
        MenuPrincipal.SetActive(false);
        Set();
        MenuLils.SetActive(true);
    }
    public void Opciones(){
        MenuPrincipal.SetActive(false);
        OpcionesMenu.SetActive(true);
    }
    public void Volver(){
        MenuPrincipal.SetActive(true);
        OpcionesMenu.SetActive(false);
        MenuLils.SetActive(false);
        MenuCambiar.SetActive(false);
        MenuStats.SetActive(false);
    }
    public void SalirdelJuego(){
        Application.Quit();
    }
    public void CambiarVolumen(float valor){
        AudioListener.volume=slider.value;
        PlayerPrefs.SetFloat("Volumen",slider.value);
    }
    public void SetHP(float hp,GameObject _hpBar){
        _hpBar.transform.localScale=new Vector3(hp,1,1);
    }
    public void Set(){
        for(int i=0;i<3;i++){
            _icon[i].sprite=playerParty.getMonstruo(i).Stats.getIcon;
            levelText[i].text="Nvl."+playerParty.getMonstruo(i).getLevel.ToString();
            nombreText[i].text=playerParty.getMonstruo(i).Stats.Name;
            SetHP(playerParty.getMonstruo(i).percentageVida, _hpBar[i]);
            vidaText[i].text=playerParty.getMonstruo(i).VidaActual.ToString()+"/"+playerParty.getMonstruo(i).VidaMax.ToString();
            
        }
    }  
    public void CerrarMenus(){
        MenuPrincipal.SetActive(false);
        OpcionesMenu.SetActive(false);
        MenuLils.SetActive(false);
        MenuCambiar.SetActive(false);
        MenuStats.SetActive(false);
    }
    public void SetReserva(){
        for(int i=0;i<7;i++){
            _spritesreserva[i].sprite=reservaParty.getMonstruo(i).Stats.getIcon;
            nombresreserva[i].text=reservaParty.getMonstruo(i).Stats.Name;
        }
    }
    public void BotonCambiar(){
        MenuLils.SetActive(false);
        SetReserva();
        MenuCambiar.SetActive(true);
    }
    public void Cambiar(){
        Monstruo temp=playerParty.getMonstruo(indexparty);
        playerParty.setMonstruo(indexparty,reservaParty.getMonstruo(indexreserva));
        reservaParty.setMonstruo(indexreserva,temp);
        Set();
        SetReserva();
        MenuCambiar.SetActive(false);
        MenuLils.SetActive(true);
    }
    public void SetIndexParty(int index){
        indexparty=index;
    }
    public void SetIndexReserva(int index){
        indexreserva=index;
    }
    public void BotonStats(){
        MenuLils.SetActive(false);
        MenuStats.SetActive(true);
        SetStats();
    }
    public void SetStats(){
        SpriteStats.sprite=playerParty.getMonstruo(indexparty).Stats.getSprite;
        Nombre.text="Nombre: "+playerParty.getMonstruo(indexparty).Stats.Name;
        Tipo.text="Tipo:"+playerParty.getMonstruo(indexparty).Stats.getTipo1.ToString()+"/"+playerParty.getMonstruo(indexparty).Stats.getTipo2.ToString();
        Nivel.text="Nivel: "+playerParty.getMonstruo(indexparty).getLevel.ToString();
        Vida.text="Vida: "+playerParty.getMonstruo(indexparty).VidaActual.ToString()+"/"+playerParty.getMonstruo(indexparty).VidaMax.ToString();
        Ataque.text="Ataque: "+playerParty.getMonstruo(indexparty).Attack.ToString();
        Defensa.text="Defensa: "+playerParty.getMonstruo(indexparty).Defense.ToString();
        AtaqueMagico.text="Ataque Magico: "+playerParty.getMonstruo(indexparty).MagicAttack.ToString();
        DefensaMagica.text="Defensa Magica: "+playerParty.getMonstruo(indexparty).MagicDefense.ToString();
        SetHabilidades();
    }
    public void SetHabilidades(){
        for(int i=0;i<4;i++){
            if(i<playerParty.getMonstruo(indexparty)._abilities.Count){
                NombreHabilidad[i].text=playerParty.getMonstruo(indexparty)._abilities[i]._ability.getName;
                DescripcionHabilidad[i].text=playerParty.getMonstruo(indexparty)._abilities[i]._ability.getDescription;
                MultiHabilidad[i].text=playerParty.getMonstruo(indexparty)._abilities[i]._ability.getValor.ToString();
                TipoHabilidad[i].text=playerParty.getMonstruo(indexparty)._abilities[i]._ability.getTipo.ToString();
                ValorHabilidad[i].text=playerParty.getMonstruo(indexparty)._abilities[i]._ability.getPoder.ToString();
            }
            else{
                NombreHabilidad[i].text="";
                DescripcionHabilidad[i].text="";
                MultiHabilidad[i].text="";
                TipoHabilidad[i].text="";
                ValorHabilidad[i].text="";
            }
        }

    }
}
