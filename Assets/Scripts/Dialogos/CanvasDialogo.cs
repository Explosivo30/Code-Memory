using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CanvasDialogo : MonoBehaviour
{
    [SerializeField] Dialogo.DialoguesAssetMenu dialogoAika;
    [SerializeField] TMP_Text textDialogo;
    string prueba;

    private void Start()
    {
        textDialogo.text = dialogoAika.GetRootNode().dialogo;
    }
}
