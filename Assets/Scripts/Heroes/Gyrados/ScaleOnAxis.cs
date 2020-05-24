using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleOnAxis : MonoBehaviour
{
    [Range(0, 100)]
    public float scaleFactor = 1;
    private Vector3 initialScale;
    public bool[] axis = new bool[3];

    private void Awake()
    {
        initialScale = transform.localScale;
    }
    private void FixedUpdate()
    {
        Vector3 scale = initialScale * scaleFactor;

        for (int i = 0; i < 3; i++)
        {
            if (!axis[i])
                scale[i] = transform.localScale[i];
        }

        transform.localScale = scale;
    }
}
