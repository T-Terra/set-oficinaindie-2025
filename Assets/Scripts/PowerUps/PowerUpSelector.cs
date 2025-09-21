using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class PowerUpSelector : MonoBehaviour
{
    public static PowerUpSelector Instance;

    public GameObject PowerUpPrefab; // Prefab que você quer instanciar
    public Transform parentObject; // Objeto que será o pai
    public int priceReroll = 500;
    [SerializeField] GameObject RollButtonObj;
    [SerializeField] GameObject HUDPowerupObj;
    public List<PowerUpCardData> allPowerUps;
    public List<PowerUpCardData> availablePowerUps;
    public int numberToSelect = 3;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        availablePowerUps = new List<PowerUpCardData>(allPowerUps);
    }

    void Update()
    {

    }

    public List<PowerUpCardData> GetRandomPowerUps()
    {
        List<PowerUpCardData> selected = new List<PowerUpCardData>();

        var groups = availablePowerUps
            .GroupBy(p => p.AbilityType)
            .OrderBy(p => Random.value);

        foreach (var group in groups)
        {
            List<PowerUpCardData> OrdenedList = group.OrderBy(p => p.level).ToList();
            PowerUpCardData chosen = OrdenedList[0];

            if (selected.Count() < numberToSelect) selected.Add(chosen);
        }

        return selected;
    }

    public void EnablePowerUpHud()
    {
        GameManager.Instance.Pause(true);

        List<PowerUpCardData> selected = GetRandomPowerUps();
        foreach (PowerUpCardData card in selected)
        {
            GameObject instance = Instantiate(PowerUpPrefab, parentObject);

            PowerUpButton powerUpButton = instance.GetComponent<PowerUpButton>();
            powerUpButton.Setup(card);
        }

        if (GameManager.Instance.playerData.coins <= priceReroll)
        {
            HandleRerollButton(false);
        }
        else
        {
            HandleRerollButton(true);
        }
        HUDPowerupObj.SetActive(true);
    }

    public void RollPowerUp()
    {
        foreach (Transform child in parentObject.transform)
        {
            Destroy(child.gameObject);
        }
        EnablePowerUpHud();
    }

    public void RerollPowerUp()
    {
        RollPowerUp();
        GameManager.Instance.playerData.SpendCoins(priceReroll);
    }

    public void HandleRerollButton(bool enable)
    {
        RollButtonObj.GetComponent<Button>().interactable = enable;
    }

    public void DiseblePowerUpHud(bool enable)
    {
        HUDPowerupObj.SetActive(enable);

        GameManager.Instance.Pause(false);
    }
}