using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovements : MonoBehaviour
{
    [HideInInspector] public GameObject MAIN_CAMERA;

    private CharacterController controller;

    private const float gravity = -9.81f;

    public float speed = 12f;
    public float jumpHeight = 1.5f;

    public Vector3 velocity;

    public Transform groundCheck;
    public float groundDistance = 0.4f;

    public LayerMask groundMask;
    public bool isGrounded;

    public Animator anim;


    void Start()
    {
        MAIN_CAMERA = GameObject.FindGameObjectWithTag("MainCamera");
        controller = GetComponent<CharacterController>();
        groundCheck = GameObject.FindGameObjectWithTag("GroundChecker").transform;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (groundCheck == null)
        {
            groundCheck = GameObject.FindGameObjectWithTag("GroundChecker").transform;
            return;
        }
        if (anim == null)
        {
            anim = GetComponent<Animator>();
            return;
        }
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
            velocity.y = -2f;

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = MoveMaths(x, z);
        if (move != Vector3.zero)
            anim.SetFloat("speed", Mathf.Min(anim.GetFloat("speed") + 2.25f * Time.deltaTime, 1f));
        else
            anim.SetFloat("speed", Mathf.Max(anim.GetFloat("speed") - 2.25f * Time.deltaTime, 0f));

        controller.Move(move * speed * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
            Jump(jumpHeight);


        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);

    
    }

    public void Jump(float jumpH)
    {
        velocity.y = Mathf.Sqrt(jumpH * -2f * gravity);
    }

    Vector3 MoveMaths(float x, float z)
    {
        Vector3 moveZ =
            transform.right * z * Mathf.Sin(MAIN_CAMERA.transform.rotation.eulerAngles.y * Mathf.Deg2Rad) +
            transform.forward * z * Mathf.Cos(MAIN_CAMERA.transform.rotation.eulerAngles.y * Mathf.Deg2Rad);

        Vector3 moveX =
            transform.right * x * Mathf.Cos(MAIN_CAMERA.transform.rotation.eulerAngles.y * Mathf.Deg2Rad) +
            transform.forward * x * Mathf.Sin(MAIN_CAMERA.transform.rotation.eulerAngles.y * Mathf.Deg2Rad) * -1f;

        return
            moveX + moveZ;
    }

    Vector3 MoveStatic(float x, float z)
    {
        return transform.right * x + transform.forward * z;
    }

}
