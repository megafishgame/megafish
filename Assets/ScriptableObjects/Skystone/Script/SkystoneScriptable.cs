using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New  Skystone", menuName = "Skystone")]
public class SkystoneScriptable : ScriptableObject
{
    public Sprite icon;
    [Tooltip("up, right, down, left")] public int[] Attack = new int[4];
}
