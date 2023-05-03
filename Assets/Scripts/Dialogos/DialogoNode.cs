using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dialogo
{
    [System.Serializable]
    public class DialogoNode
    {
        public string uniqueID;
        public string dialogo;
        public List<string> respuestas = new List<string>();
        public Rect rect = new Rect(0,0,200, 100);
    }
}
