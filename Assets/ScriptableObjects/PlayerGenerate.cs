using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEditor;
using UnityEngine;

public class PlayerGenerate : MonoBehaviour
{
    public PlayerScriptable player;
    void Start()
    {
        GameObject character = Instantiate(player.CharacterModel, transform.position, Quaternion.identity) as GameObject;
        GameObject camera = Instantiate(player.CAMERA_FREELOOK, transform.position, Quaternion.identity) as GameObject;

        character.AddComponent<CharacterController>();
        character.AddComponent<PlayerMovements>();
        character.AddComponent<UseCapacities>();
        character.AddComponent(player.Capacities.GetClass());

        ChangeCharacterControllerSize(character);
        SetupCamera(camera, character);

        character.name = player.CharacterName;
        character.tag = "Player";
        camera.tag = "CAMERA_FREELOOK";

    }

    void ChangeBoxSize(GameObject character)
    {
        BoxCollider BC = character.GetComponent<BoxCollider>();
        BC.size = new Vector3(0.7f, 1.05f, 0.6f);
        BC.center = new Vector3(0, 0.52f, 0);
    }
    void ChangeCharacterControllerSize(GameObject character)
    {
        CharacterController CC = character.GetComponent<CharacterController>();
        CC.height = 1.05f;
        CC.radius = 0.4f;
        CC.center = new Vector3(0, 0.52f, 0);
    }

    void SetupCamera(GameObject camera, GameObject character)
    {
        CinemachineFreeLook CFL = camera.GetComponent<CinemachineFreeLook>();
        CFL.Follow = character.transform;
        CFL.LookAt = character.transform;
        character.GetComponent<PlayerMovements>().CAMERA_FREELOOK = camera;
    }
}
