using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Security.Cryptography;

public class ExplosionTorusJump : MonoBehaviour
{
    public float jump = 0.3f;
    public float time;
    private void Awake()
    {
        DOTween.Play(Jump());
    }
    Sequence Jump()
    {
        Sequence s = DOTween.Sequence();
        s.Append(transform.DOJump(transform.position - new Vector3(0, jump, 0), 0.75f, 1, time));
        return s;
    }
    public void Update()
    {
        Vector3 loc = transform.position;
        loc.x = GameObject.FindGameObjectWithTag("Player").transform.position.x;
        loc.z = GameObject.FindGameObjectWithTag("Player").transform.position.z;
        transform.position = loc;
    }
}
