using DG.Tweening.Plugins.Core.PathCore;
using System;
using System.Collections.Generic;
using UnityEngine;

public class DragonCapacities : ScriptCapacities
{
    public GameObject WaterBall;
    public List<int> objectToLoad = new List<int>();
    public GameObject Camera;
    public const float power = 1000;

    private void Awake()
    {
        WaterBall = GameObject.FindGameObjectWithTag("Respawn").GetComponent<ObjectToLoad>().objects[0];
        Camera = GameObject.FindGameObjectWithTag("MainCamera");
    }

    public override void Capacity1()
    {
        Vector3 force = transform.forward;
        force = new Vector3(force.x,  2f * -Mathf.Sin(Mathf.Deg2Rad * Camera.transform.rotation.eulerAngles.x), force.z);
        force *= power;
        GameObject WaterBallCreated = Instantiate(WaterBall, transform.position + Vector3.up, Quaternion.identity) as GameObject;

        WaterBallCreated.GetComponent<Rigidbody>().AddForce(force);
    }
    public override void Capacity2()
    {
        Debug.Log("Capacity 2 trigger");
    }
}