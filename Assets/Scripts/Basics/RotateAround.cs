using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAround : MonoBehaviour
{
    public float speed;
    private void FixedUpdate()
    {
        gameObject.transform.Rotate(Vector3.up, speed);
    }
}
