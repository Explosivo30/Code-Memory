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

    [Header("Camara")]
    [SerializeField] Transform camTrans;
    [SerializeField] CameraMove camMove;
    bool isWallRight = false;
    bool isWallLeft = false;


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
        Gizmos.DrawCube(groundCheck.position, new Vector3(0.6f, 0.2f, 0.5f));
        Gizmos.DrawCube(rightWallCheck.position, new Vector3(0.2f, 0.7f, 0.4f));
        Gizmos.DrawCube(leftWallCheck.position, new Vector3(0.2f, 0.7f, 0.4f));
    }
    
    
    void Update()
    {
        isGrounded = Physics.CheckBox(groundCheck.position, new Vector3(0.6f,0.2f,0.5f), Quaternion.identity, groundMask);
        isWallLeft = Physics.CheckBox(leftWallCheck.position, new Vector3(0.2f, 0.7f, 0.4f), Quaternion.identity, isWallGroundMask);
        isWallRight = Physics.CheckBox(rightWallCheck.position, new Vector3(0.2f, 0.7f, 0.4f), Quaternion.identity, isWallGroundMask);

        
        //if I'm wall running im grounded
        if (isWallLeft || isWallRight) { isGrounded = true; isWall = true; }
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
        if(lockControls == false) { PlayerMovement(); }

        Debug.Log("El Left wall running es" + isWallLeft);
        Debug.Log("El Right wall running es" + isWallRight);
        Debug.Log("Is grounded es " + isGrounded);

        IsWallRunning();
        Jump();

        Debug.Log(isWallLeft);
        Debug.Log(isWallRight);

        //if (Input.GetKeyDown(KeyCode.Space)) { isWall = false; }
        //if (isWall) { return; }
        if(isGrounded && velocity.y < 0)
        {
            velocity.y = isWall ? -1 : 0f;
        }
        
        velocity.y += gravity * Time.deltaTime;
        ch.Move(velocity * Time.deltaTime);

        //Tarda 0.5 en hacer efecto para que se desplace al lado
        DelayTime();

        // ¡Edgar tu puedes! ;)!!!!!!
        //Gracias guapa


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
    
    void PlayerWallRunning()
    {

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        move = transform.forward * z;

        //El sprint
        ch.Move(move * (isSprinting ? sprintSpeed : speed) * Time.deltaTime);
    }

    void Jump()
    {
        if(isGrounded == false) { return; }
        if (Input.GetKeyDown(KeyCode.Space))
        {

            Debug.Log("SALTAO");
            Debug.Log(isGrounded);


            if (isWallLeft)
            {
                JumpRight();
            }
            else if (isWallRight)
            {
                
                JumpLeft();
            }
            
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            Debug.Log(velocity.y);

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
        Debug.Log("esWallRight");
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            velocity.x = -transform.right.x * Mathf.Sqrt(100f);
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
        Debug.Log("esWallLeft");
        if (Input.GetKeyDown(KeyCode.Space))
        {
            velocity.x = transform.right.x * Mathf.Sqrt(100f);
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

}