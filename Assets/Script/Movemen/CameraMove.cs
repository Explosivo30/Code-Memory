using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField]float sensitivityX = 5f;
    [SerializeField]float sensitivityY = 5f;
    public float rotateZ = 0f;
    [SerializeField] Transform playerBody;

    float posCameraX;
    float posCameraY;

    public float xRotation = 0f;


    void Start()
    {
        posCameraX = transform.position.x;
        posCameraY = transform.position.y;

        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {



        float rotationX = Input.GetAxis("Mouse X") * sensitivityX * Time.deltaTime;

       float rotationY = Input.GetAxis("Mouse Y") * sensitivityY * Time.deltaTime;

        xRotation -= rotationY;

        xRotation = Mathf.Clamp(xRotation, -60, 90);

        //TODO: only let it rotate  if you are not wall running with a boolean

        //if () { }

        transform.localRotation = Quaternion.Euler(xRotation, 0f, rotateZ);

        playerBody.Rotate(Vector3.up * rotationX);

        //END TODO HERE

    }
}
