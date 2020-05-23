using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class AnimationGenerate : MonoBehaviour
{
    public List<PlayerScriptable> players = new List<PlayerScriptable>();
    public int index;
    public GameObject canvas;

    private GameObject character;
    PlayerScriptable player;

    private float actualTime = 2;
    private bool activateTimer = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
            canvas.SetActive(!canvas.activeSelf);
        if (Input.GetKeyDown(KeyCode.Return) && canvas.activeSelf)
            UseNumpad();
        if (activateTimer)
            Timer();
    }


    void Anim()
    {
        character = Instantiate(player.CharacterModel, transform.position, Quaternion.identity) as GameObject;
        character.AddComponent<TypeHolder>().holder = player;
        character.tag = "NewPlayer";
        activateTimer = true;
        //play some animation
    }
    
    // Use Numpad or Nfc
    void UseNumpad()
    {
        string text = canvas.transform.GetChild(0).transform.Find("Text").GetComponent<Text>().text;
        int number = 0;
        Int32.TryParse(text, out number);
        CreateCharacter(number);
    }
    void UseNFC()
    {

    }
    void CreateCharacter(int number)
    {
        player = players[number % players.Count];
        Anim();
    }

    void Timer()
    {
        if (actualTime < 0)
        {
            GameObject.FindGameObjectWithTag("NewPlayer").GetComponent<TypeHolder>().respawn = true;
            SceneManager.UnloadSceneAsync("Summon");
        }
        else
            actualTime -= Time.deltaTime;
    }
}
