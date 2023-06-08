using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SquareScript : MonoBehaviour
{
    public bool squareOccupied = false;
    private bool canBuild = false;
    private bool terenZalesiony = false;
    private bool terenKamienisty = false;
    private bool terenWodny = false;
    private bool terenZwykly = false;
    public bool visibleByPlayer;
    private bool tartakAI, kamieniolomAI, straznicaAI, koszaryAI;
    public int iloscKamieniolomow;
    private GameObject borderPrefab, koszaryManager;
    private bool terenGracza = false;
    private string buildingOnThisSquare;

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

    private void Update()
    {
        // Find the child object with the specified name
        Transform border = transform.Find("border(Clone)");
        if (buildingOnThisSquare == "straznica")
        {
            GetComponent<SpriteRenderer>().color = Color.black;
            squareOccupied = true;
        }
        else if (buildingOnThisSquare == "koszary")
        {
            GetComponent<SpriteRenderer>().color = Color.cyan;
            squareOccupied = true;
        }
        // Check if the child object was found
        if (border!= null)
        {
            visibleByPlayer = true;
        }

        if (!visibleByPlayer && !squareOccupied)
        {
            GetComponent<SpriteRenderer>().color = Color.black;
        } else if (!squareOccupied && visibleByPlayer)
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

        koszaryManager = GameObject.Find("KoszaryManager");
        KoszaryManager km = koszaryManager.GetComponent<KoszaryManager>();

        if (transform.Find("border(Clone)") != null)
        {
            //print("twoj teren");
            terenGracza = true;
        }
        else
        {
            //print("neutral");
            terenGracza = false;
        }

        if (builder.canBuild && !squareOccupied && terenGracza)
        {
            if(builder.buildingType == "kamieniolom" && terenKamienisty)
            {
                stats.liczbaKamieniolomow += 1;
                GetComponent<SpriteRenderer>().sprite = kamieniolom;
                Build(builder.buildingType);
            } 
            else if(builder.buildingType == "tartak" && terenZalesiony)
            {
                stats.liczbaTartakow += 1;
                GetComponent<SpriteRenderer>().sprite = tartak;
                Build(builder.buildingType);
            }
            else if (builder.buildingType == "straznica" && terenZwykly)
            {
                squareOccupied = true;
                stats.liczbaStraznic += 1;
                GetComponent<SpriteRenderer>().color = Color.black;
                Build(builder.buildingType);
            }
            else if (builder.buildingType == "koszary" && terenZwykly)
            {
                squareOccupied = true;
                stats.liczbaKoszar += 1;
                GetComponent<SpriteRenderer>().color = Color.cyan;
                Build(builder.buildingType);

                km.PobierzWspolrzedneKoszar(transform);
            }
        }
        int lucznikCount = km.iloscZolnierzy; // set to the total number of Lucznik objects in your scene


        for (int i = 1; i <= lucznikCount; i++)
        {
            if (GameObject.Find("Lucznik " + i.ToString()) != null)
            {
                GameObject soldier = GameObject.Find("Lucznik " + i.ToString());
                SoldierScript solsc = soldier.GetComponent<SoldierScript>();
                if (solsc.zolnierzWybrany)
                {
                    print("dostalem info o wybraniu zolnierza");
                    solsc.MoveSoldier(transform);
                    break; // stop checking for other Lucznik objects
                }
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
        buildingOnThisSquare = buildingType;
        
    }

    void OnMouseExit()
    {
        if (!squareOccupied)
        {
            GetComponent<SpriteRenderer>().color = new Color(0.764151f, 0.764151f, 0.764151f);
        }
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
