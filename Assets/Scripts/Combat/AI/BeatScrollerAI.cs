using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatScrollerAI : MonoBehaviour
{
    [SerializeField] float beatTempo;
    [SerializeField] GameObject Scroller;
    [SerializeField] Vector3 ScrollerInitialPosition;
    public bool hasStarted;
    void Start()
    {
        beatTempo=beatTempo/60f;
        ScrollerInitialPosition=Scroller.transform.position;
    }
    public void ScrollerPositionStart(){
        Scroller.transform.position=ScrollerInitialPosition;
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(true);
            //child.gameObject.objectRenderer.enabled = false;
        }
    }

    void Update()
    {
        if(hasStarted){
            transform.position += new Vector3(beatTempo*Time.deltaTime,0f,0f);   
                   
        }
    }

}
