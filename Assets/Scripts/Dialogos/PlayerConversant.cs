using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Dialogo
{
    public class PlayerConversant : MonoBehaviour
    {
        [SerializeField] DialoguesAssetMenu currentDialogue;
        DialogoNode currentNode = null;

        private void Awake()
        {
            currentNode = currentDialogue.GetRootNode();
        }

        public string GetText()
        {
            if(currentNode == null)
            {
                return "";
            }

           return currentNode.GetDialogo();
        }

        public void GetDialogue(DialoguesAssetMenu currentDialogue)
        {
            this.currentDialogue = currentDialogue;
        }

        public void Next()
        {
            DialogoNode[] children = currentDialogue.GetAllChildren(currentNode).ToArray();
            currentNode = children[0];
        }

        public bool HasNext()
        {
            return currentDialogue.GetAllChildren(currentNode).Count() > 0;
        }
        
    }
}
