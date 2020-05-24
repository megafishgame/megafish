using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Security.Cryptography;

public class ExplosionTorusJump : MonoBehaviour
{
    private void Awake()
    {
        DOTween.Play(Jump());
    }
    Sequence Jump()
    {
        Sequence s = DOTween.Sequence();
        s.Append(transform.DOJump(transform.position, 0.75f, 1, 0.75f));
        return s;
    }
}
