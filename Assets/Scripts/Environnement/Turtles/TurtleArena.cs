using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TurtleArena : MonoBehaviour
{
    public Vector3 offset;
    public Vector3 size;
    public LayerMask LayerMask;
    public bool isIn;
    private List<GameObject> children = new List<GameObject>();
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawCube(gameObject.transform.position + offset, size);
    }
    private void Start()
    {
        int childCount = gameObject.transform.childCount;
        for (int i = 0; i < childCount; i++)
        {
            children.Add(gameObject.transform.GetChild(i).gameObject);
        }
    }
    private void FixedUpdate()
    {
        isIn = Physics.CheckBox(gameObject.transform.position + offset, size / 2, Quaternion.identity, LayerMask);
        if (isIn)
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovements>().lastArena = gameObject;
        Collider[] colliders = Physics.OverlapBox(gameObject.transform.position + offset, size / 2);

        int childCount = gameObject.transform.childCount;
        for (int i = 0; i < childCount; i++)
        {
            children.Add(gameObject.transform.GetChild(i).gameObject);
        }

        foreach (var collider in colliders)
        {
            if (children.Contains(collider.gameObject))
                children.Remove(collider.gameObject);
        }
        if(children.Count != 0)
            children[0].GetComponent<TurtleMovement>().Reset();
        children = new List<GameObject>();
    }
}
