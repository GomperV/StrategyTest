using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BuildManager : MonoBehaviour
{
    public bool canBuild = false;
    private int stoneCost, woodCost;
    public string buildingType;
    public Dictionary<string, int> resources = new Dictionary<string, int>();
    public int zolnierze;
    [SerializeField] private TMP_Text[] resourceTexts;

    void Start()
    {
        resources["drewno"] = 100;
        resources["kamien"] = 100;
        resources["zloto"] = 200;
        UpdateResourceTexts();
    }

    public void UpdateResourceTexts()
    {
        for (int i = 0; i < resourceTexts.Length; i++)
        {
            string resourceName = resourceTexts[i].gameObject.name.Substring(0);
            resourceTexts[i].text = resources[resourceName].ToString();
        }
    }

    public void BuildKamieniolom()
    {
        buildingType = "kamieniolom";
        stoneCost = 20;
        woodCost = 10;
        if (resources["kamien"] >= stoneCost && resources["drewno"] >= woodCost)
        {
            resources["kamien"] -= stoneCost;
            resources["drewno"] -= woodCost;
            canBuild = true;
            UpdateResourceTexts();
        }
    }

    public void BuildTartak()
    {
        buildingType = "tartak";
        stoneCost = 20;
        woodCost = 20;
        if (resources["kamien"] >= stoneCost && resources["drewno"] >= woodCost)
        {
            resources["kamien"] -= stoneCost;
            resources["drewno"] -= woodCost;
            canBuild = true;
            UpdateResourceTexts();
        }
    }
    public void BuildStraznica()
    {
        buildingType = "straznica";
        stoneCost = 20;
        woodCost = 20;
        if (resources["kamien"] >= stoneCost && resources["drewno"] >= woodCost)
        {
            resources["kamien"] -= stoneCost;
            resources["drewno"] -= woodCost;
            canBuild = true;
            UpdateResourceTexts();
        }
    }
    public void BuildKoszary()
    {
        buildingType = "koszary";
        stoneCost = 30;
        woodCost = 30;
        if (resources["kamien"] >= stoneCost && resources["drewno"] >= woodCost)
        {
            resources["kamien"] -= stoneCost;
            resources["drewno"] -= woodCost;
            canBuild = true;
            UpdateResourceTexts();
        }
    }
}