using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMoveHoverBike : MonoBehaviour
{
    [SerializeField] float sensitivityX = 200f;
    [SerializeField] float sensitivityY = 200f;
    float xRotation = 0f;
    float yRotation = 0f;

    [SerializeField] float clampDerecha = -20f;
    [SerializeField] float clampIzquierda = 40f;
    [SerializeField] float clampAbajo = -20f;
    [SerializeField] float clampArriba = 40f;

    // Update is called once per frame
    void Update()
    {
        float rotationX = Input.GetAxis("Mouse X") * sensitivityX * Time.deltaTime;

        float rotationY = Input.GetAxis("Mouse Y") * sensitivityY * Time.deltaTime;

        xRotation -= rotationY;
        yRotation += rotationX;
        xRotation = Mathf.Clamp(xRotation, clampAbajo, clampArriba);
        yRotation = Mathf.Clamp(yRotation, clampDerecha, clampIzquierda);

        transform.localRotation = Quaternion.Euler(xRotation, yRotation,0f);
    }
}
