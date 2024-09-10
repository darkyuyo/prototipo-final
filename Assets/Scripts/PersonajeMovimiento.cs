using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Unity.VisualScripting;

public class PersonajeMovimiento : MonoBehaviour
{
    [SerializeField] private float velocidad;
    //public bool EnMovimiento => _direccionMovimiento != Vector2.zero;
    public Vector2 DireccionMovimiento => _direccionMovimiento;
    public LayerMask HierbaLayer;
    public LayerMask InteractableLayer;
    private Rigidbody2D _rb;
    private Vector2 _input;
    public Vector2 _direccionMovimiento;
    public AudioSource BaseMusic;
    //public event Action OnEncountered;
    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        BaseMusic.Play();
    }

    // Update is called once per frame
    public void HandleUpdate()
    {
        _input=new Vector2(x:Input.GetAxisRaw("Horizontal"),y:Input.GetAxisRaw("Vertical"));

        //x

        if(_input.x>0.1)
        {
            _direccionMovimiento.x=1f;
            CheckForEncounters();
        }
        else if(_input.x<0)
        {
            _direccionMovimiento.x=-1f;
            CheckForEncounters();
        }
        else
        {
            _direccionMovimiento.x=0f;
        }

        //y


        if(_input.y>0.1)
        {
            _direccionMovimiento.y=1f;
            CheckForEncounters();
        }
        else if(_input.y<0)
        {
            _direccionMovimiento.y=-1f;
            CheckForEncounters();
        }
        else
        {
            _direccionMovimiento.y=0f;
        }
        if(Input.GetKeyDown(KeyCode.Z))
        {
            Interact();
        }
        if(Input.GetKeyDown(KeyCode.X))
        {
            GameController.Instance.OpenMenu();
        }
    }

    private void FixedUpdate()
    {
        _rb.MovePosition(_rb.position + _direccionMovimiento*velocidad * Time.fixedDeltaTime);
    }
    public void Interact(){
        var collider=Physics2D.OverlapCircle(transform.position,0.5f,InteractableLayer);
        if(collider!=null){
            collider.GetComponent<Interactable>()?.Interact();
        }
    }
    private void CheckForEncounters(){
        if(Physics2D.OverlapCircle(transform.position,0.1f,HierbaLayer)!=null){
            //Debug.Log("Hierba");
            if(UnityEngine.Random.Range(1,1001)<=1f){
                GameController.Instance.RandomBattle();
            }
        }
    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Interactable")){
            other.GetComponent<NPC>()?.SetTriangulo(true);
        }
    }
    public void OnTriggerExit2D(Collider2D other)
    {
        if(other.CompareTag("Interactable")){
            other.GetComponent<NPC>()?.SetTriangulo(false);
        }
    }
}
