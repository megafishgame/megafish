﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodeOnCollision : MonoBehaviour
{
    public GameObject particles;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Player"))
            return;
        if(collision.transform.CompareTag("Enemie"))
            collision.transform.GetComponent<EnemieScriptCapacity>().TakeDamage(50);
        Instantiate(particles, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
