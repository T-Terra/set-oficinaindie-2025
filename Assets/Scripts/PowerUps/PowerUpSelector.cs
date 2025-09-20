using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class PowerUpSelector : MonoBehaviour
{
    public GameObject PowerUpPrefab; // Prefab que você quer instanciar
    public Transform parentObject; // Objeto que será o pai

    void Start()
    {
        List<PowerUpCardData> selecionados = GetRandomPowerUps();
        foreach (PowerUpCardData p in selecionados)
        {
            GameObject instance = Instantiate(PowerUpPrefab, parentObject);
            Debug.Log("Selecionado: " + p.description);
            instance.GetComponentInChildren<TMP_Text>().text = p.description;
        }
    }

    public List<PowerUpCardData> allPowerUps;
    public int numberToSelect = 3;

    public List<PowerUpCardData> GetRandomPowerUps()
    {
        List<PowerUpCardData> tempList = new List<PowerUpCardData>(allPowerUps);
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
}