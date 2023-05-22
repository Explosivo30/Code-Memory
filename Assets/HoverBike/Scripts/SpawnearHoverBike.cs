using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnearHoverBike : MonoBehaviour
{
    [SerializeField] Animator anim;
    private Rigidbody objectToInstantiate;
    [SerializeField] GameObject Disco;
    [SerializeField] Transform player;
    Rigidbody objectInstantiation;
    [SerializeField] float gravity = -18;
    [SerializeField] float h = 5f;
    [SerializeField] bool timerToSpawn = true;
    public float timer = 0;
    [SerializeField] float TimeToWaitToSpawn = 100f;


    private void Awake()
    {
        objectToInstantiate = Disco.GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.V) && timerToSpawn == true)
        {
            Invoke("ThrowObject",1.1f);
            timerToSpawn = false;
            anim.SetBool("SacarMoto", true);
            Invoke("DesactivarAnimacion", 0.5f);

        }
        if (timer >= TimeToWaitToSpawn)
        {
            timer = 0f;
            timerToSpawn = true;
        }
        timer += 1f;
    }
    void DesactivarAnimacion()
    {
        anim.SetBool("SacarMoto", false);
    }
    #region ThrowWeapon
    void ThrowObject()
    {
        objectInstantiation = Instantiate(objectToInstantiate, transform.position, Quaternion.identity) as Rigidbody;
        Physics.gravity = Vector3.up * gravity;
        objectInstantiation.useGravity = true;
        objectInstantiation.velocity = CalculateThrow(player);
        Debug.Log(CalculateThrow(player));
    }



    public Vector3 CalculateThrow(Transform endPos)
    {
        float displacementY = endPos.position.y - objectInstantiation.transform.position.y;
        Vector3 displacementXZ = new Vector3(endPos.position.x - objectInstantiation.transform.position.x, 0, endPos.position.z - objectInstantiation.transform.position.z);


        Vector3 velocityY = Vector3.up * Mathf.Sqrt(-2 * gravity * h);
        Vector3 velocityXZ = displacementXZ / (Mathf.Sqrt(-2 * h / gravity) + Mathf.Sqrt(2 * (displacementY - h) / gravity));

        return velocityXZ + velocityY;
    }
    #endregion

}
