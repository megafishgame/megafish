using System.IO;
using UnityEngine;


public enum EnumJSON
{
    gyrados,
    hey
}


public class jsonReader : MonoBehaviour
{
    private string rootPath;
    private string fileLocation = @"Assets\JSON";
    private string path;
    public EnumJSON fileN;
    public JSONcharacter character;

    void Start()
    {
        string fileName = fileN.ToString();
        fileName += ".json";
        rootPath = Directory.GetCurrentDirectory();
        path = Path.Combine(rootPath, fileLocation, fileName);
        if (!File.Exists(path))
            Debug.LogError("Not a correct json file");
        else
        {
            string json = File.ReadAllText(path);
            character = JsonUtility.FromJson<JSONcharacter>(json);
            
            if (gameObject.GetComponent<playerStats>() == null)
                Debug.LogError("Need playerStats");
            else
            {
                playerStats PS = gameObject.GetComponent<playerStats>();
                PS.level = character.level;
                PS.name = character.name;
                PS.staminaBuff = character.staminaBuff;
                PS.currentXP = character.currentXP;
                PS.currentHP = character.currentHP;
                PS.currentStamina = character.currentStamina;
                PS.isDead = character.isDead;
                PS.hasBeenSummoned = character.hasBeenSummoned;
                PS.initialHP = character.initialHP;
                PS.initialStamina = character.initialStamina;
            }
        }
    }
    public void levelup()
    {
        playerStats PS = gameObject.GetComponent<playerStats>();
        character.level = PS.level;
        string json = JsonUtility.ToJson(character);
        System.IO.File.WriteAllText(path, json);
    }
    public void hasBeenSummoned()
    {
        playerStats PS = gameObject.GetComponent<playerStats>();
        character.hasBeenSummoned = PS.hasBeenSummoned;
        string json = JsonUtility.ToJson(character);
        System.IO.File.WriteAllText(path, json);
    }
}
public class JSONcharacter
{
    public string name;
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
}