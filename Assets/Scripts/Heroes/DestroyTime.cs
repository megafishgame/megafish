using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class DestroyTime : MonoBehaviour
{
    public float time; 
    public GameObject particles;
    public bool instantiate;
    void Awake()
    {
        StartCoroutine(Timer());
    }
    IEnumerator Timer()
    {
        yield return new WaitForSeconds(time);
        if(instantiate)
            Instantiate(particles, transform.position, Quaternion.identity);
        Destroy(gameObject);

    }
}
