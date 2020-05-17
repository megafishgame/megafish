using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
public class UseCapacities : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            gameObject.SendMessage("Capacity1");
        }
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            gameObject.SendMessage("Capacity2");
        }
    }
}
