using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerGenerate : MonoBehaviour
{
    public PlayerScriptable player;
    public Vector3 playerPosition = Vector3.zero;

    private GameObject character;
    private GameObject camera_player;

    void Awake()
    {
        Generate();
    }

    void Generate()
    {
        character = playerPosition == Vector3.zero ? Instantiate(player.CharacterModel, transform.position, Quaternion.identity) as GameObject
            : Instantiate(player.CharacterModel, playerPosition, Quaternion.identity) as GameObject;
        camera_player = Instantiate(player.CAMERA_FREELOOK, transform.position, Quaternion.identity) as GameObject;

        SetupCamera(camera_player, character);

        GameObject model = character.transform.Find("Armature").gameObject;

        GameObject empty = new GameObject("GroundChecker");
        empty.transform.parent = character.transform;
        empty.transform.position = playerPosition;
        empty.tag = "GroundChecker";

        ScriptManager(model, player);
        
        ChangeCharacterControllerSize(character);

        character.name = player.CharacterName;
        character.tag = "Player";
        camera_player.tag = "CAMERA_FREELOOK";

        character.GetComponent<PlayerMovements>().groundMask.value = 1 << 8;
        character.GetComponent<PlayerChange>().gender = player.Gender;

        character.GetComponent<Animator>().runtimeAnimatorController = player.Anim;
        character.GetComponent<Animator>().avatar = player.Avatar;

        LayerManager();
        GenerateUI();

        DEBUG();
    }

    void DEBUG()
    {
        character.AddComponent<DebugActionPlayer>();
    }

    void ChangeBoxSize(GameObject character)
    {
        BoxCollider BC = character.GetComponent<BoxCollider>();
        BC.size = new Vector3(0.7f, 1.05f, 0.6f);
        BC.center = new Vector3(0, 0.52f, 0);
    }

    void ScriptManager(GameObject model, PlayerScriptable player)
    {
        character.AddComponent<jsonReader>().fileN = player.json;

        character.AddComponent<CharacterController>();
        character.AddComponent<PlayerMovements>();
        character.AddComponent<UseCapacities>();
        character.AddComponent<PlayerChange>();
        character.AddComponent<RotateUsingCamera>();
        character.AddComponent<GetAllTurtleArena>();


        System.Type MyScriptType = System.Type.GetType(player.Capacities + ",Assembly-CSharp");
        character.AddComponent(MyScriptType);
    }

    void LayerManager()
    {
        character.layer = LayerMask.NameToLayer("Player");
    }

    void ChangeCharacterControllerSize(GameObject character)
    {
        CharacterController CC = character.GetComponent<CharacterController>();
        CC.height = 1.05f;
        CC.radius = 0.4f;
        CC.center = new Vector3(0, 0.52f, 0);
    }

    void SetupCamera(GameObject camera_player, GameObject character)
    {
        CinemachineFreeLook CFL = camera_player.GetComponent<CinemachineFreeLook>();
        CFL.Follow = character.transform;
        CFL.LookAt = character.transform;
    }

    public void SavePosition()
    {
        playerPosition = character.transform.position;
    }
    public void Summon()
    {
        SceneManager.LoadScene(1, LoadSceneMode.Additive);
        character.GetComponent<PlayerMovements>().enabled = false;
    }

    public void DestroyOldGenerate()
    {
        Destroy(camera_player);
        Destroy(character); 
        Generate();
        Debug.Log("generate...");
    }

    public void GenerateUI()
    {
        character.AddComponent<PlayerStats>().icon = player.icon;
        GameObject UI = Instantiate(player.UI) as GameObject;
        UI.GetComponent<UImanager>().player = character;
    }
}
