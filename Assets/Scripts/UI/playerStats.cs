using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerStats : MonoBehaviour
{
    private jsonReader json;
    [Header("properties")]
    public Sprite icon;
    public new string name;
    public int level;
    public int initialHP;
    public int initialStamina;
    public int HPBuff;
    public int staminaBuff;
    public int currentXP;
    public int currentHP;
    public int currentStamina;
    public bool isDead;
    public bool hasBeenSummoned;

    public int HPMax;
    public int staminaMax;
    [Header("functions")]
    public bool levelUp;
    private void Awake()
    {
        json = GetComponent<jsonReader>();
    }
    private void Start()
    {
        hasBeenSummoned = true;
        json.hasBeenSummoned();
        HPMax = initialHP + HPBuff * level;
        staminaMax = initialStamina + staminaMax * level;
    }
    private void FixedUpdate()
    {
        if (levelUp)
        {
            levelUp = false;
            level += 1;
            json.levelup();
            HPMax = initialHP + HPBuff * level;
            staminaMax = initialStamina + staminaMax * level;
        }
    }
    private void OnApplicationQuit()
    {
        hasBeenSummoned = false;
        json.hasBeenSummoned();
    }
}
