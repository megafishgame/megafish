using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GenerateTable : MonoBehaviour
{
    public GameObject holder;
    public GameObject IA;
    public GameObject[,] table;
    public int size = 3;
    public float offset;
    void Start()
    {
        table = new GameObject[size, size];
        Generate();
    }
    void Generate()
    {
        Vector3 pos = transform.position;
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                GameObject stoneHolder = Instantiate(holder, pos, Quaternion.identity);
                table[i, j] = stoneHolder;
                stoneHolder.transform.parent = gameObject.transform;
                pos.x += offset;
            }
            pos.z -= offset;
            pos.x = transform.position.x;
        }
    }
}
