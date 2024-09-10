using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Timer : MonoBehaviour
{
    public Slider slider;
    public TextMeshProUGUI text;
    public float gameTime,time;
    public bool stopTimer,hasStarted;
    [SerializeField] PanelInicio panelInicio;
    void Start()
    {
        stopTimer = false;
        time=gameTime;
        slider.maxValue = time;
        slider.value = time;
    }
    public void InitTimer(){
        gameTime=time;
        //Debug.Log("pasa aca 1");
    } 
    void Update()
    {
        if(hasStarted){
            gameTime -=Time.deltaTime;
            int minutes = Mathf.FloorToInt(gameTime / 60f);
            int seconds = Mathf.FloorToInt(gameTime - minutes * 60);
            string niceTime = string.Format("{0:0}:{1:00}", minutes, seconds);
            if (gameTime > 0)
            {
                slider.value =gameTime;
                text.text = niceTime;
            }
            else if(!stopTimer)
            {
                stopTimer = true;
                //Debug.Log("pasa aca 2");
                panelInicio.TimerPerdida();
                
            }
        }
    }
}   