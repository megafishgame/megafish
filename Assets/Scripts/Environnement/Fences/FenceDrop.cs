using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FenceDrop : MonoBehaviour
{
    public float Ymovement;
    public float time = 3;

    public bool drop;
    void Update()
    {
        if(drop)
        {
            drop = false;
            Drop();
        }
    }

    void Drop()
    {
        Ymovement += gameObject.transform.position.y;
        gameObject.transform.DOMoveY(Ymovement, time);
    }
}
