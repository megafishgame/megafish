using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sinusoid : MonoBehaviour
{
    public bool cos;
    public bool sin;
    public float factor;
    public bool[] axis = new bool[3];
    public Vector3 init;
    public float theta;
    public float timeFactor = 1;

    public void Awake()
    {
        init = transform.localPosition;
    }
    public void FixedUpdate()
    {
        Vector3 position = Vector3.zero;
        for (int i = 0; i < 3; i++)
        {
            if (axis[i])
            {
                if (cos)
                {
                    position[i] = init[i] + factor * Mathf.Cos(theta);
                }
                if (sin)
                {
                    position[i] = init[i] + factor * Mathf.Sin(theta);
                }
            }
            else
                position[i] = init[i];
        }
        transform.localPosition = position;
        theta += Time.deltaTime * timeFactor;
    }
}
