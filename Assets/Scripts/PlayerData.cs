using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public int xp = 0;
    public int coins = 0;
    public int level = 0;

    public int maxShield = 20;
    public int shield = 20;
    public float baseDamage = 1.0f;
    public float damage = 1.0f;

    public float coinMultiplier = 1.0f;

    public int clones = 0;
    public float cloneDamage = 0;

    public List<PowerUpCardData> powerUpCards = new();


    public void TakeDamage(int damage)
    {
        if (shield < 0)
        {
            GameManager.Instance.GameOver();
            return;
        }

        shield -= damage;
    }

    public void AddExperience(int amount)
    {
        xp += amount;
        int xpForNextLevel = (level + 1) * 5;
        if (xp >= xpForNextLevel)
        {
            xp -= xpForNextLevel;
            level++;
            GameManager.Instance.LevelUp();
        }
    }

    public void AddCoins(int amount)
    {
        coins += Mathf.RoundToInt(amount * coinMultiplier);
    }

    public bool SpendCoins(int amount)
    {
        if (coins >= amount)
        {
            coins -= amount;
            return true;
        }
        else
        {
            Debug.LogError("Not enough coins!");
            return false;
        }
    }

    public void AddPowerUpCard(PowerUpCardData card)
    {
        powerUpCards.Add(card);
        UpdateStats();
    }

    void UpdateStats()
    {
        var bestLevelCards = powerUpCards.GroupBy(card => card.AbilityType)
            .Select(group => group.OrderByDescending(card => card.level).First())
            .ToList();

        foreach (var card in bestLevelCards)
        {
            if (card.increaseShield > 0)
            {
                maxShield += card.increaseShield;
                shield += card.increaseShield;
                shield = Mathf.Min(shield, maxShield);
            }
            if (card.clones > 0)
            {
                clones = card.clones;
                cloneDamage = damage * card.damage;
            }
            else if (card.damage > 0) damage = card.damage;
            if (card.coinMultiplier > 0) coinMultiplier = card.coinMultiplier;
        }
    }

}