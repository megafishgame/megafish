using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DissolveSinusoid : MonoBehaviour
{
    public Material dissolve;
    public float time;
    public float factor;
    public float value;
    void Update()
    { 
        time += Time.deltaTime * factor;
        value = Mathf.Abs(Mathf.Sin(time)) / 5 + 0.15f;
        dissolve.SetFloat("_SliceAmount", value);
    }
}
