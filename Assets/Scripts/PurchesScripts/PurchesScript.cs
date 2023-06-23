using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PurchesScript : MonoBehaviour
{
    [SerializeField] int amount;
    public void GiveAmount() 
    {
        PrefabVariables.GemCurrency += amount;
    }
}
