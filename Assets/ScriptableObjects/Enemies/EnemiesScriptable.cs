using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New  Enemie", menuName = "Enemie")]
public class EnemiesScriptable : ScriptableObject
{
    public Avatar Avatar;
    public string EnemieName;
    public GameObject EnemieModel;
    public RuntimeAnimatorController Anim;
    public float lifeMax;
    public GameObject deathParticles;
    public EnumType.GenderPlayer Gender;
    public EnumScriptName.ScriptEnemiesName Capacities;
    public EnemieEnum.gender Type;
    public EnemieEnum.movements MovementsMode;

}
