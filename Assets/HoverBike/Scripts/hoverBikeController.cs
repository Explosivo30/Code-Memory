using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hoverBikeController : MonoBehaviour
{
    public Rigidbody rigidbody;
    public float acceleration;
    public float rotationRate;

    public float turnRotationAngle;
    public float turnRotationSeekSpeed;

    private float rotationVelocity;
    //private float groundAngleVelocity;

    [SerializeField] GameObject camaraAActivar;
    [SerializeField] Vector3 hoverBikePosition;
    [SerializeField] GameObject Player;
    [SerializeField] GameObject PlayerInBike;

    public bool inBike = false;
    public bool playerInisde = false;
    public bool activarAnimacion = false;
    private bool YaPuedeBajarDeHoverBike = false;

    private void Start()
    {
        //Player = GameObject.FindWithTag("Player");
        //rigidbody.centerOfMass
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        { playerInisde = true; }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        { playerInisde = false; }
    }
    private void Update()
    {
        if ((playerInisde == true) && (Input.GetKeyDown(KeyCode.E)))
        {
            inBike = true;
            camaraAActivar.SetActive(true);
            Invoke("OcultarPlayer", 0.2f);
            Invoke("TiempoDeEsperaParaBajar", 0.3f);
            Debug.Log("InBike");
            playerInisde = false;
        }
        if ((inBike == true) && (Input.GetKeyDown(KeyCode.E)) && (YaPuedeBajarDeHoverBike == true))
        {
            activarAnimacion = false;
            hoverBikePosition = rigidbody.transform.position;
            inBike = false;
            Invoke("SpawnearPlayer", 0.7f);
            Invoke("DesactivarCam", 0.7f);
            YaPuedeBajarDeHoverBike = false;
        }
    }
    private void FixedUpdate()
    {
        if (inBike)
        {
            MovimientoHoverBike();
        }
        //Cunado el Player se hacerque y pulse E se Avtivara la funcion movimient oque es la que permitira mover la hoverBike, y tardara 8un par de segundos
        //para dar tiempo a la cama a colocarse en el sitio crrespondiente.

    }
    //Movimineto de la HoverBike
    public void MovimientoHoverBike()
    {
        if (Physics.Raycast(transform.position, transform.up * -1, 5f))
        {
            rigidbody.drag = 1;
            Vector3 forwardForce = transform.forward * acceleration * Input.GetAxis("Vertical");
            forwardForce = forwardForce * Time.deltaTime * rigidbody.mass;
            rigidbody.AddForce(forwardForce);
        }
        else
        {
            rigidbody.drag = 1;
        }
        Vector3 turnTorque = Vector3.up * rotationRate * Input.GetAxis("Horizontal");
        turnTorque = turnTorque * Time.deltaTime * rigidbody.mass;
        rigidbody.AddTorque(turnTorque);

        Vector3 newRotation = transform.eulerAngles;
        newRotation.z = Mathf.SmoothDampAngle(newRotation.z, Input.GetAxis("Horizontal") * -turnRotationAngle, ref rotationVelocity, turnRotationSeekSpeed);
        transform.eulerAngles = newRotation;

    }
    void OcultarPlayer()
    {
        activarAnimacion = true;
        Player.SetActive(false);
        PlayerInBike.SetActive(true);
    }
    void TiempoDeEsperaParaBajar()
    {
        YaPuedeBajarDeHoverBike = true;
    }
    //Esta funcion hara que una vez el player puse e para bajar spawnee el player al lado para hacer ver que ha vajado
    public void SpawnearPlayer()
    {
        Player.transform.position = hoverBikePosition;
        PlayerInBike.SetActive(false);
        Player.SetActive(true);
    }
    public void DesactivarCam()
    {
        camaraAActivar.SetActive(false);
    }
}