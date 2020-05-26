using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New  Player", menuName ="Player")]
public class PlayerScriptable : ScriptableObject
{
    public GameObject CAMERA_FREELOOK;
    public GameObject UI;
    public Sprite icon;
    public Avatar Avatar;
    public string CharacterName;
    public GameObject CharacterModel;
    public RuntimeAnimatorController Anim;

    public EnumType.GenderPlayer Gender;
    public EnumJSON json;
    public EnumScriptName.ScriptName Capacities;
}
