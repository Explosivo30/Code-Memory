using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapplingPower : MonoBehaviour
{

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
        
    }

    // Update is called once per frame
    void Update()
    {
       
            switch (state)
            {
                default:
                case State.Normal:
                hookshotTransform.transform.position = hookshotTransform.parent.transform.position;
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
                hookshotTransform.localScale = Vector3.zero;
                state = State.HookshotThrown;

            }
        }
    }

    void HandleHookshotThrow()
    {
        hookshotTransform.LookAt(hookShotPosition);

        float hookshotThrowSpeed = 140f;
        hookshotSize += hookshotThrowSpeed * Time.deltaTime;
        hookshotTransform.localScale = new Vector3(1, 1, hookshotSize);
        if(hookshotSize >= Vector3.Distance(transform.position, hookShotPosition))
        {

            state = State.HookshotFlyingPlayer;
        }
    }

    void HandleHookshotMovement()
    {
        hookshotTransform.LookAt(hookShotPosition);

        Vector3 hookshotDir = (hookShotPosition - transform.position).normalized;

        float hookshotSpeed = 25f;
        //Move CharacterController
        cc.Move(hookshotDir * hookshotSpeed * Time.deltaTime);
        if (Vector3.Distance(transform.position, hookShotPosition) < 2f)
        {
            hookshotTransform.gameObject.SetActive(false);
            state = State.Normal;
        }

        if (TestInputDownHookshot())
        {
            //Cancel Hookshot
            state = State.Normal;
        }
        
        
        if (playerMovement.TestInputJump())
        {
            playerMovement.characterVelocityMomentum = hookshotDir * hookshotSpeed * momentumGrapplingHookRange * Time.deltaTime;
            state = State.Normal;
            DebugHitPointTransform.position = new Vector3(0f, 0f, 0f);
        }
        
    }

    bool TestInputDownHookshot()
    {
        return Input.GetMouseButtonDown(1);
    }

    
    
}
