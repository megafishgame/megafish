﻿using UnityEngine;

public class test_capacities : ScriptCapacities
{
    public override void Capacity1()
    {
        Debug.Log("Capacity 1 trigger, override");
    }
    public override void Capacity2()
    {
        GetComponent<PlayerMovements>().Jump(GetComponent<PlayerMovements>().jumpHeight * 3);
    }
}