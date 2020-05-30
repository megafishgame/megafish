using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gradiant : MonoBehaviour
{
    public int axis;
    public Color tip;
    public Color init;
    void Start()
    {
        var mesh = GetComponent<MeshFilter>().mesh;
        var uv = mesh.uv;
        var colors = new Color[uv.Length];

        // Instead if vertex.y we use uv.x
        for (var i = 0; i < uv.Length; i++)
            colors[i] = Color.Lerp(init, tip, uv[i][axis]);

        mesh.colors = colors;
    }
}
