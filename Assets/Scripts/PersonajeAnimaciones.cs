using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonajeAnimaciones : MonoBehaviour
{
    private Animator _animator;
    private PersonajeMovimiento _personajeMovimiento;
    // Start is called before the first frame update
    private void Awake(){
        _animator = GetComponent<Animator>();
        _personajeMovimiento = GetComponent<PersonajeMovimiento>();
    }

    // Update is called once per frame
    void Update()
    {

       _animator.SetFloat("X",_personajeMovimiento.DireccionMovimiento.x);
       _animator.SetFloat("Y",_personajeMovimiento.DireccionMovimiento.y); 
    }
}
