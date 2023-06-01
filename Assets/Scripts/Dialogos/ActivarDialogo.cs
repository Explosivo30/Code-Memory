using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dialogo;
using UI;

public class ActivarDialogo : MonoBehaviour
{
    [SerializeField] MovementPlayer movementPlayer;
    [SerializeField] DialoguesAssetMenu dialogoADar;
    [SerializeField] GameObject Dialgo;
    [SerializeField] DialogueUI textDialogo;
    public bool textoYaAcabado = false;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerConversant hola = other.GetComponent<PlayerConversant>();
            hola.GetDialogue(dialogoADar);

            
            movementPlayer.bikeLockControls = true;
            Dialgo.SetActive(true);
            EventForGame.instance.activarDialogo.Invoke();
            

        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Dialgo.SetActive(false);
        }
    }
    private void Update()
    {
        if (textoYaAcabado == true)
        {
            movementPlayer.bikeLockControls = false;
        }
    }
}
