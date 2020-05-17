using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovements : MonoBehaviour
{
    [HideInInspector] public GameObject CAMERA_FREELOOK;
    [HideInInspector] public GameObject MAIN_CAMERA;
    public Vector3 DEBUG_CAMERA;
    public Vector3 DEBUG_AXIS;

    private CharacterController controller;

    private const float gravity = -9.81f;

    public float speed = 12f;
    public float jumpHeight = 1.5f;

    Vector3 velocity;

    void Start()
    {
        MAIN_CAMERA = GameObject.FindGameObjectWithTag("MainCamera");
        controller = GetComponent<CharacterController>();   
    }

    // Update is called once per frame
    void Update()
    {
        if (controller.isGrounded)
            velocity.y = 0f;
        if (Input.GetKeyDown(KeyCode.Space))
            Jump();

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        DEBUG_CAMERA = MAIN_CAMERA.transform.rotation.eulerAngles;
        DEBUG_AXIS = new Vector3(Mathf.Cos(MAIN_CAMERA.transform.rotation.eulerAngles.y * Mathf.Deg2Rad), 0, Mathf.Sin(MAIN_CAMERA.transform.rotation.eulerAngles.y * Mathf.Deg2Rad));

        Vector3 moveZ =
            transform.right * z * Mathf.Sin(MAIN_CAMERA.transform.rotation.eulerAngles.y * Mathf.Deg2Rad) +
            transform.forward * z * Mathf.Cos(MAIN_CAMERA.transform.rotation.eulerAngles.y * Mathf.Deg2Rad);

        Vector3 moveX =
            transform.right * x * Mathf.Cos(MAIN_CAMERA.transform.rotation.eulerAngles.y * Mathf.Deg2Rad) +
            transform.forward * x * Mathf.Sin(MAIN_CAMERA.transform.rotation.eulerAngles.y * Mathf.Deg2Rad)  * -1f;

        Vector3 move = moveX + moveZ;

        controller.Move(move * speed * Time.deltaTime);

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);
    }

    void Jump()
    {
        velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
    }

}
