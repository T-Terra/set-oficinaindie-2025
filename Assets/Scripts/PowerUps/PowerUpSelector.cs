using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class PowerUpSelector : MonoBehaviour
{
    public GameObject PowerUpPrefab; // Prefab que você quer instanciar
    public Transform parentObject; // Objeto que será o pai
    public List<PowerUpCardData> allPowerUps;
    public List<PowerUpCardData> availablePowerUps;
    public int numberToSelect = 3;
    [SerializeField] GameObject RollButton;

    void Start()
    {
        availablePowerUps = new List<PowerUpCardData>(allPowerUps);
    }

    void Update()
    {
        
    }

    public List<PowerUpCardData> GetRandomPowerUps()
    {
        List<PowerUpCardData> tempList = new List<PowerUpCardData>(availablePowerUps);
        List<PowerUpCardData> selected = new List<PowerUpCardData>();

        var groups = tempList
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
        List<PowerUpCardData> selected = GetRandomPowerUps();
        foreach (PowerUpCardData card in selected)
        {
            GameObject instance = Instantiate(PowerUpPrefab, parentObject);

            PowerUpButton powerUpButton = instance.GetComponent<PowerUpButton>();
            powerUpButton.Setup(card);
        }
        RollButton.SetActive(true);
    }

    public void RollPowerUp()
    {
        foreach (Transform child in parentObject.transform)
        {
            if (parentObject.transform.childCount != 0) Destroy(child.gameObject);
        }
        EnablePowerUpHud();
    }
}