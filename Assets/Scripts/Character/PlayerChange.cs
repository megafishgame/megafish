using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerChange : MonoBehaviour
{
    public EnumType.GenderPlayer gender;
    public bool regenerate = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            GameObject.FindGameObjectWithTag("Respawn").GetComponent<PlayerGenerate>().SavePosition();
            GameObject.FindGameObjectWithTag("Respawn").GetComponent<PlayerGenerate>().Summon();
            regenerate = true;
        }
        if (regenerate)
        {
            GameObject newPlayer = GameObject.FindGameObjectWithTag("NewPlayer");
            if (newPlayer != null && newPlayer.GetComponent<TypeHolder>().respawn)
            {
                Destroy(newPlayer);
                GameObject.FindGameObjectWithTag("Respawn").GetComponent<PlayerGenerate>().player = 
                    newPlayer.GetComponent<TypeHolder>().holder;
                GameObject.FindGameObjectWithTag("Respawn").GetComponent<PlayerGenerate>().DestroyOldGenerate();
                regenerate = false;
            }
        }
    }
}
