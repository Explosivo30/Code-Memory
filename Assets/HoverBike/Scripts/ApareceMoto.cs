using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApareceMoto : MonoBehaviour
{
    public GameObject HoverBike;
    [SerializeField] Vector3 PositionASpawnear;
    private float rotacionHoverBike;
    [SerializeField] GameObject Disco;

    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        Invoke("OnGounded", 2f);
    }
    void OnGounded()
    {
        PositionASpawnear = Disco.transform.position;
        rotacionHoverBike = Disco.transform.eulerAngles.y;
        HoverBike.transform.position = new Vector3(PositionASpawnear.x, PositionASpawnear.y + 2f, PositionASpawnear.z);
        HoverBike.transform.rotation = Quaternion.AngleAxis(rotacionHoverBike, Vector3.up);
    }
}
