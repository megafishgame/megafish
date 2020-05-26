﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemiesGenerator : MonoBehaviour
{
    public EnemiesScriptable enemie;
    private GameObject character;

    void Start()
    {
        character = Instantiate(enemie.EnemieModel, transform.position, Quaternion.identity) as GameObject;

        ManageComponents();
        DestroyThis();
    }
    void ManageComponents()
    {
        character.AddComponent<BoxCollider>();
        ChangeBoxSize(character);

        System.Type MyScriptType = System.Type.GetType(enemie.Capacities + ",Assembly-CSharp");
        character.AddComponent(MyScriptType);

        character.AddComponent<NavMeshAgent>().height = 1;
    }
    void ChangeBoxSize(GameObject character)
    {
        BoxCollider BC = character.GetComponent<BoxCollider>();
        BC.size = new Vector3(0.7f, 1.05f, 0.6f);
        BC.center = new Vector3(0, 0.52f, 0);
    }
    void DestroyThis()
    {
        Destroy(gameObject);
    }
}
