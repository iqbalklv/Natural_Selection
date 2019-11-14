using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodSystem : MonoBehaviour
{

    public static List<GameObject> foods;
    public static List<Vector2> foodLocs = new List<Vector2>();
    public int initialFoodCount;
    public float foodCooldown;
    int foodCount;

    public GameObject foodItem;
    public Transform[] corners;
    public static Transform[] bounds;

    void Awake()
    {
        bounds = new Transform[2];

        Debug.Log(corners);
        bounds[0] = corners[0];
        Debug.Log(bounds);

        bounds[1] = corners[1];
        foodCount = initialFoodCount;

        StartCoroutine(BulkFoodSpawn());
        StartCoroutine(FoodSpawn());
    }

    public IEnumerator BulkFoodSpawn()
    {
        while (true)
        {
            for (int i = 0; i < foodCount; i++)
            {
                SpawnFoodInRandomPointInArea();
            }

            int foodReproRate = foodLocs.Count / 2;
            foodCount = foodReproRate;

            yield return new WaitForSeconds(foodCooldown);
        }
    }

    public IEnumerator FoodSpawn()
    {
        for (int i = 0; i < foodCount; i++)
        {
            SpawnFoodInRandomPointInArea();
        }

        while (true)
        {
        
       // for (int i = 0; i < foodCount; i++)
       // {
                SpawnFoodInRandomPointInArea();
                yield return new WaitForSeconds(.01f);
       // }
                
           int foodReproRate = foodLocs.Count / 2;
           foodCount = foodReproRate;

            //yield return new WaitForSeconds(foodCooldown);

        }
    }

    void SpawnFoodInRandomPointInArea()
    {
        Vector2 pos;
        int chance = 0;

        do
        {
            chance++;

            pos.x = ((int)Random.Range(bounds[0].position.x, bounds[1].position.x));
            pos.y = ((int)Random.Range(bounds[1].position.y, bounds[0].position.y));

            if (chance > 10)
                break;

        } while (foodLocs.Contains(pos));

        if(chance <= 10)
        {
            foodLocs.Add(pos);
            Instantiate(foodItem, pos, Quaternion.identity);
        }

    }
}
