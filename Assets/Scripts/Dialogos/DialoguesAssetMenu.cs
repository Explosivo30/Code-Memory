using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Dialogo
{
    [CreateAssetMenu(fileName = "Dialogo", menuName = "Code Memory/Dialogo", order = 0)]
    public class DialoguesAssetMenu : ScriptableObject, ISerializationCallbackReceiver
    {
        [SerializeField]
        List<DialogoNode> nodes = new List<DialogoNode>();
        [SerializeField]
        Vector2 newNodeOffset = new Vector2(200, 0);

        Dictionary<string, DialogoNode> nodeLookup = new Dictionary<string, DialogoNode>();

        private void Awake()
        {
            OnValidate();
        }

        private void OnValidate()
        {
            nodeLookup.Clear();
            foreach(DialogoNode node in GetAllNodes())
            {
                nodeLookup[node.name] = node;
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
            foreach(string childID in parentNode.GetRespuestas())
            {
                foreach (DialogoNode node in GetAllNodes())
                {
                    if (node.name == childID)
                    {
                        if (nodeLookup.ContainsKey(childID))
                        {
                            yield return nodeLookup[childID];
                        }
                    }
                }
                
            }
        }

#if UNITY_EDITOR
        public void CreateNode(DialogoNode parent)
        {
            DialogoNode newNode = MakeNode(parent);
            Undo.RegisterCreatedObjectUndo(newNode, "Created Dialogue Node");
            Undo.RecordObject(this, "Added Dialogue Node");
            AddNode(newNode);
        }

        

        public void DeleteNode(DialogoNode nodeToDelete)
        {
            Undo.RecordObject(this, "Borrado Dialogo");
            nodes.Remove(nodeToDelete);
            OnValidate();
            CleanNodeChildren(nodeToDelete);
            Undo.DestroyObjectImmediate(nodeToDelete);
        }

        private void AddNode(DialogoNode newNode)
        {
            nodes.Add(newNode);
            OnValidate();
        }

        private DialogoNode MakeNode(DialogoNode parent)
        {
            
            DialogoNode newNode = CreateInstance<DialogoNode>();
            newNode.name = Guid.NewGuid().ToString();
            if (parent != null)
            {
                parent.AddRespuesta(newNode.name);
                newNode.SetPlayerSpeaking(!parent.IsPlayerSpeaking());
                newNode.SetPosition(parent.GetRect().position + newNodeOffset);
            }

            return newNode;
        }

        private void CleanNodeChildren(DialogoNode nodeToDelete)
        {
            foreach (DialogoNode node in GetAllNodes())
            {
                node.RemoveRespuesta(nodeToDelete.name);
            }
        }

#endif
        public void OnBeforeSerialize()
        {
#if UNITY_EDITOR
            if (nodes.Count == 0)
            {
                DialogoNode newNode = MakeNode(null);
                AddNode(newNode);
            }


            if (AssetDatabase.GetAssetPath(this) != "")
            {
                foreach(DialogoNode node in GetAllNodes())
                {
                    if(AssetDatabase.GetAssetPath(node) == "")
                    {
                        AssetDatabase.AddObjectToAsset(node, this);
                    }
                }
            }
#endif
        }

        public void OnAfterDeserialize()
        {

        }
    }

}
