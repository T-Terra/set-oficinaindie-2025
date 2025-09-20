using System;
using UnityEngine;

[CreateAssetMenu(fileName = "PowerUpCardData", menuName = "Power-Up Card/New Card")]
public class PowerUpCardData : ScriptableObject
{
    [SerializeField] int Id;
    [SerializeField] Sprite SpriteRender;
    [SerializeField] int Level;
    [SerializeField] float Damage;
    [Tooltip("1- Attack \n2- Defense \n3- Strategy")]
    [SerializeField] int AbilityType;
    [SerializeField] float Cooldown;

}
