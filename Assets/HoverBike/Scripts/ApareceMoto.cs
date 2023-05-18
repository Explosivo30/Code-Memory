using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApareceMoto : MonoBehaviour
{
    GameObject HoverBike;
    [SerializeField] Vector3 PositionASpawnear;
    private float rotacionobjectHoverBike;
    [SerializeField] GameObject Disco;

    // Start is called before the first frame update
    void Start()
    {
        HoverBike = GameObject.FindGameObjectWithTag("HoverBike");
    }
    private void OnCollisionEnter(Collision collision)
    {
        Invoke("OnGounded", 2f);
    }
    void OnGounded()
    {
        PositionASpawnear = Disco.transform.position;
        rotacionobjectHoverBike = Disco.transform.eulerAngles.y;
        HoverBike.transform.position = new Vector3(PositionASpawnear.x, PositionASpawnear.y + 2f, PositionASpawnear.z);
        HoverBike.transform.rotation = Quaternion.AngleAxis(rotacionobjectHoverBike, Vector3.up);
        Invoke("EliminarDisco", 1f);
    }
    void EliminarDisco()
    {
        Destroy(Disco);
    }
}
