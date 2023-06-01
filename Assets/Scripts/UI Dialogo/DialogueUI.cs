using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dialogo;
using TMPro;
using UnityEngine.UI;

namespace UI
{
    public class DialogueUI : MonoBehaviour
    {
        PlayerConversant playerConversant;
        [SerializeField] TextMeshProUGUI AIText;
        [SerializeField] Button nextButton;

        private void Awake()
        {
            EventForGame.instance.activarDialogo.AddListener(UpdateUI);
        }

        void Start()
        {
            playerConversant = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerConversant>();
            nextButton.onClick.AddListener(Next);
            UpdateUI();
            
        }

        void Next()
        {
            playerConversant.Next();
            UpdateUI();
        }

        public void UpdateUI()
        {
            AIText.text = playerConversant.GetText();
            nextButton.gameObject.SetActive(playerConversant.HasNext());
        }
    }
}

