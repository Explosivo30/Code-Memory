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
            Dialgo.SetActive(true);
            hola.GetDialogue(dialogoADar);
            textDialogo.UpdateUI();
            //EventForGame.instance.desactivarDialogo.AddListener(TextoAcabado);
            

        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Dialgo.SetActive(false);
        }
    }

    void TextoAcabado()
    {
        gameObject.SetActive(false);
    }
}
