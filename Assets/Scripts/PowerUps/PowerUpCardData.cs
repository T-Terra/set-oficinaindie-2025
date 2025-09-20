using System;
using UnityEngine;

[CreateAssetMenu(fileName = "PowerUpCardData", menuName = "Power-Up Card/New Card")]
public class PowerUpCardData : ScriptableObject
{
    public Sprite sprite;
    public int level;
    public string description;
    public float damage;
    public int clones;
    public int increaseShield;
    public float coinMultiplier;
    public int AbilityType;

}
