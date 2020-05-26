using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test_capacities_enemies : EnemieScriptCapacity
{
    public override void Capacity1()
    {
        Debug.Log("Capacity 1 trigger, override");
    }
    public override void Capacity2()
    {
        Debug.Log("Capacity 2 trigger, override");
    }
}
