using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : MonoBehaviour
{
    private GameObject borderPrefab;
    // Start is called before the first frame update
    void Start()
    {
        borderPrefab = GameObject.Find("border");
        // Get the "Teren" game object
        GameObject teren = GameObject.Find("Teren");

        // Get a random child of the "Teren" game object
        Transform randomChild = teren.transform.GetChild(Random.Range(0, teren.transform.childCount));
        GameObject border = Instantiate(borderPrefab, randomChild.position, Quaternion.identity, randomChild) as GameObject;
        border.GetComponent<SpriteRenderer>().color = Color.red;
    }
}
