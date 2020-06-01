using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Container : MonoBehaviour
{
    [HideInInspector] public bool newEntry;
    public bool isEmpty;
    public GameObject stone;
    public GameObject skystone;
    public void Generate()
    {
        skystone = Instantiate(stone, transform.position + new Vector3(0, 0.25f, 0), Quaternion.identity) as GameObject;
    }
    public void Reshape(int j, int i, int size)
    {
        skystone.GetComponent<SkystoneBehaviour>().Reshape(i, j, size);
    }

    public void Attack(int j, int i, int size)
    {
        skystone.GetComponent<SkystoneBehaviour>().AttackTimer(i, j, size);
    }
}
