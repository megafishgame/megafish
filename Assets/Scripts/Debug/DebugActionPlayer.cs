using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugActionPlayer : MonoBehaviour
{
    private PlayerStats playerStats;
    private UImanager UI;
    private void Start()
    {
        playerStats = GetComponent<PlayerStats>();
        UI = GameObject.FindGameObjectWithTag("UI").GetComponent<UImanager>();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Keypad0))
        {
            Summoned();
        }

        if (Input.GetKeyDown(KeyCode.Keypad1))
        {
            TakeDamage();
        }
        if (Input.GetKeyDown(KeyCode.Keypad2))
        {
            TakeStamina();
        }
        if (Input.GetKeyDown(KeyCode.Keypad3))
        {
            GainXP();
        }
    }
    void TakeDamage()
    {
        playerStats.currentHP -= 25;
        playerStats.UpdateHealth();
        UI.Shake(UI.playerIcon, 0.5f, 3);
        UI.Lerp();
    }
    void TakeStamina()
    {
        playerStats.currentStamina -= 25;
        playerStats.UpdateStamina();
    }
    void GainXP()
    {
        playerStats.currentXP += 25;
        playerStats.UpdateXP();
    }
    void Summoned()
    {
        playerStats.hasBeenSummoned = !playerStats.hasBeenSummoned;
        playerStats.UpdateSummoned();
    }
}
