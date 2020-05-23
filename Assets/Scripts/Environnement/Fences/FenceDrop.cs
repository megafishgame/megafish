using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Cinemachine;
using System.Threading;

public class FenceDrop : MonoBehaviour
{
    public float Ymovement;
    public float time = 5;
    public float offset = -0.75f;
    private bool start = true;
    private float destroytime = 3;
    private float starttime = 0.5f;
    private CinemachineVirtualCamera localCamera;
    private GameObject cam;

    private bool startAnimation;

    public EnumType.GenderPlayer Gender;
    private bool first = true;
    private void Start()
    {
        gameObject.GetComponentInChildren<CinemachineVirtualCamera>().enabled = false;
        cam = GameObject.FindGameObjectWithTag("CAMERA_FREELOOK");
    }
    private void FixedUpdate()
    {
        if (!start)
        {
            if (first)
            {
                first = false;
                cam.GetComponent<CinemachineFreeLook>().m_XAxis.m_MaxSpeed /= 10; 
                cam.GetComponent<CinemachineFreeLook>().m_YAxis.m_MaxSpeed /= 10;
                GameObject.FindGameObjectWithTag("Player").GetComponent<RotateUsingCamera>().enabled = false;
            }

            Timer();
            StartTimer();
        }
    }


    void Drop()
    {
        Ymovement += gameObject.transform.position.y + offset;
        gameObject.transform.DOMoveY(Ymovement, time);
        localCamera = gameObject.GetComponentInChildren<CinemachineVirtualCamera>();
        localCamera.enabled = true;
        localCamera.transform.parent = null;
        cam = GameObject.FindGameObjectWithTag("CAMERA_FREELOOK");
        cam.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (start && other.transform.tag == "Player" && other.transform.GetComponent<PlayerStats>().Gender == Gender)
            start = false;
    }
    private void Timer()
    {
        if (destroytime < 0)
        {
            localCamera.enabled = false;
            cam.SetActive(true);
            cam.GetComponent<CinemachineFreeLook>().m_XAxis.m_MaxSpeed *= 10;
            cam.GetComponent<CinemachineFreeLook>().m_YAxis.m_MaxSpeed *= 10;
            GameObject.FindGameObjectWithTag("Player").GetComponent<RotateUsingCamera>().enabled = true;
            Destroy(this);
        }
        else
            destroytime -= Time.deltaTime;

    }
    private void StartTimer()
    {
        if (starttime < 0)
        {
            Drop();
            starttime = 999;
        }
        else
            starttime -= Time.deltaTime;

    }
}
