using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public EnumType.GenderPlayer Gender;

    void DoorsUpdate()
    {
        GameObject[] doors = GameObject.FindGameObjectsWithTag("Door");
        foreach (var door in doors)
        {
            door.GetComponent<DeactivateDoorForGender>().enabled = true;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            GameObject.FindGameObjectWithTag("Respawn").GetComponent<PlayerGenerate>().SavePosition();
            GameObject.FindGameObjectWithTag("Respawn").GetComponent<PlayerGenerate>().Regenerate();
            DoorsUpdate();
        }
    }
}
