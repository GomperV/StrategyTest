using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SquareScript : MonoBehaviour
{
    private bool squareOccupied = false;
    private bool canBuild = false;
    private bool terenZalesiony = false;
    private bool terenKamienisty = false;
    private bool terenWodny = false;
    private bool terenZwykly = false;
    private bool tartakAI, kamieniolomAI, straznicaAI, koszaryAI;
    public int iloscKamieniolomow;
    private GameObject borderPrefab;
    private bool terenGracza = false;

    public Sprite grass, stone, woods, water, tartak, kamieniolom;
    void Start()
    {
        borderPrefab = GameObject.Find("border");
        iloscKamieniolomow = 0;
        // Generate a random number between 0 and 1
        float randomValue = Random.Range(0f, 1f);
        // Set the terenZalesiony flag if the random number is greater than 0.5
        if (randomValue > 0.7f)
        {
            terenZalesiony = true;
            //GetComponent<SpriteRenderer>().color = new Color(0, 0.3f, 0);
            GetComponent<SpriteRenderer>().sprite = woods;
        }
        else if (randomValue > 0.65f && randomValue <= 0.7f)
        {
            terenKamienisty = true;
            GetComponent<SpriteRenderer>().sprite = stone;
            //GetComponent<SpriteRenderer>().color = new Color(0.6792453f, 0.6792453f, 0.6792453f);
        }
        else if (randomValue > 0.4f && randomValue <= 0.5f)
        {
            terenWodny = true;
            GetComponent<SpriteRenderer>().sprite = water;
            //GetComponent<SpriteRenderer>().color = Color.blue;
        }
        if(!terenKamienisty && !terenZalesiony && !terenWodny)
        {
            terenZwykly = true;
            GetComponent<SpriteRenderer>().sprite = grass;
        }

    }
    void OnMouseOver()
    {
        if (!squareOccupied)
        {
            GetComponent<SpriteRenderer>().color = Color.white;
        }

    }
    void OnMouseDown()
    {
        GameObject go = GameObject.Find("BuilderManager");
        BuildManager builder = go.GetComponent<BuildManager>();

        GameObject go2 = GameObject.Find("TurnManager");
        TurnManagerScript stats = go2.GetComponent<TurnManagerScript>();

        if (transform.Find("border(Clone)") != null)
        {
            print("twoj teren");
            terenGracza = true;
        }
        else
        {
            print("neutral");
            terenGracza = false;
        }

        if (builder.canBuild && !squareOccupied && terenGracza)
        {
            if(builder.buildingType == "kamieniolom" && terenKamienisty)
            {
                stats.liczbaKamieniolomow += 1;
                GetComponent<SpriteRenderer>().sprite = kamieniolom;
                //GetComponent<SpriteRenderer>().color = Color.gray;
                Build(builder.buildingType);
            } 
            else if(builder.buildingType == "tartak" && terenZalesiony)
            {
                stats.liczbaTartakow += 1;
                GetComponent<SpriteRenderer>().sprite = tartak;
                //GetComponent<SpriteRenderer>().color = Color.yellow;
                Build(builder.buildingType);
            }
            else if (builder.buildingType == "straznica" && terenZwykly)
            {
                stats.liczbaStraznic += 1;
                GetComponent<SpriteRenderer>().color = Color.black;
                Build(builder.buildingType);
            }
        }
    }

    private void Build(string buildingType)
    {
        GameObject go = GameObject.Find("BuilderManager");
        BuildManager builder = go.GetComponent<BuildManager>();
        squareOccupied = true;
        canBuild = false;
        builder.canBuild = false;

        if (buildingType == "straznica")
        {
            RozszerzTeren();
        }
    }

    void OnMouseExit()
    {
        if (!squareOccupied)
        {
            GetComponent<SpriteRenderer>().color = new Color(0.764151f, 0.764151f, 0.764151f);
        }
        //if (!squareOccupied && !terenZalesiony && !terenWodny && !terenKamienisty)
        //{
        //    //SpriteRenderer renderer = GetComponent<SpriteRenderer>();
        //    //renderer.color = new Color(0.01159281f, 0.5849056f, 0f, 1f);
        //    GetComponent<SpriteRenderer>().sprite = grass;
        //}
        //else if (!squareOccupied && terenZalesiony)
        //{
        //    //GetComponent<SpriteRenderer>().color = new Color(0, 0.3f, 0);
        //    GetComponent<SpriteRenderer>().sprite = woods;
        //}
        //else if (!squareOccupied && terenWodny)
        //{
        //    GetComponent<SpriteRenderer>().sprite = water;
        //    //GetComponent<SpriteRenderer>().color = Color.blue;
        //}
        //else if (!squareOccupied && terenKamienisty)
        //{
        //    GetComponent<SpriteRenderer>().sprite = stone;
        //    //GetComponent<SpriteRenderer>().color = new Color(0.6792453f, 0.6792453f, 0.6792453f);
        //} else if (!squareOccupied && terenZwykly)
        //{
        //    GetComponent<SpriteRenderer>().sprite = grass;
        //    //SpriteRenderer renderer = GetComponent<SpriteRenderer>();
        //    //renderer.color = new Color(0.01159281f, 0.5849056f, 0f, 1f);
        //}
    }
    private void RozszerzTeren()
    {
        GameObject teren = GameObject.Find("Teren");
        borderPrefab = GameObject.Find("border");
        // Get a random child of the "Teren" game object
        //Transform randomChild = teren.transform.GetChild(Random.Range(0, teren.transform.childCount));
        GameObject border = Instantiate(borderPrefab, transform.position, Quaternion.identity, transform) as GameObject;
        border.GetComponent<SpriteRenderer>().color = Color.red;

        float radius = 2f;
        Vector2 circlePosition = transform.position;

        Collider2D[] neighbours = Physics2D.OverlapCircleAll(circlePosition, radius);

        List<GameObject> neighbourGameObjects = new List<GameObject>();
        foreach (Collider2D collider in neighbours)
        {
            GameObject neighbourGameObject = collider.gameObject;
            neighbourGameObjects.Add(neighbourGameObject);
        }

        StartCoroutine(InstantiateBorders(neighbourGameObjects, borderPrefab));
    }

    IEnumerator InstantiateBorders(List<GameObject> neighbourGameObjects, GameObject borderPrefab)
    {
        int i = 0;
        foreach (GameObject neighbour in neighbourGameObjects)
        {
            Transform neighbourTransform = neighbour.transform;
            if (neighbourTransform != null)
            {
                GameObject border = Instantiate(borderPrefab, neighbourTransform.position, Quaternion.identity, neighbourTransform);
                border.GetComponent<SpriteRenderer>().color = Color.red;
                i++;
                print("obiekt nr: " + i);
                yield return new WaitForSecondsRealtime(0.1f);
                if (i > 25)
                {
                    yield return null;
                    break;
                }
            }
            yield return null;
        }
    }
}
