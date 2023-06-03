using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;
using Cinemachine;

public class MovementPlayer : MonoBehaviour
{
    [SerializeField] Animator anim;
    [Header("Player")]
    float speed = 12f;
    [SerializeField]float walkingSpeed = 15f;
    [SerializeField] float maxSpeed = 40f;
    [SerializeField] CharacterController ch;
    [SerializeField] float cooldownBoostTotal;
    [SerializeField] float cooldownBoostTimer = 5f;
    [SerializeField] float gravity = -9.81f;
    public bool lockControls = false;
    public bool bikeLockControls = false; 
    [SerializeField]bool playerTouchingSomething;

    #region SprintWallRunning
    float timeBoostLeft;
    [SerializeField]float timerBoostTotal;
    [SerializeField]float sumarVelocidad = 5f;
    [SerializeField] CinemachineVirtualCamera playerCamFov;
    float startingFov;
    [SerializeField]GameObject[] numberSpeed;
    int currentNumberSpeed = 0;
    #endregion

    #region WallRunning
    RaycastHit leftWallHit;

    RaycastHit rightWallHit;

    [SerializeField]float raycastLenght = 2f;
    [SerializeField]float groundRaycastLenght = .2f;

    public UnityEvent onMovilityWallEvent = new UnityEvent();

    #endregion

    Vector3 move;
    [SerializeField] float timeWallJumpAir = 1f;
    [SerializeField] Vector3 velocity;

    [Header("Ckecking Attributes")]
    [SerializeField] Transform groundCheck;
    [SerializeField] Transform rightWallCheck;
    [SerializeField] Transform leftWallCheck;
    public LayerMask groundMask;
    public LayerMask isWallGroundMask;
    [SerializeField] float jumpHeight = 24f;


    #region Grappling
    GrapplingPower grapplingPower;
    public Vector3 characterVelocityMomentum;
    #endregion

    [SerializeField] float sideJump = 12f;

    bool isGrounded;


    bool isWall;

    [Header("Camara")]
    [SerializeField] Transform camTrans;
    [SerializeField] CameraMove camMove;
    bool isWallRight = false;
    bool isWallLeft = false;
        

    public bool freeze;

    [Header("Sprint")]
    float sprintSpeed = 2f;
    
    bool canSprint = true;
     
    bool isSprinting => canSprint;
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

    [SerializeField] AudioSource audioSalto;


    private void OnEnable()
    {
        defaultYPos = camTrans.transform.localPosition.y;
    }

    private void Awake()
    {
        grapplingPower = GetComponent<GrapplingPower>();
        onMovilityWallEvent.AddListener(Jump);
        canSprint = false;
        timeBoostLeft = timerBoostTotal;
        startingFov = playerCamFov.m_Lens.FieldOfView;

    }

    // Start is called before the first frame update
    void Start()
    {
        speed = walkingSpeed;
        sprintSpeed = maxSpeed;
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
        if (!grapplingPower.isGrappling)
        {
            timeBoostLeft -= Time.deltaTime;
            if(timeBoostLeft <= 0)
            {
                canSprint = false;
            }
            //NO ES QUE FUNCIONE MAL ES QUE HAS DE IR A WALL RUNNING MOVEMENT Y ACABARLO PORQUE ESTA EL LOCK CONTROLS
            CheckForWall();

            //if I'm wall running im grounded
            if (isWallLeft || isWallRight) 
            {
                if (isWall == false && speed <= maxSpeed)
                {
                    speed += sumarVelocidad;
                    playerCamFov.m_Lens.FieldOfView += 2.5f;
                    AddNewUINumberSpeed();

                    cooldownBoostTimer = cooldownBoostTotal;
                }

                canSprint = true;
                timeBoostLeft = timerBoostTotal;
                isGrounded = true;
                isWall = true;
                WallRunningMovement();
                lockControls = true;
                
            }
            else { isWall = false; lockControls = false; }

            cooldownBoostTimer -= Time.deltaTime;
            if(cooldownBoostTimer <= 0f)
            {
                speed = walkingSpeed;
                playerCamFov.m_Lens.FieldOfView = startingFov;
                UpdateSpeedUIReset();
            }

            Slide();

            ResetCamera();

            HandleHeadbob();

            //Si lockear los controles es falso  sigues caminando
            if (lockControls == false && bikeLockControls == false) { PlayerMovement(); }
            



            IsWallRunning();
            Jump();

            if (isGrounded && velocity.y < 0f)
            {
                velocity.y = isWall ? -.2f : -10f;
                //velocity.y = -1;
            }
            
                velocity.y += gravity * Time.deltaTime;

            switch (isGrounded)
            {
                case false:
                    anim.SetBool("hasJumped", true);
                    anim.SetBool("returnedToGround", false);
                    break;

                case true:
                    anim.SetBool("hasJumped", false);
                    anim.SetBool("returnedToGround", true);
                    break;
            }

            ch.Move(velocity * Time.deltaTime);

                // ¡Edgar tu puedes! ;)!!!!!!
                // Gracias guapa
        }
        
        if(grapplingPower.isGrappling)
        {
            velocity.y = 0f;
        }
    }

