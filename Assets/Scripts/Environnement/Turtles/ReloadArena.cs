using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReloadArena : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            if(!GetComponent<TurtleArena>().isIn && GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovements>().lastArena == gameObject)
            {
                for (int i = 0; i < transform.childCount; i++)
                {
                    Transform child = transform.GetChild(i);
                    if (child.CompareTag("Turtle") || child.CompareTag("Crystal"))
                    {
                        child.GetComponent<TurtleMovement>().Reset();
                    }
                }
            }
        }
    }
}
