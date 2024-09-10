using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnknownNotesAI : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject prefabPoder,prefabNota,objetoPadre;
    private Renderer objectRenderer;
    private UnknownNotesAI thisUnknown;
    public int valor;

    void Start()
    {  
        objectRenderer = GetComponent<Renderer>();
        thisUnknown=this;
        this.SetObjectVisibility(false);
    }

    public void SetObjectVisibility(bool isVisible)
    {
        objectRenderer.enabled = isVisible;
    }
    private void OnTriggerEnter2D(Collider2D other){
    if(other.tag=="Barras 2"){
        if(GameManager.instance.ObtenerMultiplierAI()>=valor){
            Instantiate(prefabPoder,transform.position, prefabPoder.transform.rotation, objetoPadre.transform);
        }
        else{
            Instantiate(prefabNota,transform.position, prefabNota.transform.rotation, objetoPadre.transform);
        }
        Destroy(this.gameObject);
        }
    }
}