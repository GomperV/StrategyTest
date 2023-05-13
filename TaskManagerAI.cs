using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskManagerAI : MonoBehaviour
{
    int territories;
    int terenDzialania;
    private GameObject task;
    // Start is called before the first frame update
    void Start()
    {
        GroundControl();
    }

    public void GroundControl()
    {
        // Get all game objects in the scene
        GameObject[] allGameObjects = FindObjectsOfType<GameObject>();

        // List to store the game objects with a child called "borderAI"
        List<GameObject> objectsWithBorderAI = new List<GameObject>();

        // Loop through all the game objects in the scene
        foreach (GameObject obj in allGameObjects)
        {
            // Check if the game object has a child called "borderAI"
            Transform borderAITransform = obj.transform.Find("borderAI(Clone)");

            // If it does, add it to the list of game objects with "borderAI" children
            if (borderAITransform != null)
            {
                objectsWithBorderAI.Add(obj);
            }
        }


        territories = objectsWithBorderAI.Count;
        print("Ilosc obszaru AI: " + territories);

        terenDzialania = Random.Range(0, territories);
        print("Teren dzialania: " + terenDzialania);
        print(objectsWithBorderAI[terenDzialania]);
        task = objectsWithBorderAI[terenDzialania];
        int operacja = Random.Range(0, 3);
        if (operacja == 0)
        {

        } else if(operacja == 1)
        {
            
        } else if(operacja == 2)
        {

        }
        task.GetComponent<SpriteRenderer>().color = Color.black;
    }
}
