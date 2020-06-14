using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugPlayerMovement : MonoBehaviour
{

    [HideInInspector] public GameObject MAIN_CAMERA; 
    private CharacterController controller; 
    public float speed = 12f;
    public bool Srotate;
    public bool Smove;
    void Start()
    {
        MAIN_CAMERA = GameObject.FindGameObjectWithTag("MainCamera");
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        if (Smove)
        {
            Vector3 move = MoveMaths(0, z);
            controller.Move(move * speed * Time.deltaTime);
        }
        if(Srotate)
            RotateMaths(x);

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
    void RotateMaths(float x)
    {
        transform.rotation = Quaternion.Euler(new Vector3(0, x * Time.deltaTime * speed * 10, 0) + transform.rotation.eulerAngles);
    }
}
