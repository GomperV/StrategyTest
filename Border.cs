using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Border : MonoBehaviour
{
    private GameObject borderPrefab;
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
                if (i > 8)
                {
                    yield return null;
                    break;
                }
            }
            yield return null;
        }
    }

    void Start()
    {

        GameObject teren = GameObject.Find("Teren"); 
        borderPrefab = GameObject.Find("border");
        // Get a random child of the "Teren" game object
        Transform randomChild = teren.transform.GetChild(Random.Range(0, teren.transform.childCount));
        GameObject border = Instantiate(borderPrefab, randomChild.position, Quaternion.identity, randomChild) as GameObject;
        border.GetComponent<SpriteRenderer>().color = Color.red;


        
        float radius = 1f;
        Vector2 circlePosition = randomChild.position;

        Collider2D[] neighbours = Physics2D.OverlapCircleAll(circlePosition, radius);

        List<GameObject> neighbourGameObjects = new List<GameObject>();
        foreach (Collider2D collider in neighbours)
        {
            GameObject neighbourGameObject = collider.gameObject;
            neighbourGameObjects.Add(neighbourGameObject);
        }

        StartCoroutine(InstantiateBorders(neighbourGameObjects, borderPrefab));
    }
}
