using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class TurnManagerScript : MonoBehaviour
{
    public int liczbaKamieniolomow, liczbaTartakow, liczbaZlota, liczbaStraznic, liczbaKoszar, ktoraTura;
    int[] iloscBudynkow;
    [SerializeField] private TMP_Text KtoraTuraTXT;

    // Start is called before the first frame update
    void Start()
    {
        ktoraTura = 0;
        iloscBudynkow = new int[] { liczbaKamieniolomow, liczbaTartakow, liczbaStraznic, liczbaKoszar };
        for (int i = 0; i < iloscBudynkow.Length; i++)
        {
            iloscBudynkow[i] = 0;
        }
    }

    // Update is called once per frame
    void Update()
    {
        KtoraTuraTXT.text = ktoraTura.ToString();
    }

    public void NastepnaTura()
    {
        ktoraTura++;

        GameObject go = GameObject.FindWithTag("Teren");
        SquareScript square = go.GetComponent<SquareScript>();

        GameObject ai = GameObject.Find("EnemyAI");
        TaskManagerAI task = ai.GetComponent<TaskManagerAI>();

        task.GroundControl();

        GameObject go2 = GameObject.Find("BuilderManager");
        BuildManager stats = go2.GetComponent<BuildManager>();

        if (liczbaKamieniolomow > 0)
        {
            stats.resources["kamien"] = stats.resources["kamien"] + liczbaKamieniolomow * 20;
        }
        if (liczbaTartakow > 0)
        {
            stats.resources["drewno"] = stats.resources["drewno"] + liczbaTartakow * 25;
        }
        if (liczbaKoszar > 0)
        {
            stats.zolnierze += 1;
            stats.resources["zloto"] = stats.resources["zloto"]- liczbaKoszar * 5;
        }
        if (liczbaStraznic > 0)
        {
            stats.resources["zloto"] = stats.resources["zloto"] - liczbaStraznic * 5;
        }
        stats.UpdateResourceTexts();
    }
}
