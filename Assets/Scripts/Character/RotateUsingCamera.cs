using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateUsingCamera : MonoBehaviour
{
    [HideInInspector] public GameObject MAIN_CAMERA;
    public float timeReset = 0.25f;
    private float time;

    public Vector3 last = Vector3.zero;
    public Vector3 actual;
    public float offset = 0; //-90;

    void Start()
    {
        MAIN_CAMERA = GameObject.FindGameObjectWithTag("MainCamera");
    }
    void FixedUpdate()
    {
        Timer();

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        if (Mathf.Abs(x) >= 0.1f || Mathf.Abs(z) >= 0.1f)
        {
            transform.DORotateQuaternion(Quaternion.Euler(new Vector3(offset, MAIN_CAMERA.transform.rotation.eulerAngles.y, 0)), 0.2f);
            time = timeReset;
        }
        else
            Timer();
    }
    void Timer()
    {
        if (time < 0)
        {
            time = timeReset;
            Rotate();
        }
        else
            time -= Time.deltaTime;
    }

    void Rotate()
    {
        last = actual;
        actual = new Vector3(offset, MAIN_CAMERA.transform.rotation.eulerAngles.y, 0);
        float rotation = Vector3.Distance(last, actual);


         if (rotation < 20)
            transform.DORotateQuaternion(Quaternion.Euler(new Vector3(offset, MAIN_CAMERA.transform.rotation.eulerAngles.y, 0)), 0.5f);
        
    }
}
