using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Result : MonoBehaviour
{
    public bool hasWinLast;
    public void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

}
