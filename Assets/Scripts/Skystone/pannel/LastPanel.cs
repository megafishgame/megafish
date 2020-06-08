using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LastPanel : MonoBehaviour
{
    public int lastPanel;
    public Vector3 position;
    public void Start()
    {
        DontDestroyOnLoad(gameObject);
    }
}

