using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UImanager : MonoBehaviour
{
    [Header("Player")]
    public GameObject player;
    private playerStats playerInfo;

    private float lifeMax;
    private float staminaMax;


    [Header("LocalObject")]
    public GameObject life;
    public GameObject stamina;
    public GameObject playerIcon;
    public GameObject level;


    void Start()
    {
        playerInfo = player.GetComponent<playerStats>();
        lifeMax = playerInfo.HPMax;
        staminaMax = playerInfo.staminaMax;
        UpdateIcon(playerIcon, playerInfo.icon);
    }

    void Update()
    {
        SetFillAmount(life, playerInfo.currentHP / lifeMax);
        SetFillAmount(stamina, playerInfo.currentStamina / staminaMax);
        UpdateLevel(level, playerInfo.level); 
    }

    void SetFillAmount(GameObject target, float value)
    {
        target.GetComponent<Image>().fillAmount = value;
    }
    void UpdateIcon(GameObject target, Sprite sprite)
    {
        target.GetComponent<Image>().sprite = sprite;
    }
    void UpdateLevel(GameObject target, int value)
    {
        target.GetComponent<Text>().text = value.ToString();
    }
}
