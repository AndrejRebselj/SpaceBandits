using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Weapon", menuName = "ScriptableObjects/Weapons", order = 1)]
public class WeaponInfo : ScriptableObject
{
    public int WeaponType;
    public int Damage;
}
