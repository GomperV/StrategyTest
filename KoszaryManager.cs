using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KoszaryManager : MonoBehaviour
{
    public GameObject zolnierz;
    Transform pozycja;
    public int iloscZolnierzy = 0;
    public void PobierzWspolrzedneKoszar(Transform pozycjaKoszar)
    {
        pozycja = pozycjaKoszar;
    }
    public void CreateSoldier()
    {
        GameObject go = GameObject.Find("BuilderManager");
        BuildManager builder = go.GetComponent<BuildManager>();

        if(builder.resources["zloto"] > 10)
        {
            builder.resources["zloto"] -= 10;
            builder.UpdateResourceTexts();
            iloscZolnierzy++;
            GameObject lucznik = Instantiate(zolnierz, pozycja.position, Quaternion.identity, pozycja);
            lucznik.name = "Lucznik " + iloscZolnierzy.ToString();
            print("ROBIE LUCZNIKA");
        }

    }

}
