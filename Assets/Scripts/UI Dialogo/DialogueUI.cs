using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dialogo;
using TMPro;

namespace UI
{
    public class DialogueUI : MonoBehaviour
    {
        PlayerConversant playerConversant;
        [SerializeField] TextMeshProUGUI AIText;
        // Start is called before the first frame update


        private void Awake()
        {
            EventForGame.instance.activarDialogo.AddListener(ShowDialog);
        }

        void Start()
        {
            playerConversant = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerConversant>();
            AIText.text = playerConversant.GetText();
        }


        void ShowDialog()
        {
            AIText.text = playerConversant.GetText();
        }


    }
}

