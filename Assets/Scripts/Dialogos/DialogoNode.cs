using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Dialogo
{  
    public class DialogoNode : ScriptableObject
    {
        [SerializeField]
        bool isPlayerSpeaking = false;
        [SerializeField]
        string dialogo;
        [SerializeField]
        List<string> respuestas = new List<string>();
        [SerializeField]
        Rect rect = new Rect(0,0,200, 100);


        public Rect GetRect()
        {
            return rect;
        }
        public string GetDialogo()
        {
            return dialogo;
        }

        public List<string> GetRespuestas()
        {
            return respuestas;
        }

        public bool IsPlayerSpeaking()
        {
            return isPlayerSpeaking;
        }

#if UNITY_EDITOR

        public void SetPosition(Vector2 newPosition)
        {
            Undo.RecordObject(this, "Move Dialogue Node");
            rect.position = newPosition;
            EditorUtility.SetDirty(this);
        }

        public void SetDialogo(string newDialogo)
        {

            if (newDialogo != dialogo)
            {
                Undo.RecordObject(this, "Update Dialogue Text");
                dialogo = newDialogo;
                EditorUtility.SetDirty(this);
            }

        }

        public void AddRespuesta(string childID)
        {
            Undo.RecordObject(this, "Add Dialogue Link");
            respuestas.Add(childID);
            EditorUtility.SetDirty(this);
        }

        public void RemoveRespuesta(string childID)
        {
            Undo.RecordObject(this, "Remove Dialogue Link");
            respuestas.Remove(childID);
            EditorUtility.SetDirty(this);
        }

        public void SetPlayerSpeaking(bool newIsPlayerSpeaking)
        {
            Undo.RecordObject(this, "Change Dialog Speaker");
            isPlayerSpeaking = newIsPlayerSpeaking;
            EditorUtility.SetDirty(this);
        }




#endif




    }
}
