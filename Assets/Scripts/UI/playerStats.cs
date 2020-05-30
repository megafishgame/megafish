using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    private jsonReader json;
    [Header("properties")]
    public Sprite icon;
    public new string name;
    public int level;
    public int initialHP;
    public int initialStamina;
    public int initialXP;
    public int HPBuff;
    public int staminaBuff;
    public int xpBuff;
    public int currentXP;
    public int currentHP;
    public int currentStamina;
    public bool isDead;
    public bool hasBeenSummoned;
    public bool debug;

    public int HPMax;
    public int staminaMax;
    public int xpMax;
    //[Header("functions")]
    private void Awake()
    {
        json = GetComponent<jsonReader>();
    }
    private void Start()
    {
        UpdateMax();

        if (!hasBeenSummoned)
        {
            currentHP = HPMax;
            currentStamina = staminaMax;
            UpdateHealth();
            UpdateStamina();
        }
    }
    public void UpdateMax()
    {
        HPMax = initialHP + HPBuff * level;
        staminaMax = initialStamina + staminaMax * level;
        xpMax = initialXP + xpMax * level;
    }

    public void UpdateHealth()
    {
        json.UpdateHealth();
    }
    public void UpdateStamina()
    {
        json.UpdateStamina();
    }
    public void UpdateXP()
    {
        if(currentXP > xpMax)
        {
            LevelUp();
        }
        json.UpdateXP();
    }
    public void LevelUp()
    {
        currentXP = 0;
        level += 1;
        UpdateMax();
        json.UpdateLevel();
    }

    public void UpdateSummoned()
    {
        json.UpdateSummoned();
    }

    private void OnApplicationQuit()
    {
        UpdateHealth();
        UpdateStamina(); 
        UpdateSummoned();
    }
}
