using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dialogo
{
    [CreateAssetMenu(fileName = "Dialogo", menuName = "Code Memory/Dialogo", order = 0)]
    public class DialoguesAssetMenu : ScriptableObject
    {
        [SerializeField]
        List<DialogoNode> nodes = new List<DialogoNode>();
        Dictionary<string, DialogoNode> nodeLookup = new Dictionary<string, DialogoNode>();
        
         
#if UNITY_EDITOR
        private void Awake()
        {
            if(nodes.Count == 0)
            {
                DialogoNode rootNode = new DialogoNode();
                rootNode.uniqueID = Guid.NewGuid().ToString();
                nodes.Add(rootNode);
            }
            OnValidate();
        }

#endif

        private void OnValidate()
        {
            nodeLookup.Clear();
            foreach(DialogoNode node in GetAllNodes())
            {
                nodeLookup[node.uniqueID] = node;
            }
        }

        public IEnumerable<DialogoNode> GetAllNodes()
        {
            return nodes;
        }


        public DialogoNode GetRootNode()
        {
            return nodes[0];
        }

        public IEnumerable<DialogoNode> GetAllChildren(DialogoNode parentNode)
        {
            foreach(string childID in parentNode.respuestas)
            {
                foreach (DialogoNode node in GetAllNodes())
                {
                    if (node.uniqueID == childID)
                    {
                        if (nodeLookup.ContainsKey(childID))
                        {
                            yield return nodeLookup[childID];
                        }
                    }
                }
                
            }
        }

        public void CreateNode(DialogoNode parent)
        {
            DialogoNode newNode = new DialogoNode();
            newNode.uniqueID = Guid.NewGuid().ToString();
            parent.respuestas.Add(newNode.uniqueID);
            nodes.Add(newNode);
            OnValidate();
        }

        public void DeleteNode(DialogoNode nodeToDelete)
        {
            nodes.Remove(nodeToDelete);
            OnValidate();
            CleanNodeChildren(nodeToDelete);
        }

        private void CleanNodeChildren(DialogoNode nodeToDelete)
        {
            foreach (DialogoNode node in GetAllNodes())
            {
                node.respuestas.Remove(nodeToDelete.uniqueID);
            }
        }



    }

}
