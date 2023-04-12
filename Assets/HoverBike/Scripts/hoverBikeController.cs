using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;


public class hoverBikeController : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera vcam;
    public Rigidbody rigidbody;
    public Vector3 centerOfMass;
    public float acceleration;
    public float currrentTurboSpeed = 1f;
    public float TurboSpeed = 2f;
    public float rotationRate;

    public float turnRotationAngle;
    public float turnRotationSeekSpeed;

    private float rotationVelocity;
    public float anguloMaximoDeRotacionFrontal = 30f;
    public float anguloMaximoDeRotacionlateral = 30f;

    public float TimeToWaitStartMovements = 500f;
    //private float groundAngleVelocity;

    [SerializeField] GameObject camaraAActivar;
    [SerializeField] Vector3 hoverBikePosition;
    [SerializeField] GameObject Player;
    [SerializeField] GameObject PlayerInBike;
    [SerializeField] float maxRotation = 1f;
 
    public bool inBike = false;
    public bool playerInisde = false;
    public bool activarAnimacion = false;
    [SerializeField] float extraGravity = 500f;
    private bool YaPuedeBajarDeHoverBike = false;
    private bool IsGorund = true;
    private float curretTimeToWait = 0f;
    [SerializeField] float basicExtraForce = 1000f;


    private void Start()
    {
        currrentTurboSpeed = 1f;
        //Player = GameObject.FindWithTag("Player");
        rigidbody = GetComponent<Rigidbody>();
        rigidbody.centerOfMass = centerOfMass;
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
            Invoke("OcultarPlayer", 0f);
            Invoke("TiempoDeEsperaParaBajar", 0.3f);
            playerInisde = false;
        }
        if (inBike == true)
        {
            curretTimeToWait += 1;
        }
        //GirarMoto();
        if ((inBike == true) && (Input.GetKeyDown(KeyCode.E)) && (YaPuedeBajarDeHoverBike == true) && IsGorund == true)
        {
            activarAnimacion = false;
            inBike = false;
            Invoke("SpawnearPlayer", 0.9f);
            Invoke("DesactivarCam", 0.9f);
            YaPuedeBajarDeHoverBike = false;
            rigidbody.drag = 4f;
        }
        if (inBike == false)
        {
            rigidbody.drag = 7f;
            curretTimeToWait += 0;
        }
        if (Input.GetKeyDown("space"))
        {
            currrentTurboSpeed = TurboSpeed;            
        }
        if (Input.GetKeyUp("space"))
        {
            currrentTurboSpeed = 1f;
        }
    }
    private void FixedUpdate()
    {
        if (inBike && curretTimeToWait >= TimeToWaitStartMovements)
        {
            MovimientoHoverBike();
            ConstraintXZRotation();
        }
        //Cunado el Player se hacerque y pulse E se Avtivara la funcion movimient oque es la que permitira mover la hoverBike, y tardara 8un par de segundos
        //para dar tiempo a la cama a colocarse en el sitio crrespondiente.
    }
    //Movimineto de la HoverBike
    //void GirarMoto()
    //{
    //    if (Time.deltaTime < Time.fixedDeltaTime)
    //    {
    //        return;
    //    }
    //    if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit Hit, 30f))
    //    {
    //        Vector3 GroundForward = Vector3.Cross(Hit.normal, -transform.right);
    //        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(GroundForward), maxRotation);
    //    }
    //}
    Vector3 DirecionMoto()
    {
        Vector3 Direction = transform.forward;
        if (Physics.Raycast(transform.position,Vector3.down, out RaycastHit Hit, 30f))
        {
            Vector3 GroundForward = Vector3.Cross(Hit.normal, -transform.right); 
            if (Hit.distance > 2f)
            {
                Direction = GroundForward;
            }            
            if (Time.deltaTime < Time.fixedDeltaTime)
            {
                transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(GroundForward), maxRotation);
            }
            Debug.DrawRay(Hit.point, GroundForward, Color.red);
        }
        return Direction;
    }
    public void MovimientoHoverBike()
    {
        if (Physics.Raycast(transform.position, transform.up * -1, 5f))
        {
            rigidbody.drag = 0.7f;
            Vector3 forwardForce = DirecionMoto() * acceleration * currrentTurboSpeed * Input.GetAxis("Vertical");
            forwardForce = forwardForce * Time.deltaTime * rigidbody.mass;
            rigidbody.AddForce(forwardForce);
            IsGorund = true;
            rigidbody.AddForce(-transform.up * basicExtraForce);
        }
        else
        {
            Vector3 automaticTurnForce;
            rigidbody.drag = 0;
            IsGorund = false;
            float turn = Input.GetAxis("Vertical");
            automaticTurnForce = transform.right * Time.deltaTime * rigidbody.mass * turn * 100f;
            rigidbody.AddTorque(automaticTurnForce);
            //Extra Gravity
            rigidbody.AddForce(-Vector3.up * extraGravity);
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
        hoverBikePosition = rigidbody.transform.position;
        Player.transform.position = hoverBikePosition;
        PlayerInBike.SetActive(false);
        Player.SetActive(true);
    }
    public void DesactivarCam()
    {
        camaraAActivar.SetActive(false);
    }
    //private void ConstraintXZRotation()
    //{
    //    Vector3 eulerRotation = transform.rotation.eulerAngles;
     
    //    if (eulerRotation.x > 180f) { eulerRotation.x -= 360f; }
    //    if (eulerRotation.x < -180f) { eulerRotation.x += 360f; }
     
    //    eulerRotation.x = Mathf.Clamp(eulerRotation.x, -anguloMaximoDeRotacion, anguloMaximoDeRotacion);
    //    //eulerRotation.z = Mathf.Clamp(eulerRotation.z, -30f, 30f);
    //    transform.rotation = Quaternion.Euler(eulerRotation);
    //}

    private void ConstraintXZRotation()
    {
        //Vector3 localUpToWorld = transform.TransformDirection(Vector3.up);

        //Quaternion localUpWorldToWorldUp = Quaternion.FromToRotation(localUpToWorld, Vector3.up);


        {
            Vector3 right = transform.right;
            Vector3 onPlaneRight = -Vector3.Cross(transform.forward, Vector3.up);
            //Debug.DrawRay(transform.position, onPlaneRight, Color.cyan);

            float zAngle = Vector3.SignedAngle(right, onPlaneRight, transform.forward);
            float zDesiredAngle =
                zAngle > anguloMaximoDeRotacionlateral ? anguloMaximoDeRotacionlateral :
                zAngle < -anguloMaximoDeRotacionlateral ? -anguloMaximoDeRotacionlateral :
                zAngle;
            float angleToApply = zAngle - zDesiredAngle;

            //Debug.Log($"zAngle - {zAngle} ; zDesiredAngle - {zDesiredAngle} ; angleToApply - {angleToApply}");
            Quaternion zCorrect = Quaternion.AngleAxis(angleToApply, transform.forward);
            transform.rotation = zCorrect * transform.rotation;
        }


        {
            Vector3 forward = transform.forward;
            Vector3 onPlaneForward = Vector3.Cross(transform.right, Vector3.up);
            //Debug.DrawRay(transform.position, onPlaneForward, Color.magenta);

            float xAngle = Vector3.SignedAngle(forward, onPlaneForward, transform.right);
            float xDesiredAngle =
                xAngle > anguloMaximoDeRotacionFrontal ? anguloMaximoDeRotacionFrontal :
                xAngle < -anguloMaximoDeRotacionFrontal ? -anguloMaximoDeRotacionFrontal :
                xAngle;
            float angleToApply = xAngle - xDesiredAngle;

            //Debug.Log($"xAngle - {xAngle} ; xDesiredAngle - {xDesiredAngle} ; angleToApply - {angleToApply}");
            Quaternion xCorrect = Quaternion.AngleAxis(angleToApply, transform.right);
            transform.rotation = xCorrect * transform.rotation;
        }


        //transform.rotation = localUpWorldToWorldUp * transform.rotation;
    }

}