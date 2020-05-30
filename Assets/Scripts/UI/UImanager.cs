using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.UI;

public class UImanager : MonoBehaviour
{
    [Header("Player")]
    public GameObject player;
    private PlayerStats playerInfo;
    private GameObject playerUnity;

    private float lifeMax;
    private float staminaMax;

    [Header("Colors")]
    public Color init;
    public Color end;


    [Header("LocalObject")]
    public GameObject life;
    public GameObject stamina;
    public GameObject xp;
    public GameObject type;
    public GameObject playerIcon;
    public GameObject level;
    public GameObject whiteRing;

    private List<Color> colors = new List<Color>(new Color[] {
    new Color32(230, 126, 34, 255),
    new Color32(52, 152, 219, 255),
    new Color32(155, 89, 182, 255),
    new Color32(22, 160, 133, 255),
    });

    void Start()
    {
        playerUnity = GameObject.FindGameObjectWithTag("Player");
        playerInfo = player.GetComponent<PlayerStats>();
        lifeMax = playerInfo.HPMax;
        staminaMax = playerInfo.staminaMax;
        SetupHexagon();
        UpdateIcon(playerIcon, playerInfo.icon);
        Lerp();
    }

    void Update()
    {
        SetFillAmount(life, playerInfo.currentHP / lifeMax);
        SetFillAmount(stamina, playerInfo.currentStamina / staminaMax);
        SetFillAmount(xp, (float)playerInfo.currentXP / playerInfo.xpMax);
        UpdateLevel(level, playerInfo.level); 
    }

    void SetupHexagon()
    {
        EnumType.GenderPlayer gender = playerUnity.GetComponent<PlayerChange>().gender;
        type.GetComponent<Image>().color = colors[(int)gender];
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
    public void Shake(GameObject target, float time, float strength)
    {
        if (target.transform.localScale == Vector3.one && target.transform.position == new Vector3(108 / 2, 180 / 2, 0))
        {
            float hp = (float)player.GetComponent<PlayerStats>().currentHP / (float)player.GetComponent<PlayerStats>().HPMax;
            target.transform.DOShakeScale(time, strength / 10, 1);
            strength = strength + Mathf.Abs(hp - 1) * 10;
            target.transform.DOShakePosition(time, strength);
        }
        else
        {
            target.transform.localScale = Vector3.one;
            target.transform.position = new Vector3(108 / 2, 180 / 2, 0);
        }
    }
    public void Lerp()
    {
        float hp = (float)player.GetComponent<PlayerStats>().currentHP / (float)player.GetComponent<PlayerStats>().HPMax;
        whiteRing.GetComponent<Image>().color = Color.Lerp(init, end, hp);
    }
}
