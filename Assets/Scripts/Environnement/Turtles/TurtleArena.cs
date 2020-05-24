using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurtleArena : MonoBehaviour
{
    public Vector3 offset;
    public Vector3 size;
    public LayerMask LayerMask;
    public bool isIn;
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawCube(gameObject.transform.position + offset, size);
    }
    private void FixedUpdate()
    {
        isIn = Physics.CheckBox(gameObject.transform.position + offset, size / 2, Quaternion.identity, LayerMask);
        if (isIn)
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovements>().lastArena = gameObject;
    }
}
