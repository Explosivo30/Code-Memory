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
        MovementPlayer playerMovement;

        private void Awake()
        {
            EventForGame.instance.activarDialogo.AddListener(UpdateUI);
        }

        void Start()
        {
            playerConversant = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerConversant>();
            playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<MovementPlayer>();
            nextButton.onClick.AddListener(Next);
            UpdateUI();
            
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Comma))
            {
                if(playerConversant.HasNext())
                {
                    Next();
                }
                else
                {
                    playerMovement.bikeLockControls = false;
                }
                
            }
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

