using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PowerUpButton : MonoBehaviour
{
    [SerializeField] Sprite sprite;
    [SerializeField] int level;
    [SerializeField] TMP_Text description;
    [SerializeField] float damage;
    [SerializeField] int clones;
    [SerializeField] int increaseShield;
    [SerializeField] float coinMultiplier;
    [SerializeField] int AbilityType;

    private PowerUpCardData powerUpData;

    // Chamado pelo HUD para configurar o botão
    public void Setup(PowerUpCardData data)
    {
        powerUpData = data;

        if (sprite != null) sprite = data.sprite;
        level = data.level;
        if (description != null) description.text = data.description;
        damage = data.damage;
        clones = data.clones;
        increaseShield = data.increaseShield;
        coinMultiplier = data.coinMultiplier;
        AbilityType = data.AbilityType;
    }

    // Quando o botão é clicado
    public void OnClick()
    {
        // aqui você pode aplicar o efeito no jogador, ex:
        PowerUpSelector.Instance.availablePowerUps.Remove(powerUpData);
        GameManager.Instance.playerData.AddPowerUpCard(powerUpData);
        PowerUpSelector.Instance.DiseblePowerUpHud(false);
    }
}
