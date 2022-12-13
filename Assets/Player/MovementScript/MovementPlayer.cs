using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementPlayer : MonoBehaviour
{

    [Header("Player")]
    float speed = 12f;
    [SerializeField]float speedMax = 12f;
    [SerializeField] CharacterController ch;
    [SerializeField] float gravity = -9.81f;
    bool lockControls = false;

    #region WallRunning
    RaycastHit leftWallHit;

    RaycastHit rightWallHit;

    [SerializeField]float raycastLenght = 2f;
    [SerializeField]float groundRaycastLenght = 1.5f;

    #endregion

    Vector3 move;

    [SerializeField] Vector3 velocity;

    [Header("Ckecking Attributes")]
    [SerializeField] Transform groundCheck;
    [SerializeField] Transform rightWallCheck;
    [SerializeField] Transform leftWallCheck;
    public LayerMask groundMask;
    public LayerMask isWallGroundMask;
    [SerializeField] float jumpHeight = 24f;
    bool isGrounded;


    bool isWall;
    private bool activeGrapple;

    [Header("Camara")]
    [SerializeField] Transform camTrans;
    [SerializeField] CameraMove camMove;
    bool isWallRight = false;
    bool isWallLeft = false;


    public bool freeze;

    [Header("Sprint")]
    float sprintSpeed = 24f;
    [SerializeField]float sprintSpeedMax = 24f;
    bool canSprint = true;
    KeyCode sprintKey = KeyCode.LeftShift;
    bool isSprinting => canSprint && Input.GetKey(sprintKey);
    bool isWallrunning => isWall;


    [Header("HeadBob")]
    [SerializeField] float walkBobSpeed = 14f;
    [SerializeField] float walkBobAmount = 0.05f;
    [SerializeField] float sprintBobSpeed = 14f;
    [SerializeField] float sprintBobAmount = 0.11f;
    float slideAmount = 0f;
    float defaultYPos;
    float timer;


    [Header("Sliding")]
    [SerializeField] float slideSpeedDecay = 8f;
    [SerializeField] float cameraSlideDown = -0.5f;
    bool isSliding = false;


    private void OnEnable()
    {
        defaultYPos = camTrans.transform.localPosition.y;
    }

    // Start is called before the first frame update
    void Start()
    {
        speed = speedMax;
        sprintSpeed = sprintSpeedMax;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(1, 0, 0, 0.6f);
        Gizmos.DrawCube(groundCheck.position, new Vector3(1f, 0.4f, 1f));
        Gizmos.DrawRay(transform.position, transform.right);
        Gizmos.DrawRay(transform.position, -transform.right);
    }
    
    
    void Update()
    {
        //NO ES QUE FUNCIONE MAL ES QUE HAS DE IR A WALL RUNNING MOVEMENT Y ACABARLO PORQUE ESTA EL LOCK CONTROLS
        CheckForWall();


        //if I'm wall running im grounded
        if (isWallLeft || isWallRight) 
        { 
            isGrounded = true;
            isWall = true;
            WallRunningMovement();
        }
        else { isWall = false; }

        Debug.Log("Is wall es + " + isWall);


        /*
        //MIRAMOS SI ESTOYEN MID AIR
        Debug.Log("Estoy en suelo es : " + isGrounded);
        if (isGrounded || isWallLeft || isWallRight && velocity.y < 0f)
        {
            Debug.Log("HACES COSAS");
            velocity.y = -1f;
        }
        */

        Slide();

        ResetCamera();

        HandleHeadbob();

        //Si lockear los controles es falso  sigues caminando
        if (lockControls == false) { PlayerMovement(); }

        //Debug.Log("El Left wall running es" + isWallLeft);
        //Debug.Log("El Right wall running es" + isWallRight);
        //Debug.Log("Is grounded es " + isGrounded);

        IsWallRunning();
        Jump();

        //Debug.Log("is wall  left " + isWallLeft);
        //Debug.Log("is wall right " + isWallRight);

        
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = isWall ? -1f : 0f;
            velocity.y = -1f;
        }
        
        velocity.y += gravity * Time.deltaTime;

        ch.Move(velocity * Time.deltaTime);
        

        //Tarda 0.5 en hacer efecto para que se desplace al lado
        DelayTime();

        // ¡Edgar tu puedes! ;)!!!!!!
        //Gracias guapa


    }

    private void WallRunningMovement()
    {
        Vector3 wallNormal = isWallRight ? rightWallHit.normal : leftWallHit.normal;


        Vector3 wallFordward = Vector3.Cross(wallNormal, transform.up);
        //lockControls = true;

        //TODO EMPUJAR POR LA NORMAL DE LA PARED NO TENGO ADDFORCE

    }

    private void CheckForWall()
    {
        isGrounded = Physics.Raycast(transform.position, -transform.up,groundRaycastLenght, groundMask);
        //isWallLeft = Physics.CheckBox(leftWallCheck.position, new Vector3(0.2f, 0.7f, 0.4f), Quaternion.identity, isWallGroundMask);
        isWallLeft = Physics.Raycast(transform.position, -transform.right, out leftWallHit, raycastLenght, isWallGroundMask);
        //isWallRight = Physics.CheckBox(rightWallCheck.position, new Vector3(0.2f, 0.7f, 0.4f), Quaternion.identity, isWallGroundMask);
        isWallRight = Physics.Raycast(transform.position, transform.right, out leftWallHit, raycastLenght, isWallGroundMask);
    }

    private void IsWallRunning()
    {
        if (isWallLeft)
        {
            camMove.rotateZ = Mathf.Lerp(camMove.rotateZ, -45f, Time.deltaTime);
            
        }
        else if (isWallRight)
        {
            camMove.rotateZ = Mathf.Lerp(camMove.rotateZ, 45f, Time.deltaTime);
            
        }
    }

    public void LockControls(bool lockControls)
    {
        //Si envias false sera false aqui
        this.lockControls = lockControls;
    }
    void PlayerMovement()
    {

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        move = transform.right * x + transform.forward * z;

        //El sprint
        ch.Move(move * (isSprinting ? sprintSpeed : speed) * Time.deltaTime);
    }
    
    

    void Jump()
    {
        if(isGrounded == false) { return; }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            
            if (isWallLeft)
            {
                JumpRight();
            }
            else if (isWallRight)
            {
                
                JumpLeft();
            }
            isGrounded = false;
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            //Debug.Log(velocity.y);

        }
    }

    void ZeroGravity()
    {
        velocity.y = 0;
    }

    void DashMovementSide()
    {

    }

    void ClampCamera()
    {
        float newClamp = camTrans.transform.rotation.y;
        newClamp = Mathf.Clamp(camTrans.transform.rotation.y, newClamp - 3, newClamp + 3);
        camMove.rotateZ = newClamp;
    }


    void JumpLeft()
    {
        //Debug.Log("esWallRight");
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            velocity = -transform.right * 10f;
            isWallRight = false;
        }

        if (isGrounded) 
        {
            //Tarda 0.5 en hacer efecto para que se desplace al lado
            StartCoroutine(DelayTime());
        }
    }

    void JumpRight()
    {
        //Debug.Log("esWallLeft");
        if (Input.GetKeyDown(KeyCode.Space))
        {
            velocity = transform.right * 10f; //Mathf.Sqrt(100f);
            isWallLeft = false;
        }

        if (isGrounded) 
        {
            StartCoroutine(DelayTime());
        }
    }

    //Ienumerator para que tarde un poco en ejecutarse
    IEnumerator DelayTime()
    {
        //Debug.Log("Y ahora esperamos 0.5");
        yield return new WaitForSeconds(0.7f);
        
        DelayResetVelocityX();
    }

    void DelayResetVelocityX()
    {
        //Debug.Log("HA PASADO SE HA PARADO!!!");
        velocity.x = 0;
        velocity.z = 0;
    }

    private void ResetCamera()
    {

        if (!isWallLeft || !isWallRight && camMove.rotateZ != 0f)
        {
            camMove.rotateZ = Mathf.Lerp(camMove.rotateZ, 0f, 4f* Time.deltaTime);
        }
    }

    void Slide()
    {
        if (isSprinting && Input.GetKey(KeyCode.LeftControl))
        {
            isSliding = true;
            //No agachara la cabeza porque en HandleBOB con el movimiento siempre rota a traves de 
            camTrans.localPosition = Vector3.Lerp(new Vector3(0f, camTrans.localPosition.y, 0f), new Vector3(0f, cameraSlideDown, 0f), 0.11f);
            //We slow the dash and stop it from going backwards

            if (sprintSpeed < 0.1f)
            {
                sprintSpeed = 0f;
            }
            else
            {
                //Movemos el centro del character controller para el dash
                ch.center = new Vector3(0f, -0.28f, 0f);
                ch.height = 1.28f;
                sprintSpeed -= Time.deltaTime * slideSpeedDecay;
            } 


            
        }
        else
        {
            isSliding = false;
            Debug.Log("Subimos la camara");
            //Reseteamos el center del character controller cuando se levante
            camTrans.localPosition = Vector3.Lerp(new Vector3(0f, camTrans.localPosition.y, 0f), new Vector3(0f, defaultYPos, 0f), 0.11f);
            ch.center = Vector3.zero;
            ch.height = 2f;
            speed = speedMax;
            sprintSpeed = sprintSpeedMax;
        }
    }

    void HandleHeadbob()
    {
        if (!isGrounded) return;
        if (isSliding) return;

        if (Mathf.Abs(move.x) > 0.1f || Mathf.Abs(move.z) > 0.1)
        {
            //Sacamos el modo if pro
            timer += Time.deltaTime * (isSliding ? slideAmount : isSprinting ? sprintBobSpeed : walkBobSpeed);
            camTrans.transform.localPosition = new Vector3(
                camTrans.transform.localPosition.x, camTrans.transform.localPosition.y + Mathf.Sin(timer) * (isSliding ? slideAmount : isSprinting ? sprintBobAmount :  walkBobAmount), camTrans.transform.localPosition.z);
            //Pongo la localtrans.y para que cuando se ponga el dash en false el headbob no teleporte la camara arriba y haga la animacion.
        }
        
    }


    public void ResetVelocity()
    {
        velocity = Vector3.zero;
    }

    
    Vector3 velocityToSet;

    void SetVelocity()
    {
        velocity = velocityToSet;
    }

    public void JumpToPosition(Vector3 targetPosition, float trajectoryHeight)
    {
        activeGrapple = true;

        velocityToSet = CalculateJumpVelocity(transform.position, targetPosition, trajectoryHeight);

        Invoke(nameof(SetVelocity), 0.1f);


    }


    public Vector3 CalculateJumpVelocity(Vector3 startPoint, Vector3 endPoint, float trajectoryHeight)
    {

        float gravity = this.gravity;
        float displacementY = endPoint.y - startPoint.y;
        Vector3 displacementXZ = new Vector3(endPoint.x - startPoint.x, 0f, endPoint.z - startPoint.z);

        Vector3 velocityY = Vector3.up * Mathf.Sqrt(-2 * gravity * trajectoryHeight);

        Vector3 velocityXZ = displacementXZ / (Mathf.Sqrt(-2 * trajectoryHeight / gravity)
            + Mathf.Sqrt(2 * (displacementY - trajectoryHeight) / gravity));

        return velocityXZ + velocityY;

    }

}