    private void AddNewUINumberSpeed()
    {
        numberSpeed[currentNumberSpeed].SetActive(true);
        currentNumberSpeed += 1;
    }

    private void UpdateSpeedUIReset()
    {
        foreach (GameObject multiplicador in numberSpeed)
        {
            multiplicador.SetActive(false);
        }
        currentNumberSpeed = 0;
    }

    private void WallRunningMovement()
    {
        Vector3 wallNormal = isWallRight ? rightWallHit.normal : leftWallHit.normal;


        Vector3 wallFordward = Vector3.Cross(wallNormal, transform.up);
        ch.Move(move * speed * Time.deltaTime);

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
        //Debug.Log("moviendome");
        //Debug.Log(bikeLockControls);
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        if (x == 0f && z == 0f)
        {
            anim.SetBool("moving", false);
        }
        else
        {
            anim.SetBool("moving", true);
        }
        move = transform.right * x + transform.forward * z;
        move += characterVelocityMomentum;
        //El sprint
        ch.Move(move * (speed) * Time.deltaTime);
        


        //Dampen o restar momentum

        if (characterVelocityMomentum.magnitude >= 0f)
        {
            float momentumDrag = 1f;
            characterVelocityMomentum -= characterVelocityMomentum * momentumDrag * Time.deltaTime;
            if (characterVelocityMomentum.magnitude < .0f)
            {
                characterVelocityMomentum = Vector3.zero;
            }
        }
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
            audioSalto.Play();
            Debug.Log("Salto saltado con audio");
        }
    }

    void EventJumpWall()
    {
        canSprint = false;
    }

    //For Test imput jump is for Hook
    public bool TestInputJump()
    {
        return Input.GetKeyDown(KeyCode.Space);
    }

    void ZeroGravity()
    {
        velocity.y = 0;
    }

    void DashMovementSide()
    {
        DashMoment();
    }

    void ClampCamera()
    {
        float newClamp = camTrans.transform.rotation.y;
        newClamp = Mathf.Clamp(camTrans.transform.rotation.y, newClamp - 3, newClamp + 3);
        camMove.rotateZ = newClamp;
    }

    IEnumerator DashMoment()
    {
        float startTime = 2f;
        float dashTime = 24f;
        while (Time.time < startTime + dashTime)
        {
            ch.Move(transform.forward * _dashSpeed * Time.deltaTime);
        }
        

        yield return null;
    }
    void JumpLeft()
    {
        //Debug.Log("esWallRight");
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            velocity = (-transform.right + camTrans.forward) * sideJump;
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
            velocity = (transform.right + camTrans.forward) * sideJump; //Mathf.Sqrt(100f);
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
        yield return new WaitForSeconds(timeWallJumpAir);
        
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
        if (canSprint == true && Input.GetKeyDown(KeyCode.LeftControl))
        {
            isSliding = true;
            //No agachara la cabeza porque en HandleBOB con el movimiento siempre rota a traves de 
            camTrans.localPosition = Vector3.Lerp(new Vector3(camTrans.localPosition.x, camTrans.localPosition.y, camTrans.localPosition.z), new Vector3(camTrans.localPosition.x, cameraSlideDown, camTrans.localPosition.z), 0.11f);
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

                // Hola Edgar soy Lidia
                // Aunque te diga que programar es el lenguaje de los virgenes sé lo mucho que cuesta, mucho ánimo mi rey!!! <3
            } 

        }
        else
        {
            isSliding = false;
            
            //Reseteamos el center del character controller cuando se levante
            camTrans.localPosition = Vector3.Lerp(new Vector3(camTrans.localPosition.x, camTrans.localPosition.y, camTrans.localPosition.z), new Vector3(camTrans.localPosition.x, defaultYPos, camTrans.localPosition.z), 0.11f);
            ch.center = Vector3.zero;
            ch.height = 2f;
            //speed = walkingSpeed;
            sprintSpeed = maxSpeed;
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
    float _dashSpeed = 200f;

    void SetVelocity()
    {
        velocity = velocityToSet;
    }

    //private void OnControllerColliderHit(ControllerColliderHit hit)
    //{
    //    ResetVelocity();

    //    //Delay resetVelocity if goes weird
    //}

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        ResetVelocity();
    }

    public void SetBikeLockControls(bool put)
    {
        bikeLockControls = put;
    }





    /*
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
    */

    //Saludos humano, pasa un buen dia <3
}