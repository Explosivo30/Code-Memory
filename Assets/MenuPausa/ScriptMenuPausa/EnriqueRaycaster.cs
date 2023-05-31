using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class EnriqueRaycaster : MonoBehaviour
{
    private void Update()
    {   
        Vector3 mousePosition = Mouse.current.position.ReadValue();
        Ray ray = Camera.main.ScreenPointToRay(mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit)) { Debug.Log(hit.collider.name); }

        RaycastHit2D hit2D = Physics2D.Raycast(mousePosition, Vector3.forward);
        if (hit2D.collider) { Debug.Log(hit2D.collider.name); }
    }
}
