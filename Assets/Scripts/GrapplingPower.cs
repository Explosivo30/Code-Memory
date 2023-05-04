using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapplingPower : MonoBehaviour
{
    [SerializeField] AudioSource soundGancho;
    [SerializeField] Transform cameraTransform;
    [SerializeField] Transform DebugHitPointTransform;
    [HideInInspector] public bool isGrappling = false;
    CharacterController cc;
    Vector3 hookShotPosition;
    float hookshotSize;
    [SerializeField]LayerMask layerMask;
    [SerializeField] Transform hookshotTransform;
    MovementPlayer playerMovement;
    [SerializeField] float hookRange = 50f;
    [SerializeField] float momentumGrapplingHookRange = 2f;
    [SerializeField] float moveTowardsGrappedPoint = 25f;
    [SerializeField] float hookshotThrowSpeed = 500f;
    [SerializeField] Animator anim;

    public State state;
    public enum State
    {
        Normal,
        HookshotFlyingPlayer,
        HookshotThrown
    }

    private void Awake()
    {
        state = State.Normal;
        cc = GetComponent<CharacterController>();
        hookshotTransform.gameObject.SetActive(false);
        playerMovement = GetComponent<MovementPlayer>();
        
    }

    // Start is called before the first frame update
    void Start()
    {
        EventForGame.instance.deadWhip.AddListener(ResetGrapplingDeath);
    }

    // Update is called once per frame
    void Update()
    {
       
            switch (state)
            {
                default:
                case State.Normal:
                    hookshotTransform.transform.position = hookshotTransform.parent.transform.position;
                DebugHitPointTransform.position = transform.position;
                hookshotTransform.gameObject.SetActive(false);
                
                    
                    HandleHookshotStart();
                    isGrappling = false;
                    
                break;
                case State.HookshotFlyingPlayer:
                    isGrappling = true;
                    HandleHookshotMovement();
                    break;
                case State.HookshotThrown:
                    HandleHookshotThrow();
                    break;
            }
        
        
        
    }


    void HandleHookshotStart()
    {
        if (TestInputDownHookshot())
        {
            if (Physics.Raycast(cameraTransform.position, cameraTransform.forward, out RaycastHit raycastHit, hookRange, layerMask))
            {
                DebugHitPointTransform.position = raycastHit.point;
                hookShotPosition = raycastHit.point;
                hookshotSize = 0f;
                hookshotTransform.gameObject.SetActive(true);
                //hookshotTransform.localScale = Vector3.zero;
                anim.SetBool("usingGancho", true);
                state = State.HookshotThrown;

            }
        }
    }

    void HandleHookshotThrow()
    {
        //hookshotTransform.LookAt(hookShotPosition);

        soundGancho.Play();
        hookshotSize += hookshotThrowSpeed * Time.deltaTime;
        //hookshotTransform.localScale = new Vector3(1, 1, hookshotSize);

        if(hookshotSize >= Vector3.Distance(transform.position, hookShotPosition))
        {

            state = State.HookshotFlyingPlayer;
        }
    }

    void HandleHookshotMovement()
    {
        //hookshotTransform.LookAt(hookShotPosition);

        Vector3 hookshotDir = (hookShotPosition - transform.position).normalized;

        
        //Move CharacterController
        cc.Move(hookshotDir * moveTowardsGrappedPoint * Time.deltaTime);
        if (Vector3.Distance(transform.position, hookShotPosition) < 2f)
        {
            anim.SetBool("usingGancho", false);
            hookshotTransform.gameObject.SetActive(false);
            state = State.Normal;
        }

        if (TestInputDownHookshot())
        {
            //Cancel Hookshot
            anim.SetBool("usingGancho", false);
            state = State.Normal;
        }
        
        
        if (playerMovement.TestInputJump())
        {
            playerMovement.characterVelocityMomentum = hookshotDir * moveTowardsGrappedPoint * momentumGrapplingHookRange * Time.deltaTime;
            anim.SetBool("usingGancho", false);
            state = State.Normal;
            DebugHitPointTransform.position = transform.position;
        }
        
    }

    bool TestInputDownHookshot()
    {
        return Input.GetMouseButtonDown(1);
    }


    void ResetGrapplingDeath()
    {
        state = State.Normal;
    }
    
    
}
