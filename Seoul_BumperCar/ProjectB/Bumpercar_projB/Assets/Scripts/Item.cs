using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public enum ItemType
    {
        FixPosition,
        AddLife, //+1
        AccelGauge, //*1.5f
        ExplodeEnemy,
        DismissEffect,
        DismissDebuff,
        GetShield,
        ReduceImpulse,

 
    }

    public static int GetNumber(ItemType itemType)
    {
        switch (itemType)
        {
            default:
            case ItemType.FixPosition: return 1;
            case ItemType.AddLife: return 2; 
            case ItemType.AccelGauge: return 3; 
            case ItemType.ExplodeEnemy: return 4;
            case ItemType.DismissEffect: return 5;
            case ItemType.DismissDebuff: return 6;
            case ItemType.GetShield: return 7;
            case ItemType.ReduceImpulse: return 8;
        }

    }
}
