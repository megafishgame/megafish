using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName ="New  Player", menuName ="Player")]
public class PlayerScriptable : ScriptableObject
{
    public GameObject CAMERA_FREELOOK;
    public string CharacterName;
    public GameObject CharacterModel;
    public EnumType.GenderPlayer Gender;

    public MonoScript Capacities;
}
