using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierScript : MonoBehaviour
{
    public bool zolnierzWybrany = false;
    private Collider2D squareCollider;
    public int iloscPrzesuniec = 1;

    void OnMouseDown()
    {
        zolnierzWybrany = true;
        GetComponent<SpriteRenderer>().color = Color.gray;
        print("Zolnierz wybrany: " + zolnierzWybrany);
    }

    private void Update()
    {
        GameObject[] objectsWithTag = GameObject.FindGameObjectsWithTag("Teren");

        // Loop through the game objects and check if they are within range
        foreach (GameObject obj in objectsWithTag)
        {
            if (obj != null && Vector2.Distance(transform.position, obj.transform.position) < 2f)
            {
                // Game object is within range
                print("Square w zasiegu");
                SquareScript square = obj.GetComponent<SquareScript>();

                if (square != null)
                {
                    print("Square widoczny");
                    square.visibleByPlayer = true;
                }
            }
        }

        if (iloscPrzesuniec < 1 )
        {
            GetComponent<SpriteRenderer>().color = Color.black;
        } else if(zolnierzWybrany) 
        {
            GetComponent<SpriteRenderer>().color = Color.gray;
        }
        else
        {
            GetComponent<SpriteRenderer>().color = Color.white;
        }
    }

    public void MoveSoldier(Transform pozycjaDocelowa)
    {
        if (iloscPrzesuniec > 0 && Vector2.Distance(transform.position, pozycjaDocelowa.transform.position) < 1.5f)
        {

            GetComponent<SpriteRenderer>().color = Color.white;
            zolnierzWybrany = false;
            transform.position = pozycjaDocelowa.transform.position;
            print("Soldier moved");
            iloscPrzesuniec -= 1;
        } else
        {
            print("Square za daleko");
        }

    }
}
