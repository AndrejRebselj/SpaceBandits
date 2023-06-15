using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabVariables
{
    public static int GoldCurrency;
    public static int GemCurrency;
    public static int PickedShip;
    public static int Weapon1Level;
    public static int Weapon2Level;
    public static Dictionary<int,string> ownedShips = new Dictionary<int, string>();
    public static Dictionary<int,string> ownedWeapons = new Dictionary<int, string>();

    public static void LoadValues() 
    {
        ownedShips[0] = PlayerPrefs.GetString(PlayerPrefsStrings.OwnedShip + 0, "y");
        ownedWeapons[0] = PlayerPrefs.GetString(PlayerPrefsStrings.OwnedWeapon + 0, "y");
        GoldCurrency = PlayerPrefs.GetInt(PlayerPrefsStrings.GoldCurrency,0);
        GemCurrency = PlayerPrefs.GetInt(PlayerPrefsStrings.GemCurrency,0);
        PickedShip = PlayerPrefs.GetInt(PlayerPrefsStrings.PickedShip,0);
        Weapon1Level = PlayerPrefs.GetInt(PlayerPrefsStrings.Weapon1Level,1);
        Weapon2Level = PlayerPrefs.GetInt(PlayerPrefsStrings.Weapon2Level,1);
        for (int i = 1; i < 2; i++)
        {
            ownedShips[i] = PlayerPrefs.GetString(PlayerPrefsStrings.OwnedShip+i,"n");
        }
        for (int i = 1; i < 2; i++)
        {
            ownedWeapons[i] = PlayerPrefs.GetString(PlayerPrefsStrings.OwnedWeapon + i,"n");
        }
    }

    public static void SaveValues() 
    {
        PlayerPrefs.SetInt(PlayerPrefsStrings.GoldCurrency, GoldCurrency);
        PlayerPrefs.SetInt(PlayerPrefsStrings.GemCurrency, GemCurrency);
        PlayerPrefs.SetInt(PlayerPrefsStrings.PickedShip, PickedShip);
        PlayerPrefs.SetInt(PlayerPrefsStrings.Weapon1Level, Weapon1Level);
        PlayerPrefs.SetInt(PlayerPrefsStrings.Weapon2Level, Weapon2Level);
        for (int i = 1; i < 2; i++)
        {
            PlayerPrefs.SetString(PlayerPrefsStrings.OwnedShip + i,ownedShips[i]);
        }
        for (int i = 1; i < 2; i++)
        {
            PlayerPrefs.SetString(PlayerPrefsStrings.OwnedWeapon + i,ownedWeapons[i]);
        }
    }
}

public struct PlayerPrefsStrings 
{
   public static string GoldCurrency = "GoldCurrency";
   public static string GemCurrency = "GemCurrency";
   public static string PickedShip = "PickedShip";
   public static string Weapon1Level = "Weapon1Level";
   public static string Weapon2Level = "Weapon2Level";
   public static string OwnedShip = "OwnedShip";
   public static string OwnedWeapon = "OwnedWeapon";
}
