using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionPlayer : MonoBehaviour
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
    public void TakeDamage(int dmg = 25)
    {
        playerStats.currentHP -= dmg;
        playerStats.UpdateHealth();
        UI.Shake(UI.playerIcon, 0.5f, 3);
        UI.Lerp();
    }
    public void TakeStamina(int stamina = 25)
    {
        playerStats.currentStamina -= stamina;
        playerStats.UpdateStamina();
    }
    public void GainXP(int xp = 25)
    {
        playerStats.currentXP += xp;
        playerStats.UpdateXP();
    }
    void Summoned()
    {
        playerStats.hasBeenSummoned = !playerStats.hasBeenSummoned;
        playerStats.UpdateSummoned();
    }
}
