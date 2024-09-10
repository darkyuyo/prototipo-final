using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class NPC : MonoBehaviour, Interactable
{
    [SerializeField] Dialog dialog;
    [SerializeField] Party IAparty, PartyPlayer;
    [SerializeField] GameObject Triangulo;
    public void Interact()
    { 
        DialogManagement.Instance.ShowDialog(dialog);
        if(dialog.Fight){
            GameController.Instance.SetIAParty(IAparty);
        }
        if(dialog.Heal){
            foreach (var monstruo in PartyPlayer.getMonstruos)
            { 
                monstruo.sumarVida(monstruo.VidaMax);
            }
        }
    }
    public void SetTriangulo(bool valor){
        Triangulo.SetActive(valor);
    }
}
