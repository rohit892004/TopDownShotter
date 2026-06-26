using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("InputSciptRef")]
    private Player controll;

    private PlayerData playerData;
    private CharacterController characterController;
    private Animator animator;

    [Header("PLayerValue")]
    private Vector2 Input;//INputsystem
    public Vector2 aim;//inputSystem
    public Vector3 moveInput; //Mydefine
    public float walkspeed = 5f;
    public float speed;
    public float runspeed=10f;
    public bool IsRunning;  


    public float verticalVeloctiy; //forJump


    [Header("Aiming")]
    [SerializeField]private LayerMask layerMask;
    private Vector3 aimDir;
    public Transform aimobj;


    private void Start()
    {
        playerData = GetComponent<PlayerData>();    
        characterController = GetComponent<CharacterController>();
        animator = GetComponentInChildren<Animator>();
        speed = walkspeed;
        AssignInput();
    }

    private void Update()
    {
        CharMovement();
        Aim();
        AminatorContorller();
    }
    public void CharMovement()
    {
        moveInput = new Vector3(Input.x, 0, Input.y).normalized;
        ApplyGravity();

        if (moveInput.magnitude > 0)
        {
            characterController.Move(moveInput * Time.deltaTime * speed);
        }
    }

    public void ApplyGravity()
    {
        if(!characterController.isGrounded)
        {
            verticalVeloctiy -= 9.81f * Time.deltaTime;
            moveInput.y = verticalVeloctiy;
        }
        else
        {
            verticalVeloctiy = -.5f;
        }
    }

    public void Aim()
    {
        Ray ray = Camera.main.ScreenPointToRay(aim);
        if(Physics.Raycast(ray,out var hitinfo, Mathf.Infinity, layerMask))
        {
            aimDir = hitinfo.point - transform.position;
            aimDir.y = 0f;
            aimDir.Normalize();
            transform.forward = aimDir;
            aimobj.position = new Vector3(hitinfo.point.x, aimobj.position.y, hitinfo.point.z);

        }
    }

    public void AminatorContorller()
    {
        float Xvelocity = Vector3.Dot(moveInput.normalized, transform.right);
        float Zvelocity = Vector3.Dot(moveInput.normalized, transform.forward);
        animator.SetFloat("Xvelocity", Xvelocity , .1f ,Time.deltaTime);
        animator.SetFloat("Zvelocity", Zvelocity ,.1f, Time.deltaTime);
        bool playRunAnimation = IsRunning && moveInput.magnitude > 0;
        animator.SetBool("IsRunning", playRunAnimation);
    }


    public void AssignInput()
    {
        controll = playerData.control;

        //INPUT ACCESS systemt
        controll.Charcter.Movement.performed += context => Input = context.ReadValue<Vector2>();
        controll.Charcter.Movement.canceled += context => Input = Vector2.zero;

        controll.Charcter.Aim.performed += context => aim = context.ReadValue<Vector2>();
        controll.Charcter.Aim.canceled += context => aim = Vector2.zero;

        controll.Charcter.Run.performed+= context =>
        {
            if(moveInput.magnitude > 0){
            speed = runspeed;
            IsRunning = true;}
        } ; 
        controll.Charcter.Run.canceled += context =>
        {
            speed = walkspeed;
            IsRunning = false;
        };

         
    }
}   
