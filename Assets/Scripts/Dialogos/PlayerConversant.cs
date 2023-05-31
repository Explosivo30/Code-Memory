using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dialogo
{
    public class PlayerConversant : MonoBehaviour
    {
        [SerializeField] DialoguesAssetMenu currentDialogue;

       

        public string GetText()
        {
            if(currentDialogue == null)
            {
                return "";
            }

           return currentDialogue.GetRootNode().GetDialogo();
        }


        public void GetDialogue(DialoguesAssetMenu currentDialogue)
        {
            this.currentDialogue = currentDialogue;
        }

        
    }
}
