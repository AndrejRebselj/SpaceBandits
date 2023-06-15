using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipSetup : MonoBehaviour
{
    [SerializeField] SpriteRenderer shipOption;
    [SerializeField] List<Sprite> sprites;
    private void Start()
    {
        switch (PrefabVariables.PickedShip)
        {
            case 0:
                shipOption.sprite=sprites[0];
                break;
            case 1:
                shipOption.sprite = sprites[1];
                break;
            default:
                shipOption.sprite = sprites[0];
                break;
        }
    }
}
