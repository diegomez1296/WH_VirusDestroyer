﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveController : MonoBehaviour
{
    //https://www.rapidtables.com/convert/number/ascii-to-hex.html
    public static SaveController Instance;
    public static int saveNumber;
    public static string saveNickName;

    public PlayerController player;

    private Dictionary<string, string> Prefs = new Dictionary<string, string>();
    private readonly string[] saveFilenames = { "48h50", "48h50h5fh4dh61h78", "4ch65h76h65h6c", "45h78h70",
                                        "45h78h70h54h6fh4eh65h78h74h4ch76h6c", "42h6ch61h63h6bh43h72h69h73h74h61h6ch73",
                                        "57h70h5fh30", "57h70h5fh31", "57h70h5fh32", "57h70h5fh33",
                                        "41h6dh6dh6fh42h6fh78h5fh30", "41h6dh6dh6fh42h6fh78h5fh31", "41h6dh6dh6fh42h6fh78h5fh32", "41h6dh6dh6fh42h6fh78h5fh33"};

    private void Start()
    {
        Debug.Log("F5 -> Save");

        if (Instance == null)
            Instance = FindObjectOfType<SaveController>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F5) && GameController.IsInputEnable)
            SavePrefs();

        if (Input.GetKeyDown(KeyCode.F9) && GameController.IsInputEnable)
            LoadPrefs();
    }

    public void SavePrefs()
    {
        Debug.Log("SavePrefs()");

        Prefs.Clear();

        Prefs.Add("HP", player.PlayerStats.HP +"");
        Prefs.Add("HP_Max", player.PlayerStats.HP_Max +"");
        Prefs.Add("Level", player.PlayerStats.Level +"");
        Prefs.Add("Exp", player.PlayerStats.Exp +"");
        Prefs.Add("ExpToNextLvl", player.PlayerStats.ExpToNextLvl +"");

        Prefs.Add("BlackCristals", player.PlayerStats.BlackCristals + "");

        Prefs.Add("Weapon_0", player.GetFullWeaponInfo(0));
        Prefs.Add("Weapon_1", player.GetFullWeaponInfo(1));
        Prefs.Add("Weapon_2", player.GetFullWeaponInfo(2));
        Prefs.Add("Weapon_3", player.GetFullWeaponInfo(3));

        Prefs.Add("AmmoBoxSupply_0", player.PlayerStats.GetAmmoBoxSupplyInfo(0));
        Prefs.Add("AmmoBoxSupply_1", player.PlayerStats.GetAmmoBoxSupplyInfo(1));
        Prefs.Add("AmmoBoxSupply_2", player.PlayerStats.GetAmmoBoxSupplyInfo(2));
        Prefs.Add("AmmoBoxSupply_3", player.PlayerStats.GetAmmoBoxSupplyInfo(3));

        Dictionary<string, string>.ValueCollection valueColl = Prefs.Values;

        int i = 0;
        foreach (string value in valueColl)
        {
            SaveToFile(saveFilenames[i], value);
            i++;
        }
    }
    public void SaveDefaultPrefs()
    {
        Debug.Log("SaveDefaultPrefs()");

        Prefs.Clear();

        Prefs.Add("HP", 100 + "");
        Prefs.Add("HP_Max", 100 + "");
        Prefs.Add("Level", 0 + "");
        Prefs.Add("Exp", 0 + "");
        Prefs.Add("ExpToNextLvl", 500 + "");

        Prefs.Add("BlackCristals", 0 + "");

        Prefs.Add("Weapon_0", "null");
        Prefs.Add("Weapon_1", "null");
        Prefs.Add("Weapon_2", "null");
        Prefs.Add("Weapon_3", "null");

        Prefs.Add("AmmoBoxSupply_0", "0");
        Prefs.Add("AmmoBoxSupply_1", "0");
        Prefs.Add("AmmoBoxSupply_2", "0");
        Prefs.Add("AmmoBoxSupply_3", "0");

        Dictionary<string, string>.ValueCollection valueColl = Prefs.Values;

        int i = 0;
        foreach (string value in valueColl)
        {
            SaveToFile(saveFilenames[i], value);
            i++;
        }
    }

    public void LoadPrefs()
    {
        Prefs.Clear();

        Prefs.Add("HP", ReadFromFile(saveFilenames[0]));
        Prefs.Add("HP_Max", ReadFromFile(saveFilenames[1]));
        Prefs.Add("Level", ReadFromFile(saveFilenames[2]));
        Prefs.Add("Exp", ReadFromFile(saveFilenames[3]));
        Prefs.Add("ExpToNextLvl", ReadFromFile(saveFilenames[4]));

        Prefs.Add("BlackCristals", ReadFromFile(saveFilenames[5]));

        Prefs.Add("Weapon_0", ReadFromFile(saveFilenames[6]));
        Prefs.Add("Weapon_1", ReadFromFile(saveFilenames[7]));
        Prefs.Add("Weapon_2", ReadFromFile(saveFilenames[8]));
        Prefs.Add("Weapon_3", ReadFromFile(saveFilenames[9]));

        Prefs.Add("AmmoBoxSupply_0", ReadFromFile(saveFilenames[10]));
        Prefs.Add("AmmoBoxSupply_1", ReadFromFile(saveFilenames[11]));
        Prefs.Add("AmmoBoxSupply_2", ReadFromFile(saveFilenames[12]));
        Prefs.Add("AmmoBoxSupply_3", ReadFromFile(saveFilenames[13]));

        SetPlayerPrefs();
    }

    private void SetPlayerPrefs()
    {
        player.ChangeAttribute(PlayerAttributes.HP, float.Parse(Prefs["HP"]));
        player.ChangeAttribute(PlayerAttributes.HP_MAX, float.Parse(Prefs["HP_Max"]));
        player.ChangeAttribute(PlayerAttributes.LEVEL, float.Parse(Prefs["Level"]));
        player.ChangeAttribute(PlayerAttributes.EXP, float.Parse(Prefs["Exp"]));
        player.ChangeAttribute(PlayerAttributes.EXP_TO_NEXT_LVL, float.Parse(Prefs["ExpToNextLvl"]));

        player.ChangeItemAmount(InventoryItems.BLACK_CRISTALS, int.Parse(Prefs["BlackCristals"]));

        player.PlayerStats.AmmoBoxSupply[0] = int.Parse(Prefs["AmmoBoxSupply_0"]);
        player.PlayerStats.AmmoBoxSupply[1] = int.Parse(Prefs["AmmoBoxSupply_1"]);
        player.PlayerStats.AmmoBoxSupply[2] = int.Parse(Prefs["AmmoBoxSupply_2"]);
        player.PlayerStats.AmmoBoxSupply[3] = int.Parse(Prefs["AmmoBoxSupply_3"]);

        player.SetFullWeapon(0,Prefs["Weapon_0"] != "null" ? Prefs["Weapon_0"] : "null"); 
        player.SetFullWeapon(1,Prefs["Weapon_1"] != "null" ? Prefs["Weapon_1"] : "null"); 
        player.SetFullWeapon(2,Prefs["Weapon_2"] != "null" ? Prefs["Weapon_2"] : "null"); 
        player.SetFullWeapon(3,Prefs["Weapon_3"] != "null" ? Prefs["Weapon_3"] : "null");
    }

    private void SaveToFile(string filename, string value)
    {
        string path = CONSTANS.SAVES_PATH + "Save_" + saveNumber + "©" + saveNickName;
        //FileInfo file = new FileInfo(path);
        if (!Directory.Exists(path))
            Directory.CreateDirectory(path);

        using (BinaryWriter binaryWriter = new BinaryWriter(new FileStream(path + "/" + filename + ".whvd", FileMode.Create)))
        {
            try
            {
                binaryWriter.Write(value);
            }
            catch (IOException e)
            {
                Debug.LogError("value: " + value);
                Debug.LogError(e.Message);
            }
        }
    }

    private string ReadFromFile(string filename)
    {
        string path = CONSTANS.SAVES_PATH + "Save_" + saveNumber + "©" + saveNickName;
        using (BinaryReader binaryReader = new BinaryReader(new FileStream(path + "/" + filename + ".whvd", FileMode.Open)))
        {
            try
            {
                return binaryReader.ReadString();

            }
            catch (IOException e)
            {
                Debug.LogError(e.Message);
                return "null";
            }
        }
    }

    public void DeleteFile()
    {
        
        string path = CONSTANS.SAVES_PATH + "Save_" + saveNumber + "©" + saveNickName;
        //FileInfo file = new FileInfo(path);
        Debug.LogError("Delete: " + path);
        if (Directory.Exists(path))
            Directory.Delete(path, true);
    }
}
