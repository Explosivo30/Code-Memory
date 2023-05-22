using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ApareceMoto : MonoBehaviour
{
    GameObject HoverBike;
    [SerializeField] Vector3 PositionASpawnear;
    private float rotacionobjectHoverBike;
    [SerializeField] GameObject Disco;
    [SerializeField] AudioSource SpawnAudio;


    // Start is called before the first frame update
    void Start()
    {
        HoverBike = GameObject.FindGameObjectWithTag("HoverBike");

    }
    private void OnCollisionEnter(Collision collision)
    {
        Invoke("OnGounded", 2f);
        HoverBike.transform.DOScale(new Vector3(0, 0, 0), 1);


    }
    void OnGounded()
    {
        HoverBike.transform.DOScale(new Vector3(1.362311f, 1.362311f, 1.362311f), 1);
        PositionASpawnear = Disco.transform.position;
        rotacionobjectHoverBike = Disco.transform.eulerAngles.y;
        HoverBike.transform.position = new Vector3(PositionASpawnear.x, PositionASpawnear.y + 2f, PositionASpawnear.z);
        HoverBike.transform.rotation = Quaternion.AngleAxis(rotacionobjectHoverBike, Vector3.up);
        Invoke("EliminarDisco", 1f);
        SpawnAudio.Play();
    }
    void EliminarDisco()
    {
        Destroy(Disco);
    }
}
