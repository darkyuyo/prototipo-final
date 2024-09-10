using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatScroller : MonoBehaviour
{
    [SerializeField] float beatTempo;
    public bool hasStarted;
    [SerializeField] GameObject Scroller;
    [SerializeField] Vector3 ScrollerInitialPosition;
    void Start()
    {
        beatTempo=beatTempo/60f;
        ScrollerInitialPosition=Scroller.transform.position;
    }
    void Update()
    {
        if(hasStarted){
            transform.position -= new Vector3(beatTempo*Time.deltaTime,0f,0f);          
        }
    }
    public void ScrollerPositionStart(){
        Scroller.transform.position=ScrollerInitialPosition;
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(true);
            //child.gameObject.GetComponent<NoteObject>().SetObjectVisibility(false);
        }
    }
}
