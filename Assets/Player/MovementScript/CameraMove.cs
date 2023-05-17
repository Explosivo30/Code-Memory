using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    // Start is called before the first frame update

    public float sensitivityX = 5f;
    public float sensitivityY = 5f;
    public float rotateZ = 0f;
    [SerializeField] Transform playerBody;
    public bool isWallRunning = false;
    float posCameraX;
    float posCameraY;

    public float xRotation = 0f;
    public bool desactivarMovimiento = false;


    void Start()
    {
        posCameraX = transform.position.x;
        posCameraY = transform.position.y;

        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        if (desactivarMovimiento == false)
        {
            float rotationX = Input.GetAxis("Mouse X") * sensitivityX * Time.deltaTime;

            float rotationY = Input.GetAxis("Mouse Y") * sensitivityY * Time.deltaTime;

            xRotation -= rotationY;

            xRotation = Mathf.Clamp(xRotation, -85, 60);

            transform.localRotation = Quaternion.Euler(xRotation, 0f, rotateZ);
            if (isWallRunning == false)
            {
                playerBody.Rotate(Vector3.up * rotationX);
            }
        }


    }


    public void SetIsWallRunning(bool isWallRunning)
    {
        this.isWallRunning = isWallRunning;
    }


    public bool GetIsWallRunning()
    {
        return isWallRunning;
    }


    public void SetDesactivarMovimiento()
    {
        desactivarMovimiento = !desactivarMovimiento;
    }

}
