using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeScale : MonoBehaviour
{
    private float value = 0;
    private float time = 0;
    public float maxValue = 5;
    private ScaleOnAxis SOA;
    private void Awake()
    {
        time = GetComponent<DestroyTime>().time - 0.1f;
        SOA = GetComponent<ScaleOnAxis>();
        DOTween.Play(Float());
    }
    private void FixedUpdate()
    {
        SOA.scaleFactor = value;
    }
    Sequence Float()
    {
        Sequence s = DOTween.Sequence();
        s.Append(DOTween.To(() => value, x => value = x, maxValue, time));
        return s;
    }
}
