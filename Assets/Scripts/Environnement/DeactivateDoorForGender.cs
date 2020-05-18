using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeactivateDoorForGender : MonoBehaviour
{
    public GameObject player;
    public EnumType.GenderPlayer Gender;
    private float maxTime = 0.5f;
    private float time;

    void Update()
    {
        Timer();
    }

    void Activate()
    {
        gameObject.GetComponent<MeshCollider>().enabled = player.GetComponent<PlayerStats>().Gender != Gender;
        this.enabled = false;
    }

    void Timer()
    {
        if (time < 0)
        {
            player = GameObject.FindGameObjectWithTag("Player");
            if (player == null)
                time = maxTime;
            else
                Activate();
        }
        else
            time -= Time.deltaTime;
    }
}
