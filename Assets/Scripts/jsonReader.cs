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
            
            if (gameObject.GetComponent<PlayerStats>() == null)
                Debug.LogError("Need playerStats");
            else
            {
                PlayerStats PS = gameObject.GetComponent<PlayerStats>();
                if (character.debug)
                {
                    PS.currentStamina = character.initialStamina;
                    PS.level = 0;
                    PS.currentXP = 0;
                    PS.currentHP = character.initialHP;
                }
                else
                {
                    PS.currentStamina = character.currentStamina;
                    PS.currentXP = character.currentXP;
                    PS.currentHP = character.currentHP;
                    PS.level = character.level;
                }
                PS.name = character.name;
                PS.staminaBuff = character.staminaBuff;
                PS.xpBuff = character.xpBuff;
                PS.isDead = character.isDead;
                PS.hasBeenSummoned = character.hasBeenSummoned;
                PS.initialHP = character.initialHP;
                PS.initialStamina = character.initialStamina;
                PS.initialXP = character.initialXP;
            }
        }
    }
    public void Write()
    {
        string json = JsonUtility.ToJson(character);
        System.IO.File.WriteAllText(path, json);
    }

    public void Levelup()
    {
        PlayerStats PS = gameObject.GetComponent<PlayerStats>(); // Because we need to re get it in case you switch character
        character.level = PS.level;
        Write();
    }

    public void UpdateHealth()
    {
        PlayerStats PS = gameObject.GetComponent<PlayerStats>();
        character.currentHP = PS.currentHP; 
        Write();
    }
    public void UpdateStamina()
    {
        PlayerStats PS = gameObject.GetComponent<PlayerStats>();
        character.currentStamina = PS.currentStamina; 
        Write();
    }
    public void UpdateXP()
    {
        PlayerStats PS = gameObject.GetComponent<PlayerStats>();
        character.currentXP = PS.currentXP;
        Write();
    }
    public void UpdateLevel()
    {
        PlayerStats PS = gameObject.GetComponent<PlayerStats>();
        character.level = PS.level;
        Write();
    }
    public void UpdateSummoned()
    {
        PlayerStats PS = gameObject.GetComponent<PlayerStats>();
        character.hasBeenSummoned = PS.hasBeenSummoned;
        Write();
    }
}
public class JSONcharacter
{
    public string name;
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
}