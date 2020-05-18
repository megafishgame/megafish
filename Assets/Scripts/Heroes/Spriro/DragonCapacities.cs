using UnityEngine;

public class DragonCapacities : ScriptCapacities
{
    public override void Capacity1()
    {
        Debug.Log("Capacity 1 trigger, override");
    }
    public override void Capacity2()
    {
        Debug.Log("Capacity 2 trigger");
    }
}