using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogManagement : MonoBehaviour
{
    [SerializeField] GameObject dialogBox;
    [SerializeField] Text dialogText,nombreText;
    public event Action OnShowDialog;
    public event Action OnCloseDialog;
    public event Action OnEncountered;
    Dialog dialog;
    int currentline = 0;
    public static DialogManagement Instance { get; private set; }
    private void Awake()
    {
        Instance = this;
    }

    public void ShowDialog(Dialog dialog)
    {
        OnShowDialog?.Invoke();
        this.dialog = dialog;
        dialogBox.SetActive(true);
        cambiarDialogo(currentline);
    }
    public void Fight(){
        GameController.Instance.SetValor(dialog.BattleNumber);
        OnEncountered();
    }
    public void HandleUpdate(){
        if (Input.GetKeyDown(KeyCode.Z)){
            ++currentline;
            if(dialog.MoreDialog()){
                if(dialog.ContPar()){
                    if(currentline<dialog.Lines.Count){
                        cambiarDialogo(currentline);
                    }
                    else{
                        currentline=0;
                        dialogBox.SetActive(false);
                        OnCloseDialog?.Invoke();
                        if(dialog.Fight){
                            Fight();
                        }
                        dialog.sumarCont();   
                    }
                }
                else{
                    if(currentline<dialog.Lines2.Count){
                        cambiarDialogo(currentline);
                    }
                    else{
                        currentline=0;
                        dialogBox.SetActive(false);
                        OnCloseDialog?.Invoke();
                        dialog.sumarCont();
                        dialog.desaparecerNPC();
                    }
                }
            }
            else if(currentline<dialog.Lines.Count){
                cambiarDialogo(currentline);
            }
            else{
                currentline=0;
                dialogBox.SetActive(false);
                OnCloseDialog?.Invoke();
                if(dialog.Fight){
                    Fight();
                }
            }
        }
    }
    public void cambiarDialogo(int numero){
        if(dialog.MoreDialog()==false){
            dialogText.text = dialog.Lines[numero];
            nombreText.text = dialog.getLineaNombre(numero);
        }
        else{
            if(dialog.ContPar()){
                dialogText.text = dialog.Lines[numero];
                nombreText.text = dialog.getLineaNombre(numero);
            }
            else{
                dialogText.text = dialog.Lines2[numero];
                nombreText.text = dialog.getLineaNombre2(numero);
            }
        }
    }
}
