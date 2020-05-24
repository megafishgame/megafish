using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetAllTurtleArena : MonoBehaviour
{
    private GameObject[] turtles;
    public bool[] turtlesIsIn;
    private void Awake()
    {
        turtles = GameObject.FindGameObjectsWithTag("TurtleArena");
        turtlesIsIn = new bool[turtles.Length];
    }
    private void FixedUpdate()
    {
        for (int i = 0; i < turtles.Length; i++)
        {
            turtlesIsIn[i] = turtles[i].GetComponent<TurtleArena>().isIn;
        }
    }
}
